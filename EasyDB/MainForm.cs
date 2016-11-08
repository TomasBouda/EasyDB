using Database.Lib;
using Database.Lib.Data;
using Database.Lib.DBMS;
using Database.Lib.Misc;
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

		#region Private methods
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

			manager.SearchInDb(_queryCache, searchIn);
		}

		private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			listDbObjects.DataSource = manager.FoundDbObjects;
			listDbObjects.DisplayMember = "Name";

			lblObjectsCount.Text = manager.FoundDbObjects != null ? manager.FoundDbObjects.Count + " Objects" : "";

			lblLoading.Visible = bw.IsBusy;
			txtSearchQuery.Focus();

			if (_queryCache != manager.SearchQuery)
			{
				SearchInBackground();
			}
		}

		private void listDbObjects_SelectedIndexChanged(object sender, EventArgs e)
		{
			var dbObject = listDbObjects.SelectedItem as IDbObject<MSSQL>;

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

			InitObjectsList();

			txtSearchQuery.Focus();
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			manager?.Dispose();
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
		
		#endregion
	}
}
