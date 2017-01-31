﻿using Database.Lib;
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
		public DataProvider DataProvider { get; set; } = new DataProvider();

		private string _query = "";

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
					//DataProvider.ActiveProvider = new DatabaseDataProvider.ActiveProvider<MSSQL>();
					//var connRes = DataProvider.ActiveProvider.Connect();

					var connRes = DataProvider.AddMSSqlManager("default", new MSSQLConnectionParams()
					{
						Server = connDialog.Server,
						Database = connDialog.Database,
						Username = connDialog.Username,
						Password = connDialog.Password,
						SetIntegratedSecurity = (EIntegratedSecurity)connDialog.IntegratedSecurity
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

			chListDbObjects.DataSource = new BindingSource(enums, null);
			chListDbObjects.DisplayMember = "Value";
			chListDbObjects.ValueMember = "Key";
		}

		#endregion

		#region Event handlers

		private void txtSearchQuery_TextChanged(object sender, EventArgs e)
		{
			
		}

		private void bw_DoWork(object sender, DoWorkEventArgs e)
		{
			var selected = chListDbObjects.CheckedItems.Cast<KeyValuePair<int, string>>().Select(s => s.Key).Sum();
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

			var dbObject = listDbObjects.FocusedItem?.Tag as IDbObject;

			txtSqlScript.Text = dbObject?.Script ?? "";

			if (DataProvider.ActiveManager.IsTable(dbObject))
			{
				gridColumns.DataSource = DataProvider.ActiveManager.Table(dbObject).Columns?.Tables[0];
				tabControl.SelectedTab = tabControl.TabPages[0];

				GridHighlithRow();
			}
			if (DataProvider.ActiveManager.IsView(dbObject))
			{
				tabControl.SelectedTab = tabControl.TabPages[1];
			}
			if (DataProvider.ActiveManager.IsStoredProcedure(dbObject))
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
					SetIntegratedSecurity = (EIntegratedSecurity)Settings.Default.IntegratedSecurity
				});

				connRes = DataProvider.AddMySqlManager("mysql", new MySqlConnectionParams()
				{
					Server = "127.0.0.1",
					Database = "skype",
					Username = "root",
					Password = null
				});
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
	}
}
