namespace CSharpClientApplication
{
    partial class FormChatRoom
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
            this.chatRoomMenuStrip = new System.Windows.Forms.MenuStrip();
            this.mainPanelContainer = new System.Windows.Forms.Panel();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.listClient = new System.Windows.Forms.ListView();
            this.columnConnectedUsers = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonPrivateChat = new System.Windows.Forms.Button();
            this.labelMessage = new System.Windows.Forms.Label();
            this.buttonSend = new System.Windows.Forms.Button();
            this.textBoxMessage = new System.Windows.Forms.TextBox();
            this.richTextBoxChat = new System.Windows.Forms.RichTextBox();
            this.mainPanelContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // chatRoomMenuStrip
            // 
            this.chatRoomMenuStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.chatRoomMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.chatRoomMenuStrip.Name = "chatRoomMenuStrip";
            this.chatRoomMenuStrip.Size = new System.Drawing.Size(572, 24);
            this.chatRoomMenuStrip.TabIndex = 0;
            this.chatRoomMenuStrip.Text = "mainMenuStrip";
            // 
            // mainPanelContainer
            // 
            this.mainPanelContainer.Controls.Add(this.splitContainer);
            this.mainPanelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanelContainer.Location = new System.Drawing.Point(0, 24);
            this.mainPanelContainer.Name = "mainPanelContainer";
            this.mainPanelContainer.Size = new System.Drawing.Size(572, 280);
            this.mainPanelContainer.TabIndex = 1;
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer.IsSplitterFixed = true;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.listClient);
            this.splitContainer.Panel1.Controls.Add(this.buttonPrivateChat);
            this.splitContainer.Panel1MinSize = 150;
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.labelMessage);
            this.splitContainer.Panel2.Controls.Add(this.buttonSend);
            this.splitContainer.Panel2.Controls.Add(this.textBoxMessage);
            this.splitContainer.Panel2.Controls.Add(this.richTextBoxChat);
            this.splitContainer.Size = new System.Drawing.Size(572, 280);
            this.splitContainer.SplitterDistance = 200;
            this.splitContainer.TabIndex = 0;
            // 
            // listClient
            // 
            this.listClient.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listClient.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnConnectedUsers});
            this.listClient.FullRowSelect = true;
            this.listClient.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listClient.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.listClient.Location = new System.Drawing.Point(4, 4);
            this.listClient.MultiSelect = false;
            this.listClient.Name = "listClient";
            this.listClient.Size = new System.Drawing.Size(197, 227);
            this.listClient.TabIndex = 2;
            this.listClient.UseCompatibleStateImageBehavior = false;
            this.listClient.View = System.Windows.Forms.View.Details;
            this.listClient.SelectedIndexChanged += new System.EventHandler(this.listClient_SelectedIndexChanged);
            // 
            // columnConnectedUsers
            // 
            this.columnConnectedUsers.Width = 190;
            // 
            // buttonPrivateChat
            // 
            this.buttonPrivateChat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPrivateChat.Location = new System.Drawing.Point(3, 237);
            this.buttonPrivateChat.Name = "buttonPrivateChat";
            this.buttonPrivateChat.Size = new System.Drawing.Size(194, 40);
            this.buttonPrivateChat.TabIndex = 1;
            this.buttonPrivateChat.Text = "Private Chat";
            this.buttonPrivateChat.UseVisualStyleBackColor = true;
            this.buttonPrivateChat.Click += new System.EventHandler(this.buttonPrivateChat_Click);
            // 
            // labelMessage
            // 
            this.labelMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelMessage.AutoSize = true;
            this.labelMessage.Location = new System.Drawing.Point(14, 251);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(56, 13);
            this.labelMessage.TabIndex = 3;
            this.labelMessage.Text = "Message :";
            // 
            // buttonSend
            // 
            this.buttonSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSend.Enabled = false;
            this.buttonSend.Location = new System.Drawing.Point(290, 237);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(75, 40);
            this.buttonSend.TabIndex = 2;
            this.buttonSend.Text = "Send";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // textBoxMessage
            // 
            this.textBoxMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxMessage.HideSelection = false;
            this.textBoxMessage.Location = new System.Drawing.Point(81, 237);
            this.textBoxMessage.MaxLength = 3000;
            this.textBoxMessage.Multiline = true;
            this.textBoxMessage.Name = "textBoxMessage";
            this.textBoxMessage.Size = new System.Drawing.Size(203, 40);
            this.textBoxMessage.TabIndex = 1;
            this.textBoxMessage.TextChanged += new System.EventHandler(this.textBoxMessage_TextChanged);
            this.textBoxMessage.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBoxMessage_DragDrop);
            this.textBoxMessage.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBoxMessage_DragEnter);
            // 
            // richTextBoxChat
            // 
            this.richTextBoxChat.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxChat.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.richTextBoxChat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBoxChat.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxChat.Name = "richTextBoxChat";
            this.richTextBoxChat.ReadOnly = true;
            this.richTextBoxChat.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBoxChat.Size = new System.Drawing.Size(362, 228);
            this.richTextBoxChat.TabIndex = 0;
            this.richTextBoxChat.Text = "";
            // 
            // FormChatRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 304);
            this.Controls.Add(this.mainPanelContainer);
            this.Controls.Add(this.chatRoomMenuStrip);
            this.MainMenuStrip = this.chatRoomMenuStrip;
            this.MaximumSize = new System.Drawing.Size(1920, 1080);
            this.MinimumSize = new System.Drawing.Size(588, 342);
            this.Name = "FormChatRoom";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chat Room";
            this.mainPanelContainer.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip chatRoomMenuStrip;
        private System.Windows.Forms.Panel mainPanelContainer;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.TextBox textBoxMessage;
        private System.Windows.Forms.RichTextBox richTextBoxChat;
        private System.Windows.Forms.Label labelMessage;
        private System.Windows.Forms.Button buttonPrivateChat;
        private System.Windows.Forms.ListView listClient;
        private System.Windows.Forms.ColumnHeader columnConnectedUsers;
    }
}