using Database.Lib;
using Database.Lib.Data;
using Database.Lib.DataProviders;
using Database.Lib.DataProviders.ConnectionParams;
using Database.Lib.Misc;
using Database.Lib.Search;
using EasyDB.Forms;
using EasyDB.Properties;
using EasyDB.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace EasyDB
{
	public partial class MainForm : Form
	{
		public DataProvider DataProvider { get; set; } = new DataProvider();
		IDbObject _selectedObject;

		private string _query = "";

		private List<Button> CheckButtons = new List<Button>();

		public MainForm()
		{
			InitializeComponent();
			//AcceptButton = btnSearch;
		}

		#region Private methods

		private void SearchInBackground()
		{
			if (!bw.IsBusy)
			{
				Console.WriteLine("SearchInBackground");

				lblLoading.Visible = true;
				listDbObjects.Items.Clear();
				gridColumns.DataSource = null;
				txtSqlScript.Text = "";

				_query = txtSearchQuery.Text;
				bw.RunWorkerAsync();
			}
		}

		private void SetConnection()
		{
			using (var connDialog = new ConnectionDialog())
			{
				connDialog.Server = Settings.Default.Server;
				connDialog.Database = Settings.Default.Database;
				connDialog.Username = Settings.Default.Username;
				connDialog.IntegratedSecurity = Settings.Default.IntegratedSecurity;

				if (connDialog.ShowDialog() == DialogResult.OK)
				{
					var connRes = DataProvider.AddMSSqlManager("default", new MSSQLConnectionParams()
					{
						Server = connDialog.Server,
						Database = connDialog.Database,
						Username = connDialog.Username,
						Password = connDialog.Password,
						SetIntegratedSecurity = connDialog.IntegratedSecurity
					});
					DataProvider.SetActiveManager("default");

					if (connRes.Success && DataProvider.ActiveManager.IsConnected)
					{
						Settings.Default.Server = connDialog.Server;
						Settings.Default.Database = connDialog.Database;
						Settings.Default.Username = connDialog.Username;
						Settings.Default.Password = connDialog.Password;
						Settings.Default.IntegratedSecurity = connDialog.IntegratedSecurity;
						Settings.Default.Save();

						lblStatus.Text = $"Connected {DataProvider.ActiveManager.ConnectionString}";

						SearchInBackground();
					}
					else
					{
						EasyMessageBox.Error(connRes.Exception.Message);
						lblStatus.Text = "Not Connected!";
					}
				}
			}
		} 

		private void GridHighlithRow()
		{
			if (txtSearchQuery.Text != "")
			{
				foreach (DataGridViewRow row in gridColumns.Rows)
				{
					if (row.Cells[0].Value.ToString().ToLowerInvariant().Contains(txtSearchQuery.Text.ToLowerInvariant()))
					{
						row.DefaultCellStyle.BackColor = Color.Orange;
					}
				}
			}
		}

		private void InitObjectsList()
		{
			ColumnHeader header = new ColumnHeader();
			header.Text = "Database Objects";
			header.Name = "col1";
			header.Width = listDbObjects.Width - 25;
			listDbObjects.Columns.Add(header);

			var enums = EnumHelper<EDbObjects>.ToDictionary();
			enums.Remove((int)EDbObjects.None);
			enums.Remove((int)EDbObjects.All);

			int buttonLeftPos = 0;
			foreach(var e in enums)
			{
				var checkButton = new Button();
				var buttonData = new CheckButtonData() { Checked = false, Flag = e.Key, Name = e.Value };
				checkButton.Text = e.Value;
				checkButton.Left = buttonLeftPos;
				checkButton.Width = 200;
				buttonLeftPos += checkButton.Width;
				checkButton.Click += (object sender, EventArgs evg) => 
				{
					var btn = sender as Button;
					var data = btn.Tag as CheckButtonData;
					data.Checked = !data.Checked;

					if (data.Checked)
						btn.BackColor = Color.Orange;
					else
						btn.BackColor = SystemColors.Control;

					SearchInBackground();
				};

				checkButton.Tag = buttonData;

				CheckButtons.Add(checkButton);
				panelCheckButtons.Controls.Add(checkButton);
			}

			//chListDbObjects.DataSource = new BindingSource(enums, null);
			//chListDbObjects.DisplayMember = "Value";
			//chListDbObjects.ValueMember = "Key";
		}

		private void ChangeSqlScriptCase(bool upper = true)
		{
			string sql = txtSqlScript.Text;
			var keywords = Syntax.SQL_BLUE_KEYWORDS.Concat(Syntax.SQL_GRAY_KEYWORDS).Concat(Syntax.SQL_PURPLE_KEYWORDS);

			foreach (var kw in keywords)
			{
				sql = Regex.Replace(sql, $@"\b({kw})\b", upper ? kw.ToUpper() : kw.ToLower(), RegexOptions.IgnoreCase | RegexOptions.Singleline);
			}

			txtSqlScript.Text = sql;
		}

		private void BackupSelectedScript()
		{
			if (listDbObjects.FocusedItem?.Tag != null)
			{
				var dbObject = listDbObjects.FocusedItem.Tag as IDbObject;

				if (DataProvider.ActiveManager.IsView(dbObject))
				{
					dbObject = DataProvider.ActiveManager.View(dbObject);
				}
				else if (DataProvider.ActiveManager.IsStoredProcedure(dbObject))
				{
					dbObject = DataProvider.ActiveManager.StoredProcedure(dbObject);
				}
				else return;

				Tuple<string, string> output;
				if (FileHelper.BackupScript(dbObject.Name, dbObject.Script, out output))
				{
					EasyMessageBox.MessageWithActions(
						"Script saved",
						$"Script saved successfully.\r\n{output.Item1}",
						"OK",
						new ActionButton(() => Process.Start(output.Item1), "Open File"),
						new ActionButton(() => Process.Start(output.Item2), "Open Dir")
						);
				}
				else
				{
					EasyMessageBox.Error($"Error occured while saving file.\n{output.Item1}");
				}
			}
		}

		private SqlExForm ShowSqlExecutor(string script = "")
		{
			var sqlEx = new SqlExForm(DataProvider, script);
			sqlEx.Show();

			return sqlEx;
		}

		#endregion

		#region Event handlers

		private void txtSearchQuery_TextChanged(object sender, EventArgs e)
		{
			
		}

		private void bw_DoWork(object sender, DoWorkEventArgs e)
		{
			//var selected = chListDbObjects.CheckedItems.Cast<KeyValuePair<int, string>>().Select(s => s.Key).Sum();
			var selected = CheckButtons.Where(b => ((CheckButtonData)b.Tag).Checked).Select(s => ((CheckButtonData)s.Tag).Flag).Sum();
			EDbObjects searchIn = selected == 0 ? EDbObjects.All : (EDbObjects)selected;

			if (e == null)
				e = new DoWorkEventArgs(null);

			try
			{
				e.Result = DataProvider.ActiveManager.SearchInDb(_query, searchIn);
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			var results = e.Result as SearchResults;

			if (results.Error)
			{
				EasyMessageBox.Error(results.ErrorMessage);
				lblLoading.Visible = false;
				return;
			}

			foreach(var obj in results.FoundDbObjects)
			{
				var item = new ListViewItem();
				item.Text = obj.ToString();
				item.Tag = obj;
				
				if(DataProvider.ActiveManager.IsTable(obj))
				{
					item.ImageIndex = 0;
				}
				if (DataProvider.ActiveManager.IsView(obj))
				{
					item.ImageIndex = 1;
				}
				if (DataProvider.ActiveManager.IsStoredProcedure(obj))
				{
					item.ImageIndex = 2;
				}

				listDbObjects.Items.Add(item);
			}

			lblObjectsCount.Text = results.FoundDbObjects != null ? results.FoundDbObjects.Count + " Objects" : "";

			lblLoading.Visible = bw.IsBusy;
			txtSearchQuery.Focus();
		}

		private void listDbObjects_SelectedIndexChanged(object sender, EventArgs e)
		{
			gridColumns.DataSource = null;
			txtSqlScript.Text = "";

			_selectedObject = listDbObjects.FocusedItem?.Tag as IDbObject;


			txtSqlScript.Text = _selectedObject?.Script ?? "";

			if (DataProvider.ActiveManager.IsTable(_selectedObject))
			{
				gridColumns.DataSource = DataProvider.ActiveManager.Table(_selectedObject).Columns?.Tables[0];
				tabControl.SelectedTab = tabControl.TabPages[0];

				GridHighlithRow();
			}
			if (DataProvider.ActiveManager.IsView(_selectedObject))
			{
				tabControl.SelectedTab = tabControl.TabPages[1];
			}
			if (DataProvider.ActiveManager.IsStoredProcedure(_selectedObject))
			{
				tabControl.SelectedTab = tabControl.TabPages[1];
			}
		}

		private void txtSqlScript_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
		{
			e.ChangedRange.SetSyntaxSql();
			e.ChangedRange.HighlightText(txtSearchQuery.Text);
		}


		private void MainForm_Load(object sender, EventArgs e)
		{
			if (Settings.Default.MainWindowWidth >= 100 && Settings.Default.MainWindowHeight > 100)
			{
				Width = Settings.Default.MainWindowWidth;
				Height = Settings.Default.MainWindowHeight;
			}

			InitObjectsList();
			txtSearchQuery.Focus();

			if (Settings.Default.Server != "" && Settings.Default.Database != "")
			{
				var connRes = DataProvider.AddMSSqlManager("default", new MSSQLConnectionParams()
				{
					Server = Settings.Default.Server,
					Database = Settings.Default.Database,
					Username = Settings.Default.Username,
					Password = Settings.Default.Password,
					SetIntegratedSecurity = Settings.Default.IntegratedSecurity
				});

				//connRes = DataProvider.AddMySqlManager("mysql", new MySqlConnectionParams()
				//{
				//	Server = "127.0.0.1",
				//	Database = "skype",
				//	Username = "root",
				//	Password = null
				//});

				DataProvider.SetActiveManager("default");

				if (connRes.Success && DataProvider.ActiveManager.IsConnected)
				{
					lblStatus.Text = $"Connected {DataProvider.ActiveManager.ConnectionString}";
					SearchInBackground();
					return;
				}
				else
				{
					EasyMessageBox.Error(connRes.Exception.Message);
				}
			}

			lblStatus.Text = "Not Connected!";
			lblLoading.Visible = false;
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			bw.CancelAsync();
			//DataProvider.ActiveProvider?.Dispose();

			if(WindowState != FormWindowState.Maximized)
			{
				Settings.Default.MainWindowWidth = Width;
				Settings.Default.MainWindowHeight = Height;
				Settings.Default.Save();
			}
		}
		
		private void setConnectionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SetConnection();
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{
			SearchInBackground();
		}

		private void clearCacheToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DataProvider.ActiveManager?.ClearCache();
		}

		private void copyScriptToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (listDbObjects.FocusedItem?.Tag != null)
			{
				var dbObject = listDbObjects.FocusedItem.Tag as IDbObject;

				if (DataProvider.ActiveManager.IsView(dbObject))
				{
					Clipboard.SetText(DataProvider.ActiveManager.View(dbObject).Script);
				}
				if (DataProvider.ActiveManager.IsStoredProcedure(dbObject))
				{
					Clipboard.SetText(DataProvider.ActiveManager.StoredProcedure(dbObject).Script);
				}
			}
		}

		private void txtSearchQuery_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				e.SuppressKeyPress = true;
				e.Handled = true;
				SearchInBackground();
			}
		}

		private void reportBugToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Process.Start("https://github.com/TomasBouda/EasyDB/issues/new");
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using(var aboutDial = new AboutDialog())
			{
				aboutDial.ShowDialog();
			}
		}

		#endregion

		private void listDbObjects_Resize(object sender, EventArgs e)
		{
			if(listDbObjects.Columns.Count > 0)
				listDbObjects.Columns[0].Width = listDbObjects.Width - 25;
		}

		private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
		{
			
		}
		private void uppercaseSQLToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ChangeSqlScriptCase();
		}

		private void backupScriptToolStripMenuItem_Click(object sender, EventArgs e)
		{
			BackupSelectedScript();
		}

		private void copySelectStatementToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (listDbObjects.FocusedItem?.Tag != null)
			{
				var dbObject = listDbObjects.FocusedItem.Tag as IDbObject;

				if (DataProvider.ActiveManager.IsView(dbObject))
				{
					Clipboard.SetText($"SELECT * FROM {DataProvider.ActiveManager.View(dbObject).Schema}.{DataProvider.ActiveManager.View(dbObject).Name}");
				}
				if (DataProvider.ActiveManager.IsTable(dbObject))
				{
					Clipboard.SetText($"SELECT * FROM {DataProvider.ActiveManager.Table(dbObject).Schema}.{DataProvider.ActiveManager.Table(dbObject).Name}");
				}
			}
		}

		private void lowercaseSQLToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ChangeSqlScriptCase(false);
		}

		private void backupScriptToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			BackupSelectedScript();
		}

		private void sqlExecutorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string script = "";
			if (listDbObjects.FocusedItem?.Tag != null)
			{
				var dbObject = listDbObjects.FocusedItem.Tag as IDbObject;

				if (DataProvider.ActiveManager.IsView(dbObject))
				{
					script = DataProvider.ActiveManager.View(dbObject).Script;
				}
				if (DataProvider.ActiveManager.IsStoredProcedure(dbObject))
				{
					script = DataProvider.ActiveManager.StoredProcedure(dbObject).Script;
				}
			}

			ShowSqlExecutor(script);
		}
	}
}
