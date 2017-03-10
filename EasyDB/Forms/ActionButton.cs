using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyDB.Forms
{
	public class ActionButton
	{
		public Action Action { get; set; }
		public string Text { get; set; }

		public ActionButton(Action action, string text)
		{
			Action = action;
			Text = text;
		}

		public Button ToFormsButton(int width = -1, int height = -1, int left = -1, int top = -1)
		{
			var btn = new Button();
			btn.Text = Text;
			btn.Click += (object sender, EventArgs e) => { Action.Invoke(); };

			btn.Width = width != -1 ? width : btn.Width;
			btn.Height = height != -1 ? height : btn.Height;
			btn.Left = left != -1 ? left : btn.Left;
			btn.Top = top != -1 ? top : btn.Top;

			return btn;
		}
	}
}
