using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyDB.Forms
{
	public partial class MessageForm : Form
	{
		public string Title { get; set; }
		public string LabelMessage { get; set; }
		public string OkButtonText { get; set; }
		public string ActionButtonText { get; set; }

		public Action ButtonAction { get; set; }

		public MessageForm(string title, string message, Action action, string actionButtonText, string okButtonText)
		{
			InitializeComponent();

			Text = Title = title;
			LabelMessage = lblMessage.Text = message;
			OkButtonText = btnOk.Text = okButtonText;
			ActionButtonText = btnAction.Text = actionButtonText;
			ButtonAction = action;
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}

		private void btnAction_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Yes;
			ButtonAction.Invoke();
			Close();
		}
	}
}
