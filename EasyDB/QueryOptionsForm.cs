using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyDB
{
	public partial class QueryOptionsForm : Form
	{
		public QueryOptionsForm()
		{
			InitializeComponent();
		}

		public string ColumnName { get; set; }
		public string Expresion { get; set; }

		private void QueryOptionsForm_Load(object sender, EventArgs e)
		{
			lblColumnName.Text = ColumnName;
		}

		private void btnFilter_Click(object sender, EventArgs e)
		{
			Expresion = " " + txtExpresion.Text;

			DialogResult = DialogResult.OK;
			Close();
		}
	}
}
