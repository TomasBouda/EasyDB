using Database.Lib;
using Database.Lib.Data;
using Database.Lib.DBMS;
using EasyDB.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
		}

		private void SearchInBackground()
		{
			if (!bw.IsBusy)
			{
				lblLoading.Visible = true;
				listDbObjects.DataSource = null;
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

				if (connDialog.ShowDialog() == DialogResult.OK)
				{
					manager = new DatabaseManager<MSSQL>(connDialog.Server, connDialog.Database, connDialog.Username, connDialog.Password);
					if (manager.DB.IsConnected)
					{
						Settings.Default.Server = connDialog.Server;
						Settings.Default.Database = connDialog.Database;
						Settings.Default.Username = connDialog.Username;
						Settings.Default.Password = connDialog.Password;
						Settings.Default.Save();

						lblStatus.Text = "Connected";

						SearchInBackground();
					}
				}
			}
		}

		private void txtSearchQuery_TextChanged(object sender, EventArgs e)
		{
			_queryCache = txtSearchQuery.Text;
			SearchInBackground();
		}

		private void listDbObjects_SelectedIndexChanged(object sender, EventArgs e)
		{
			var dbObject = listDbObjects.SelectedItem as IDbObject<MSSQL>;

			if(dbObject is Table<MSSQL>)
			{
				gridColumns.DataSource = ((Table<MSSQL>)dbObject).Columns?.Tables[0];
				tabControl.SelectedTab = tabControl.TabPages[0];

				if(txtSearchQuery.Text != "")
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
			if(dbObject is View<MSSQL>)
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

		private void MainForm_Load(object sender, EventArgs e)
		{
			if (Settings.Default.Server != "" && Settings.Default.Database != "")
			{
				manager = new DatabaseManager<MSSQL>(Settings.Default.Server, Settings.Default.Database, Settings.Default.Username, Settings.Default.Password);
				SearchInBackground();
			}
			else
			{
				lblStatus.Text = "Not Connected!";
				lblLoading.Visible = false;
			}
		}

		private void txtSqlScript_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
		{
			e.ChangedRange.SetSyntaxSql();

			e.ChangedRange.ClearStyle(Syntax.Highlight);

			e.ChangedRange.SetStyle(Syntax.Highlight, txtSearchQuery.Text, RegexOptions.IgnoreCase | RegexOptions.Singleline);
		}

		private void bw_DoWork(object sender, DoWorkEventArgs e)
		{
			manager.SearchInDb(_queryCache);
		}

		private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			listDbObjects.DataSource = manager.DbObjects;
			listDbObjects.DisplayMember = "Name";

			lblObjectsCount.Text = manager.DbObjects != null ? manager.DbObjects.Count + " Objects" : "";

			lblLoading.Visible = bw.IsBusy;
			txtSearchQuery.Focus();

			if(_queryCache != manager.SearchQuery)
			{
				SearchInBackground();
			}
		}

		private void setConnectionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SetConnection();
		}
	}
}
