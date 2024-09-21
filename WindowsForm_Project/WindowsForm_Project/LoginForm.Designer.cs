namespace WindowsForm_Project
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.metroPanel2 = new MetroFramework.Controls.MetroPanel();
            this.errormess = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.btnsubmit = new Guna.UI2.WinForms.Guna2Button();
            this.textpassword = new Guna.UI2.WinForms.Guna2TextBox();
            this.textusername = new Guna.UI2.WinForms.Guna2TextBox();
            this.metroPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroPanel2
            // 
            this.metroPanel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("metroPanel2.BackgroundImage")));
            this.metroPanel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.metroPanel2.Controls.Add(this.errormess);
            this.metroPanel2.Controls.Add(this.guna2HtmlLabel1);
            this.metroPanel2.Controls.Add(this.btnsubmit);
            this.metroPanel2.Controls.Add(this.textpassword);
            this.metroPanel2.Controls.Add(this.textusername);
            this.metroPanel2.HorizontalScrollbarBarColor = true;
            this.metroPanel2.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel2.HorizontalScrollbarSize = 10;
            this.metroPanel2.Location = new System.Drawing.Point(1083, 239);
            this.metroPanel2.Name = "metroPanel2";
            this.metroPanel2.Size = new System.Drawing.Size(694, 534);
            this.metroPanel2.TabIndex = 5;
            this.metroPanel2.VerticalScrollbarBarColor = true;
            this.metroPanel2.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel2.VerticalScrollbarSize = 10;
            this.metroPanel2.Paint += new System.Windows.Forms.PaintEventHandler(this.metroPanel2_Paint);
            // 
            // errormess
            // 
            this.errormess.BackColor = System.Drawing.Color.Transparent;
            this.errormess.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errormess.ForeColor = System.Drawing.Color.Red;
            this.errormess.Location = new System.Drawing.Point(186, 454);
            this.errormess.Name = "errormess";
            this.errormess.Size = new System.Drawing.Size(352, 26);
            this.errormess.TabIndex = 8;
            this.errormess.Text = "Username or Password was wrong !!!";
            this.errormess.Visible = false;
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("Comic Sans MS", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(286, 3);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(151, 69);
            this.guna2HtmlLabel1.TabIndex = 7;
            this.guna2HtmlLabel1.Text = "Log In";
            // 
            // btnsubmit
            // 
            this.btnsubmit.BackColor = System.Drawing.Color.Transparent;
            this.btnsubmit.BorderRadius = 20;
            this.btnsubmit.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnsubmit.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnsubmit.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnsubmit.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnsubmit.FillColor = System.Drawing.Color.Thistle;
            this.btnsubmit.Font = new System.Drawing.Font("Comic Sans MS", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsubmit.ForeColor = System.Drawing.Color.MediumPurple;
            this.btnsubmit.Location = new System.Drawing.Point(218, 356);
            this.btnsubmit.Name = "btnsubmit";
            this.btnsubmit.PressedColor = System.Drawing.Color.Orchid;
            this.btnsubmit.Size = new System.Drawing.Size(284, 59);
            this.btnsubmit.TabIndex = 6;
            this.btnsubmit.Text = "Submit";
            this.btnsubmit.Click += new System.EventHandler(this.guna2Button1_Click);
            // 
            // textpassword
            // 
            this.textpassword.BackColor = System.Drawing.Color.Transparent;
            this.textpassword.BorderColor = System.Drawing.SystemColors.ButtonFace;
            this.textpassword.BorderRadius = 20;
            this.textpassword.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textpassword.DefaultText = "";
            this.textpassword.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.textpassword.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.textpassword.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textpassword.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textpassword.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textpassword.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textpassword.ForeColor = System.Drawing.Color.Black;
            this.textpassword.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textpassword.IconLeftSize = new System.Drawing.Size(30, 30);
            this.textpassword.Location = new System.Drawing.Point(99, 244);
            this.textpassword.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.textpassword.Name = "textpassword";
            this.textpassword.PasswordChar = '*';
            this.textpassword.PlaceholderForeColor = System.Drawing.Color.DarkGray;
            this.textpassword.PlaceholderText = "Password";
            this.textpassword.SelectedText = "";
            this.textpassword.Size = new System.Drawing.Size(507, 58);
            this.textpassword.TabIndex = 5;
            this.textpassword.TextChanged += new System.EventHandler(this.textpassword_TextChanged);
            // 
            // textusername
            // 
            this.textusername.BackColor = System.Drawing.Color.Transparent;
            this.textusername.BorderColor = System.Drawing.SystemColors.ButtonFace;
            this.textusername.BorderRadius = 20;
            this.textusername.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textusername.DefaultText = "";
            this.textusername.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.textusername.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.textusername.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textusername.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textusername.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textusername.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textusername.ForeColor = System.Drawing.Color.Black;
            this.textusername.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textusername.IconLeftSize = new System.Drawing.Size(30, 30);
            this.textusername.Location = new System.Drawing.Point(99, 132);
            this.textusername.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.textusername.Name = "textusername";
            this.textusername.PasswordChar = '\0';
            this.textusername.PlaceholderForeColor = System.Drawing.Color.DarkGray;
            this.textusername.PlaceholderText = "User Name";
            this.textusername.SelectedText = "";
            this.textusername.Size = new System.Drawing.Size(507, 58);
            this.textusername.TabIndex = 4;
            this.textusername.TextChanged += new System.EventHandler(this.guna2TextBox1_TextChanged);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 1061);
            this.Controls.Add(this.metroPanel2);
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.metroPanel2.ResumeLayout(false);
            this.metroPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Controls.MetroPanel metroPanel2;
        private Guna.UI2.WinForms.Guna2TextBox textusername;
        private Guna.UI2.WinForms.Guna2TextBox textpassword;
        private Guna.UI2.WinForms.Guna2Button btnsubmit;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel errormess;
    }
}

