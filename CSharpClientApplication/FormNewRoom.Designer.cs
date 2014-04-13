namespace CSharpClientApplication
{
    partial class FormNewRoom
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
            this.labelChatRoomName = new System.Windows.Forms.Label();
            this.textBoxChatRoomName = new System.Windows.Forms.TextBox();
            this.buttonCreateChatRoom = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelChatRoomName
            // 
            this.labelChatRoomName.AutoSize = true;
            this.labelChatRoomName.Location = new System.Drawing.Point(12, 30);
            this.labelChatRoomName.Name = "labelChatRoomName";
            this.labelChatRoomName.Size = new System.Drawing.Size(95, 13);
            this.labelChatRoomName.TabIndex = 0;
            this.labelChatRoomName.Text = "Chat Room name :";
            // 
            // textBoxChatRoomName
            // 
            this.textBoxChatRoomName.Location = new System.Drawing.Point(110, 27);
            this.textBoxChatRoomName.MaxLength = 30;
            this.textBoxChatRoomName.Name = "textBoxChatRoomName";
            this.textBoxChatRoomName.Size = new System.Drawing.Size(161, 20);
            this.textBoxChatRoomName.TabIndex = 1;
            // 
            // buttonCreateChatRoom
            // 
            this.buttonCreateChatRoom.Location = new System.Drawing.Point(196, 69);
            this.buttonCreateChatRoom.Name = "buttonCreateChatRoom";
            this.buttonCreateChatRoom.Size = new System.Drawing.Size(76, 23);
            this.buttonCreateChatRoom.TabIndex = 2;
            this.buttonCreateChatRoom.Text = "OK";
            this.buttonCreateChatRoom.UseVisualStyleBackColor = true;
            this.buttonCreateChatRoom.Click += new System.EventHandler(this.buttonCreateChatRoom_Click);
            // 
            // FormNewRoom
            // 
            this.AcceptButton = this.buttonCreateChatRoom;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 104);
            this.Controls.Add(this.buttonCreateChatRoom);
            this.Controls.Add(this.textBoxChatRoomName);
            this.Controls.Add(this.labelChatRoomName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormNewRoom";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create a new Chat Room";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelChatRoomName;
        private System.Windows.Forms.TextBox textBoxChatRoomName;
        private System.Windows.Forms.Button buttonCreateChatRoom;
    }
}