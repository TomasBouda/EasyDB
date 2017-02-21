namespace EasyDB.Forms
{
	partial class MessageForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageForm));
			this.lblMessage = new System.Windows.Forms.Label();
			this.btnAction = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblMessage
			// 
			this.lblMessage.BackColor = System.Drawing.Color.White;
			this.lblMessage.Location = new System.Drawing.Point(1, 0);
			this.lblMessage.Margin = new System.Windows.Forms.Padding(0);
			this.lblMessage.Name = "lblMessage";
			this.lblMessage.Padding = new System.Windows.Forms.Padding(5);
			this.lblMessage.Size = new System.Drawing.Size(356, 65);
			this.lblMessage.TabIndex = 0;
			this.lblMessage.Text = "label1";
			// 
			// btnAction
			// 
			this.btnAction.Location = new System.Drawing.Point(82, 74);
			this.btnAction.Name = "btnAction";
			this.btnAction.Size = new System.Drawing.Size(75, 33);
			this.btnAction.TabIndex = 1;
			this.btnAction.Text = "Open";
			this.btnAction.UseVisualStyleBackColor = true;
			this.btnAction.Click += new System.EventHandler(this.btnAction_Click);
			// 
			// btnOk
			// 
			this.btnOk.Location = new System.Drawing.Point(199, 74);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 33);
			this.btnOk.TabIndex = 2;
			this.btnOk.Text = "OK";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// MessageForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(357, 117);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.btnAction);
			this.Controls.Add(this.lblMessage);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MessageForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "MessageForm";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label lblMessage;
		private System.Windows.Forms.Button btnAction;
		private System.Windows.Forms.Button btnOk;
	}
}