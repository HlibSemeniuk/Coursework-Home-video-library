namespace Database_Test
{
    partial class Form1
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
            this.button_ActorMenu = new System.Windows.Forms.Button();
            this.button_Directors = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_ActorMenu
            // 
            this.button_ActorMenu.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.button_ActorMenu.Location = new System.Drawing.Point(7, 12);
            this.button_ActorMenu.Name = "button_ActorMenu";
            this.button_ActorMenu.Size = new System.Drawing.Size(75, 23);
            this.button_ActorMenu.TabIndex = 0;
            this.button_ActorMenu.Text = "Актори";
            this.button_ActorMenu.UseVisualStyleBackColor = true;
            this.button_ActorMenu.Click += new System.EventHandler(this.button1_Click);
            // 
            // button_Directors
            // 
            this.button_Directors.Location = new System.Drawing.Point(169, 12);
            this.button_Directors.Name = "button_Directors";
            this.button_Directors.Size = new System.Drawing.Size(75, 23);
            this.button_Directors.TabIndex = 1;
            this.button_Directors.Text = "Режисери";
            this.button_Directors.UseVisualStyleBackColor = true;
            this.button_Directors.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(88, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Фільми";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(250, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(96, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Жанри фільмів";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 42);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button_Directors);
            this.Controls.Add(this.button_ActorMenu);
            this.Name = "Form1";
            this.Text = "Домашня відеотека";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_ActorMenu;
        private System.Windows.Forms.Button button_Directors;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}

