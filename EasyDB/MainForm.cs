using Database.Lib;
using Database.Lib.Data;
using Database.Lib.DataProviders;
using Database.Lib.DataProviders.ConnectionParams;
using Database.Lib.Misc;
using Database.Lib.Search;
using EasyDB.Properties;
using EasyDB.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace EasyDB
{
	public partial class MainForm : Form
	{
		public DatabaseManager<MSSQL> manager;

		private string _queryCache = "";

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
					manager = new DatabaseManager<MSSQL>();
					var connRes = manager.Connect(new MSSQLConnectionParams()
					{
						Server = connDialog.Server,
						Database = connDialog.Database,
						Username = connDialog.Username,
						Password = connDialog.Password,
						SetIntegratedSecurity = (EIntegratedSecurity)connDialog.IntegratedSecurity
					});

					if (connRes.Success && manager.DB.IsConnected)
					{
						Settings.Default.Server = connDialog.Server;
						Settings.Default.Database = connDialog.Database;
						Settings.Default.Username = connDialog.Username;
						Settings.Default.Password = connDialog.Password;
						Settings.Default.IntegratedSecurity = connDialog.IntegratedSecurity;
						Settings.Default.Save();

						lblStatus.Text = $"Connected {manager.DB.Connection.ConnectionString}";

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
			var enums = EnumHelper<EDbObjects>.ToDictionary();
			enums.Remove((int)EDbObjects.None);
			enums.Remove((int)EDbObjects.All);
			chListDbObjects.DataSource = new BindingSource(enums, null);
			chListDbObjects.DisplayMember = "Value";
			chListDbObjects.ValueMember = "Key";
		}

		#endregion

		#region Event handlers

		private void txtSearchQuery_TextChanged(object sender, EventArgs e)
		{
			_queryCache = txtSearchQuery.Text;
			SearchInBackground();
		}

		private void bw_DoWork(object sender, DoWorkEventArgs e)
		{
			var selected = chListDbObjects.CheckedItems.Cast<KeyValuePair<int, string>>().Select(s => s.Key).Sum();
			EDbObjects searchIn = selected == 0 ? EDbObjects.All : (EDbObjects)selected;

			if (e == null)
				e = new DoWorkEventArgs(null);

			try
			{
				e.Result = manager.SearchInDb(_queryCache, searchIn);
			}
			catch(Exception ex)
			{
				e.Result = manager.SearchInDb(_queryCache, searchIn);
				Console.WriteLine(ex.Message);
			}
		}

		private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			var results = e.Result as SearchResults<MSSQL>;

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
				
				if(obj is Table<MSSQL>)
				{
					item.ImageIndex = 0;
				}
				else if (obj is View<MSSQL>)
				{
					item.ImageIndex = 1;
				}
				else if (obj is StoredProcedure<MSSQL>)
				{
					item.ImageIndex = 2;
				}

				listDbObjects.Items.Add(item);
			}
			

			lblObjectsCount.Text = results.FoundDbObjects != null ? results.FoundDbObjects.Count + " Objects" : "";

			lblLoading.Visible = bw.IsBusy;
			txtSearchQuery.Focus();

			if (_queryCache != manager.SearchQuery)
			{
				SearchInBackground();
			}
		}

		private void listDbObjects_SelectedIndexChanged(object sender, EventArgs e)
		{
			gridColumns.DataSource = null;
			txtSqlScript.Text = "";

			var dbObject = listDbObjects.FocusedItem?.Tag as IDbObject<MSSQL>;

			if (dbObject is Table<MSSQL>)
			{
				gridColumns.DataSource = ((Table<MSSQL>)dbObject).Columns?.Tables[0];
				tabControl.SelectedTab = tabControl.TabPages[0];

				GridHighlithRow();
			}
			if (dbObject is View<MSSQL>)
			{
				txtSqlScript.Text = ((View<MSSQL>)dbObject).Script;
				tabControl.SelectedTab = tabControl.TabPages[1];
			}
			if (dbObject is StoredProcedure<MSSQL>)
			{
				txtSqlScript.Text = ((StoredProcedure<MSSQL>)dbObject).Script;
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
				manager = new DatabaseManager<MSSQL>();
				var connRes = manager.Connect(new MSSQLConnectionParams()
				{
					Server = Settings.Default.Server,
					Database = Settings.Default.Database,
					Username = Settings.Default.Username,
					Password = Settings.Default.Password,
					SetIntegratedSecurity = (EIntegratedSecurity)Settings.Default.IntegratedSecurity
				});

				if (connRes.Success && manager.DB.IsConnected)
				{
					lblStatus.Text = $"Connected {manager.DB.Connection.ConnectionString}";
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
			//manager?.Dispose();

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
			manager?.ClearCache();
		}

		private void copyScriptToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (listDbObjects.FocusedItem?.Tag != null)
			{
				var dbObject = listDbObjects.FocusedItem.Tag as DbObject<MSSQL>;

				if (dbObject is View<MSSQL>)
				{
					Clipboard.SetText(((View<MSSQL>)dbObject).Script);
				}
				if (dbObject is StoredProcedure<MSSQL>)
				{
					Clipboard.SetText(((StoredProcedure<MSSQL>)dbObject).Script);
				}
			}
		}

		private void txtSearchQuery_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				SearchInBackground();
				e.Handled = true;
				e.SuppressKeyPress = true;
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
	}
}
