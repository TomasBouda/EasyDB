using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyDB
{
	public partial class AboutDialog : Form
	{
		public AboutDialog()
		{
			InitializeComponent();
		}

		private void AboutDialog_Load(object sender, EventArgs e)
		{
			lblCopyRight.Text = $"{DateTime.Now.Year} © Tomáš Bouda";
		}

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start("https://github.com/TomasBouda/EasyDB");
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
