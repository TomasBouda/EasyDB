namespace EasyDB
{
	partial class QueryOptionsForm
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
			this.lblColumnName = new System.Windows.Forms.Label();
			this.btnFilter = new System.Windows.Forms.Button();
			this.txtExpresion = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// lblColumnName
			// 
			this.lblColumnName.AutoSize = true;
			this.lblColumnName.Location = new System.Drawing.Point(12, 9);
			this.lblColumnName.Name = "lblColumnName";
			this.lblColumnName.Size = new System.Drawing.Size(35, 13);
			this.lblColumnName.TabIndex = 0;
			this.lblColumnName.Text = "label1";
			// 
			// btnFilter
			// 
			this.btnFilter.Location = new System.Drawing.Point(211, 25);
			this.btnFilter.Name = "btnFilter";
			this.btnFilter.Size = new System.Drawing.Size(75, 20);
			this.btnFilter.TabIndex = 1;
			this.btnFilter.Text = "Filter";
			this.btnFilter.UseVisualStyleBackColor = true;
			this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
			// 
			// txtExpresion
			// 
			this.txtExpresion.Location = new System.Drawing.Point(12, 25);
			this.txtExpresion.Name = "txtExpresion";
			this.txtExpresion.Size = new System.Drawing.Size(169, 20);
			this.txtExpresion.TabIndex = 2;
			// 
			// QueryOptionsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(298, 51);
			this.Controls.Add(this.txtExpresion);
			this.Controls.Add(this.btnFilter);
			this.Controls.Add(this.lblColumnName);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "QueryOptionsForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Where";
			this.Load += new System.EventHandler(this.QueryOptionsForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblColumnName;
		private System.Windows.Forms.Button btnFilter;
		private System.Windows.Forms.TextBox txtExpresion;
	}
}