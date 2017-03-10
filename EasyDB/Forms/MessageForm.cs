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
		public string Message { get; set; }
		public string OkButtonText { get; set; }
		public List<ActionButton> ActionButtons { get; set; }

		public MessageForm(string title, string message, string okButtonText, params ActionButton[] actionButtons)
		{
			InitializeComponent();

			Text = Title = title;
			Message = lblMessage.Text = message;
			OkButtonText = btnOk.Text = okButtonText;

			ActionButtons = actionButtons.ToList();

			int left = 0;
			foreach(var btn in ActionButtons)
			{
				Action action = btn.Action;
				btn.Action = () => 
				{
					DialogResult = DialogResult.Yes;
					action();
					Close();
				};

				var fBtn = btn.ToFormsButton(height: panelActionButtons.Height, left: left);
				panelActionButtons.Controls.Add(fBtn);

				left = fBtn.Width + 10;
			}
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}
	}
}
