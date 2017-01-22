﻿namespace EasyDB
{
	partial class MainForm
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.gridColumns = new System.Windows.Forms.DataGridView();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.txtSqlScript = new FastColoredTextBoxNS.FastColoredTextBox();
			this.listDbObjects = new System.Windows.Forms.ListView();
			this.cmsListObj = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.copyScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.txtSearchQuery = new System.Windows.Forms.TextBox();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.lblLoading = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
			this.lblObjectsCount = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.bw = new System.ComponentModel.BackgroundWorker();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.setConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.clearCacheToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.reportBugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.panel1 = new System.Windows.Forms.Panel();
			this.chListDbObjects = new System.Windows.Forms.CheckedListBox();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.btnSearch = new System.Windows.Forms.Button();
			this.tableLayoutPanel1.SuspendLayout();
			this.tabControl.SuspendLayout();
			this.tabPage1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridColumns)).BeginInit();
			this.tabPage2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtSqlScript)).BeginInit();
			this.cmsListObj.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.tabControl, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.listDbObjects, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 76);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(872, 323);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.tabPage1);
			this.tabControl.Controls.Add(this.tabPage2);
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.Location = new System.Drawing.Point(223, 3);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(646, 317);
			this.tabControl.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.gridColumns);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(638, 291);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Columns";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// gridColumns
			// 
			this.gridColumns.AllowUserToAddRows = false;
			this.gridColumns.AllowUserToDeleteRows = false;
			this.gridColumns.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
			this.gridColumns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridColumns.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridColumns.Location = new System.Drawing.Point(3, 3);
			this.gridColumns.Name = "gridColumns";
			this.gridColumns.ReadOnly = true;
			this.gridColumns.Size = new System.Drawing.Size(632, 285);
			this.gridColumns.TabIndex = 1;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.txtSqlScript);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(638, 291);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Sql";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// txtSqlScript
			// 
			this.txtSqlScript.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
			this.txtSqlScript.AutoScrollMinSize = new System.Drawing.Size(2, 17);
			this.txtSqlScript.BackBrush = null;
			this.txtSqlScript.CharHeight = 17;
			this.txtSqlScript.CharWidth = 8;
			this.txtSqlScript.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtSqlScript.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
			this.txtSqlScript.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtSqlScript.Font = new System.Drawing.Font("Consolas", 11.25F);
			this.txtSqlScript.IsReplaceMode = false;
			this.txtSqlScript.Location = new System.Drawing.Point(3, 3);
			this.txtSqlScript.Name = "txtSqlScript";
			this.txtSqlScript.Paddings = new System.Windows.Forms.Padding(0);
			this.txtSqlScript.ReadOnly = true;
			this.txtSqlScript.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
			this.txtSqlScript.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txtSqlScript.ServiceColors")));
			this.txtSqlScript.Size = new System.Drawing.Size(632, 285);
			this.txtSqlScript.TabIndex = 0;
			this.txtSqlScript.Zoom = 100;
			this.txtSqlScript.TextChanged += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.txtSqlScript_TextChanged);
			// 
			// listDbObjects
			// 
			this.listDbObjects.ContextMenuStrip = this.cmsListObj;
			this.listDbObjects.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listDbObjects.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.listDbObjects.Location = new System.Drawing.Point(3, 3);
			this.listDbObjects.MultiSelect = false;
			this.listDbObjects.Name = "listDbObjects";
			this.listDbObjects.Size = new System.Drawing.Size(214, 317);
			this.listDbObjects.SmallImageList = this.imageList;
			this.listDbObjects.TabIndex = 1;
			this.listDbObjects.UseCompatibleStateImageBehavior = false;
			this.listDbObjects.View = System.Windows.Forms.View.SmallIcon;
			this.listDbObjects.SelectedIndexChanged += new System.EventHandler(this.listDbObjects_SelectedIndexChanged);
			// 
			// cmsListObj
			// 
			this.cmsListObj.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyScriptToolStripMenuItem});
			this.cmsListObj.Name = "cmsListObj";
			this.cmsListObj.Size = new System.Drawing.Size(135, 26);
			// 
			// copyScriptToolStripMenuItem
			// 
			this.copyScriptToolStripMenuItem.Name = "copyScriptToolStripMenuItem";
			this.copyScriptToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
			this.copyScriptToolStripMenuItem.Text = "Copy script";
			this.copyScriptToolStripMenuItem.Click += new System.EventHandler(this.copyScriptToolStripMenuItem_Click);
			// 
			// imageList
			// 
			this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList.Images.SetKeyName(0, "table.png");
			this.imageList.Images.SetKeyName(1, "view.png");
			this.imageList.Images.SetKeyName(2, "sp.png");
			// 
			// txtSearchQuery
			// 
			this.txtSearchQuery.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtSearchQuery.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.txtSearchQuery.Location = new System.Drawing.Point(3, 3);
			this.txtSearchQuery.Name = "txtSearchQuery";
			this.txtSearchQuery.Size = new System.Drawing.Size(831, 26);
			this.txtSearchQuery.TabIndex = 0;
			this.txtSearchQuery.TextChanged += new System.EventHandler(this.txtSearchQuery_TextChanged);
			this.txtSearchQuery.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSearchQuery_KeyUp);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblLoading,
            this.toolStripProgressBar1,
            this.lblObjectsCount,
            this.toolStripStatusLabel1,
            this.lblStatus});
			this.statusStrip1.Location = new System.Drawing.Point(0, 399);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(872, 22);
			this.statusStrip1.TabIndex = 1;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// lblLoading
			// 
			this.lblLoading.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.lblLoading.Image = global::EasyDB.Properties.Resources.loading;
			this.lblLoading.Name = "lblLoading";
			this.lblLoading.Size = new System.Drawing.Size(16, 17);
			// 
			// toolStripProgressBar1
			// 
			this.toolStripProgressBar1.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
			this.toolStripProgressBar1.Name = "toolStripProgressBar1";
			this.toolStripProgressBar1.Size = new System.Drawing.Size(150, 16);
			this.toolStripProgressBar1.Visible = false;
			// 
			// lblObjectsCount
			// 
			this.lblObjectsCount.Name = "lblObjectsCount";
			this.lblObjectsCount.Size = new System.Drawing.Size(0, 17);
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(10, 17);
			this.toolStripStatusLabel1.Text = "|";
			// 
			// lblStatus
			// 
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(0, 17);
			// 
			// bw
			// 
			this.bw.WorkerSupportsCancellation = true;
			this.bw.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bw_DoWork);
			this.bw.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bw_RunWorkerCompleted);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(872, 24);
			this.menuStrip1.TabIndex = 2;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// optionsToolStripMenuItem
			// 
			this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setConnectionToolStripMenuItem,
            this.clearCacheToolStripMenuItem});
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
			this.optionsToolStripMenuItem.Text = "Options";
			// 
			// setConnectionToolStripMenuItem
			// 
			this.setConnectionToolStripMenuItem.Name = "setConnectionToolStripMenuItem";
			this.setConnectionToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
			this.setConnectionToolStripMenuItem.Text = "Set connection";
			this.setConnectionToolStripMenuItem.Click += new System.EventHandler(this.setConnectionToolStripMenuItem_Click);
			// 
			// clearCacheToolStripMenuItem
			// 
			this.clearCacheToolStripMenuItem.Name = "clearCacheToolStripMenuItem";
			this.clearCacheToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
			this.clearCacheToolStripMenuItem.Text = "Clear cache";
			this.clearCacheToolStripMenuItem.Click += new System.EventHandler(this.clearCacheToolStripMenuItem_Click);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reportBugToolStripMenuItem,
            this.aboutToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.helpToolStripMenuItem.Text = "Help";
			// 
			// reportBugToolStripMenuItem
			// 
			this.reportBugToolStripMenuItem.Name = "reportBugToolStripMenuItem";
			this.reportBugToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
			this.reportBugToolStripMenuItem.Text = "Report bug";
			this.reportBugToolStripMenuItem.Click += new System.EventHandler(this.reportBugToolStripMenuItem_Click);
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
			this.aboutToolStripMenuItem.Text = "About";
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.chListDbObjects);
			this.panel1.Controls.Add(this.tableLayoutPanel2);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 24);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(872, 52);
			this.panel1.TabIndex = 3;
			// 
			// chListDbObjects
			// 
			this.chListDbObjects.BackColor = System.Drawing.SystemColors.Control;
			this.chListDbObjects.Dock = System.Windows.Forms.DockStyle.Top;
			this.chListDbObjects.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.chListDbObjects.FormattingEnabled = true;
			this.chListDbObjects.HorizontalScrollbar = true;
			this.chListDbObjects.Location = new System.Drawing.Point(0, 31);
			this.chListDbObjects.MultiColumn = true;
			this.chListDbObjects.Name = "chListDbObjects";
			this.chListDbObjects.Size = new System.Drawing.Size(872, 19);
			this.chListDbObjects.TabIndex = 1;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 2;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
			this.tableLayoutPanel2.Controls.Add(this.btnSearch, 1, 0);
			this.tableLayoutPanel2.Controls.Add(this.txtSearchQuery, 0, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(872, 31);
			this.tableLayoutPanel2.TabIndex = 2;
			// 
			// btnSearch
			// 
			this.btnSearch.BackgroundImage = global::EasyDB.Properties.Resources.search_icon;
			this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btnSearch.Location = new System.Drawing.Point(842, 4);
			this.btnSearch.Margin = new System.Windows.Forms.Padding(5, 4, 3, 3);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Size = new System.Drawing.Size(25, 24);
			this.btnSearch.TabIndex = 1;
			this.btnSearch.UseVisualStyleBackColor = true;
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(872, 421);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.menuStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "EasyDB";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tabControl.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridColumns)).EndInit();
			this.tabPage2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.txtSqlScript)).EndInit();
			this.cmsListObj.ResumeLayout(false);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TextBox txtSearchQuery;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.DataGridView gridColumns;
		private System.Windows.Forms.TabPage tabPage2;
		private FastColoredTextBoxNS.FastColoredTextBox txtSqlScript;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel lblObjectsCount;
		private System.ComponentModel.BackgroundWorker bw;
		private System.Windows.Forms.ToolStripStatusLabel lblLoading;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem setConnectionToolStripMenuItem;
		private System.Windows.Forms.ToolStripStatusLabel lblStatus;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.CheckedListBox chListDbObjects;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Button btnSearch;
		private System.Windows.Forms.ToolStripMenuItem clearCacheToolStripMenuItem;
		private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
		private System.Windows.Forms.ContextMenuStrip cmsListObj;
		private System.Windows.Forms.ToolStripMenuItem copyScriptToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem reportBugToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ListView listDbObjects;
		private System.Windows.Forms.ImageList imageList;
	}
}

