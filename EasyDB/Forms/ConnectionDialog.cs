using System;
using System.Collections.Generic;
using System.Linq;
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
		public bool IntegratedSecurity { get; set; }

		private void btnSave_Click(object sender, EventArgs e)
		{
			Server = txtServer.Text;
			Database = txtDatabase.Text;
			Username = txtUsername.Text;
			Password = txtPassword.Text;
			IntegratedSecurity = (int)(cbIntegratedSec.SelectedValue ?? 1) == 0 ? false : true;

			DialogResult = DialogResult.OK;
			Close();
		}

		private void ConnectionDialog_Load(object sender, EventArgs e)
		{
			txtServer.Text = Server;
			txtDatabase.Text = Database;
			txtUsername.Text = Username;

			var integSec = new Dictionary<int, string>
			{
				{ 0, "false" },
				{ 1, "true" }
			}.ToList();

			cbIntegratedSec.DataSource = integSec;
			cbIntegratedSec.ValueMember = "Key";
			cbIntegratedSec.DisplayMember = "Value";
			
			cbIntegratedSec.SelectedValue = IntegratedSecurity ? 1 : 0;
		}
	}
}
