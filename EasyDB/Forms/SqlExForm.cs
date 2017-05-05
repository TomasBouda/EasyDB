using System;
using System.Data;
using System.Windows.Forms;
using TomLabs.OpenSource.SQuirreL.Search;
using TomLabs.OpenSource.WinForms.Utils.SyntaxHiglithing;

namespace EasyDB.Forms
{
	public partial class SqlExForm : Form
	{
		private string _script;

		public string Script
		{
			get
			{
				return _script;
			}
			set
			{
				_script = value;
				txtSqlScript.Text = value;
			}
		}

		public SqlSyntax Syntax { get; set; }

		public DataProvider DataProvider { get; private set; }

		public SqlExForm(DataProvider dp, SqlSyntax syntax, string script = "")
		{
			DataProvider = dp;
			Syntax = syntax;

			InitializeComponent();
			txtSqlScript.Text = script;
		}

		private string Execute(string script)
		{
			return DataProvider.ActiveManager.ExecuteReader(script).ToString();
		}

		private DataSet ExecuteDataSet(string script)
		{
			return DataProvider.ActiveManager.ExecuteDataSet(script);
		}

		private void txtSqlScript_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
		{
			e.ChangedRange.SetSyntax(Syntax);
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			gridView.DataSource = ExecuteDataSet(txtSqlScript.Text).Tables[0];
		}

		private void executeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			gridView.DataSource = ExecuteDataSet(txtSqlScript.Text).Tables[0];
		}
	}
}
