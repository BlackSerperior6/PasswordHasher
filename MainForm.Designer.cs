namespace PasswordHasher
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            LoginBox = new TextBox();
            PasswordBox = new TextBox();
            Login = new Label();
            label2 = new Label();
            LoginButton = new Button();
            SighinButton = new Button();
            DebugGrid = new DataGridView();
            LoginColumn = new DataGridViewTextBoxColumn();
            Password = new DataGridViewTextBoxColumn();
            Hash = new DataGridViewTextBoxColumn();
            Salt = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)DebugGrid).BeginInit();
            SuspendLayout();
            // 
            // LoginBox
            // 
            LoginBox.Location = new Point(110, 21);
            LoginBox.Name = "LoginBox";
            LoginBox.Size = new Size(113, 23);
            LoginBox.TabIndex = 0;
            // 
            // PasswordBox
            // 
            PasswordBox.Location = new Point(110, 50);
            PasswordBox.Name = "PasswordBox";
            PasswordBox.Size = new Size(113, 23);
            PasswordBox.TabIndex = 1;
            // 
            // Login
            // 
            Login.AutoSize = true;
            Login.Location = new Point(56, 21);
            Login.Name = "Login";
            Login.Size = new Size(41, 15);
            Login.TabIndex = 2;
            Login.Text = "Логин";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(56, 50);
            label2.Name = "label2";
            label2.Size = new Size(49, 15);
            label2.TabIndex = 3;
            label2.Text = "Пароль";
            // 
            // LoginButton
            // 
            LoginButton.Location = new Point(56, 94);
            LoginButton.Name = "LoginButton";
            LoginButton.Size = new Size(75, 23);
            LoginButton.TabIndex = 4;
            LoginButton.Text = "Вход";
            LoginButton.UseVisualStyleBackColor = true;
            LoginButton.Click += LoginButton_Click;
            // 
            // SighinButton
            // 
            SighinButton.Location = new Point(148, 94);
            SighinButton.Name = "SighinButton";
            SighinButton.Size = new Size(89, 23);
            SighinButton.TabIndex = 5;
            SighinButton.Text = "Регистрация";
            SighinButton.UseVisualStyleBackColor = true;
            SighinButton.Click += SighinButton_Click;
            // 
            // DebugGrid
            // 
            DebugGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DebugGrid.Columns.AddRange(new DataGridViewColumn[] { LoginColumn, Password, Hash, Salt });
            DebugGrid.Location = new Point(0, 123);
            DebugGrid.Name = "DebugGrid";
            DebugGrid.Size = new Size(443, 462);
            DebugGrid.TabIndex = 6;
            // 
            // LoginColumn
            // 
            LoginColumn.HeaderText = "Логин";
            LoginColumn.Name = "LoginColumn";
            LoginColumn.ReadOnly = true;
            // 
            // Password
            // 
            Password.HeaderText = "Пароль";
            Password.Name = "Password";
            Password.ReadOnly = true;
            // 
            // Hash
            // 
            Hash.HeaderText = "Хеш";
            Hash.Name = "Hash";
            Hash.ReadOnly = true;
            // 
            // Salt
            // 
            Salt.HeaderText = "Соль";
            Salt.Name = "Salt";
            Salt.ReadOnly = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(443, 580);
            Controls.Add(DebugGrid);
            Controls.Add(SighinButton);
            Controls.Add(LoginButton);
            Controls.Add(label2);
            Controls.Add(Login);
            Controls.Add(PasswordBox);
            Controls.Add(LoginBox);
            Name = "MainForm";
            Text = "Регистрация";
            ((System.ComponentModel.ISupportInitialize)DebugGrid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox LoginBox;
        private TextBox PasswordBox;
        private Label Login;
        private Label label2;
        private Button LoginButton;
        private Button SighinButton;
        private DataGridView DebugGrid;
        private DataGridViewTextBoxColumn LoginColumn;
        private DataGridViewTextBoxColumn Password;
        private DataGridViewTextBoxColumn Hash;
        private DataGridViewTextBoxColumn Salt;
    }
}
