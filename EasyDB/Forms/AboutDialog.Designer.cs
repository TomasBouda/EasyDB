namespace EasyDB
{
	partial class AboutDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutDialog));
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.lblCopyRight = new System.Windows.Forms.Label();
			this.btnOk = new System.Windows.Forms.Button();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.label1.Location = new System.Drawing.Point(97, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(82, 24);
			this.label1.TabIndex = 0;
			this.label1.Text = "EasyDB";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.Location = new System.Drawing.Point(174, 23);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(29, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Beta";
			// 
			// lblCopyRight
			// 
			this.lblCopyRight.AutoSize = true;
			this.lblCopyRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.lblCopyRight.Location = new System.Drawing.Point(82, 80);
			this.lblCopyRight.Name = "lblCopyRight";
			this.lblCopyRight.Size = new System.Drawing.Size(0, 16);
			this.lblCopyRight.TabIndex = 2;
			// 
			// btnOk
			// 
			this.btnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.btnOk.Location = new System.Drawing.Point(101, 111);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(94, 29);
			this.btnOk.TabIndex = 3;
			this.btnOk.Text = "OK";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// linkLabel1
			// 
			this.linkLabel1.AutoSize = true;
			this.linkLabel1.Location = new System.Drawing.Point(104, 51);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(83, 13);
			this.linkLabel1.TabIndex = 4;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "GitHub/EasyDB";
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// AboutDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(297, 152);
			this.ControlBox = false;
			this.Controls.Add(this.linkLabel1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.lblCopyRight);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "AboutDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "EasyDB - About";
			this.Load += new System.EventHandler(this.AboutDialog_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label lblCopyRight;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.LinkLabel linkLabel1;
	}
}