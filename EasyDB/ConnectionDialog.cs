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
	public partial class ConnectionDialog : Form
	{
		public ConnectionDialog()
		{
			InitializeComponent();
		}

		public string Server { get; set; }
		public string Database { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }

		private void btnSave_Click(object sender, EventArgs e)
		{
			Server = txtServer.Text;
			Database = txtDatabase.Text;
			Username = txtUsername.Text;
			Password = txtPassword.Text;

			DialogResult = DialogResult.OK;
			Close();
		}

		private void ConnectionDialog_Load(object sender, EventArgs e)
		{
			txtServer.Text = Server;
			txtDatabase.Text = Database;
			txtUsername.Text = Username;
		}
	}
}
