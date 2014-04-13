namespace CSharpClientApplication
{
    partial class FormMain
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
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Chat Rooms");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Connected Users");
            this.splitContainerFormMain = new System.Windows.Forms.SplitContainer();
            this.treeViewChatRooms = new System.Windows.Forms.TreeView();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newChatRoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parametersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serverIPAddressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.treeViewClients = new System.Windows.Forms.TreeView();
            this.rightClickChatRoomMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerFormMain)).BeginInit();
            this.splitContainerFormMain.Panel1.SuspendLayout();
            this.splitContainerFormMain.Panel2.SuspendLayout();
            this.splitContainerFormMain.SuspendLayout();
            this.mainMenuStrip.SuspendLayout();
            this.rightClickChatRoomMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerFormMain
            // 
            this.splitContainerFormMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerFormMain.IsSplitterFixed = true;
            this.splitContainerFormMain.Location = new System.Drawing.Point(0, 0);
            this.splitContainerFormMain.Name = "splitContainerFormMain";
            this.splitContainerFormMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerFormMain.Panel1
            // 
            this.splitContainerFormMain.Panel1.Controls.Add(this.treeViewChatRooms);
            this.splitContainerFormMain.Panel1.Controls.Add(this.mainMenuStrip);
            this.splitContainerFormMain.Panel1MinSize = 279;
            // 
            // splitContainerFormMain.Panel2
            // 
            this.splitContainerFormMain.Panel2.Controls.Add(this.treeViewClients);
            this.splitContainerFormMain.Panel2MinSize = 279;
            this.splitContainerFormMain.Size = new System.Drawing.Size(244, 562);
            this.splitContainerFormMain.SplitterDistance = 279;
            this.splitContainerFormMain.TabIndex = 0;
            // 
            // treeViewChatRooms
            // 
            this.treeViewChatRooms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewChatRooms.HideSelection = false;
            this.treeViewChatRooms.Location = new System.Drawing.Point(0, 24);
            this.treeViewChatRooms.Name = "treeViewChatRooms";
            treeNode3.Name = "Root";
            treeNode3.Text = "Chat Rooms";
            this.treeViewChatRooms.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3});
            this.treeViewChatRooms.Size = new System.Drawing.Size(244, 255);
            this.treeViewChatRooms.TabIndex = 0;
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(244, 24);
            this.mainMenuStrip.TabIndex = 1;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToolStripMenuItem,
            this.newChatRoomToolStripMenuItem,
            this.parametersToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.connectToolStripMenuItem.Text = "Connect";
            this.connectToolStripMenuItem.Click += new System.EventHandler(this.connectToolStripMenuItem_Click);
            // 
            // newChatRoomToolStripMenuItem
            // 
            this.newChatRoomToolStripMenuItem.Name = "newChatRoomToolStripMenuItem";
            this.newChatRoomToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.newChatRoomToolStripMenuItem.Text = "New Chat Room";
            this.newChatRoomToolStripMenuItem.Click += new System.EventHandler(this.newChatRoomToolStripMenuItem_Click);
            // 
            // parametersToolStripMenuItem
            // 
            this.parametersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serverIPAddressToolStripMenuItem});
            this.parametersToolStripMenuItem.Name = "parametersToolStripMenuItem";
            this.parametersToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.parametersToolStripMenuItem.Text = "Parameters";
            // 
            // serverIPAddressToolStripMenuItem
            // 
            this.serverIPAddressToolStripMenuItem.Name = "serverIPAddressToolStripMenuItem";
            this.serverIPAddressToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.serverIPAddressToolStripMenuItem.Text = "Server IP Address";
            this.serverIPAddressToolStripMenuItem.Click += new System.EventHandler(this.serverIPAddressToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // treeViewClients
            // 
            this.treeViewClients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewClients.Location = new System.Drawing.Point(0, 0);
            this.treeViewClients.Name = "treeViewClients";
            treeNode4.Name = "Root";
            treeNode4.Text = "Connected Users";
            this.treeViewClients.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4});
            this.treeViewClients.Size = new System.Drawing.Size(244, 279);
            this.treeViewClients.TabIndex = 0;
            // 
            // rightClickChatRoomMenuStrip
            // 
            this.rightClickChatRoomMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem});
            this.rightClickChatRoomMenuStrip.Name = "rightClickChatRoomMenuStrip";
            this.rightClickChatRoomMenuStrip.Size = new System.Drawing.Size(108, 26);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(244, 562);
            this.Controls.Add(this.splitContainerFormMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.mainMenuStrip;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.Text = "Chat Application";
            this.splitContainerFormMain.Panel1.ResumeLayout(false);
            this.splitContainerFormMain.Panel1.PerformLayout();
            this.splitContainerFormMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerFormMain)).EndInit();
            this.splitContainerFormMain.ResumeLayout(false);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.rightClickChatRoomMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerFormMain;
        private System.Windows.Forms.TreeView treeViewChatRooms;
        private System.Windows.Forms.TreeView treeViewClients;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newChatRoomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem parametersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serverIPAddressToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip rightClickChatRoomMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;

    }
}