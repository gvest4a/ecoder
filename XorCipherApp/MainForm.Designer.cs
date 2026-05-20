namespace XorCipherApp
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
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Text = "XOR Шифрование текста";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.Padding = new System.Windows.Forms.Padding(15);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);

            // TabControl
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabEncrypt = new System.Windows.Forms.TabPage();
            this.tabDecrypt = new System.Windows.Forms.TabPage();
            
            this.tabControl.SuspendLayout();
            this.tabEncrypt.SuspendLayout();
            this.tabDecrypt.SuspendLayout();
            this.SuspendLayout();

            // tabControl
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tabControl.ItemSize = new System.Drawing.Size(140, 40);
            this.tabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl.TabPages.AddRange(new System.Windows.Forms.TabPage[] {
                this.tabEncrypt,
                this.tabDecrypt
            });

            // tabEncrypt
            this.tabEncrypt.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.tabEncrypt.AutoScroll = true;
            this.tabEncrypt.Padding = new System.Windows.Forms.Padding(15);
            this.tabEncrypt.Text = "Шифрование";
            this.tabEncrypt.Location = new System.Drawing.Point(4, 44);

            // tabDecrypt
            this.tabDecrypt.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.tabDecrypt.AutoScroll = true;
            this.tabDecrypt.Padding = new System.Windows.Forms.Padding(15);
            this.tabDecrypt.Text = "Расшифрование";
            this.tabDecrypt.Location = new System.Drawing.Point(4, 44);

            // === Encrypt Tab Controls ===
            // leftPanelEncrypt
            this.leftPanelEncrypt = new System.Windows.Forms.Panel();
            this.leftPanelEncrypt.Location = new System.Drawing.Point(15, 15);
            this.leftPanelEncrypt.Size = new System.Drawing.Size(550, 600);
            this.leftPanelEncrypt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.leftPanelEncrypt.Name = "leftPanelEncrypt";

            // rightPanelEncrypt
            this.rightPanelEncrypt = new System.Windows.Forms.Panel();
            this.rightPanelEncrypt.Location = new System.Drawing.Point(600, 15);
            this.rightPanelEncrypt.Size = new System.Drawing.Size(550, 600);
            this.rightPanelEncrypt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rightPanelEncrypt.Name = "rightPanelEncrypt";

            // lblInput (Encrypt)
            this.lblEncryptInput = new System.Windows.Forms.Label();
            this.lblEncryptInput.AutoSize = true;
            this.lblEncryptInput.Location = new System.Drawing.Point(20, 20);
            this.lblEncryptInput.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblEncryptInput.Text = "Исходный текст:";
            this.lblEncryptInput.Name = "lblEncryptInput";

            // txtEncryptInput
            this.txtEncryptInput = new System.Windows.Forms.TextBox();
            this.txtEncryptInput.Location = new System.Drawing.Point(20, 55);
            this.txtEncryptInput.Size = new System.Drawing.Size(510, 150);
            this.txtEncryptInput.Multiline = true;
            this.txtEncryptInput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtEncryptInput.Name = "txtEncryptInput";

            // lblEncryptKey
            this.lblEncryptKey = new System.Windows.Forms.Label();
            this.lblEncryptKey.AutoSize = true;
            this.lblEncryptKey.Location = new System.Drawing.Point(20, 230);
            this.lblEncryptKey.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblEncryptKey.Text = "Ключ шифрования:";
            this.lblEncryptKey.Name = "lblEncryptKey";

            // txtEncryptKey
            this.txtEncryptKey = new System.Windows.Forms.TextBox();
            this.txtEncryptKey.Location = new System.Drawing.Point(20, 260);
            this.txtEncryptKey.Size = new System.Drawing.Size(510, 40);
            this.txtEncryptKey.Name = "txtEncryptKey";

            // btnLoadEncryptFile
            this.btnLoadEncryptFile = new System.Windows.Forms.Button();
            this.btnLoadEncryptFile.Location = new System.Drawing.Point(20, 315);
            this.btnLoadEncryptFile.Size = new System.Drawing.Size(245, 45);
            this.btnLoadEncryptFile.Text = "📁 Загрузить из файла";
            this.btnLoadEncryptFile.Name = "btnLoadEncryptFile";
            this.btnLoadEncryptFile.Click += new System.EventHandler(this.BtnLoadEncryptFile_Click);

            // btnEncrypt
            this.btnEncrypt = new System.Windows.Forms.Button();
            this.btnEncrypt.Location = new System.Drawing.Point(280, 315);
            this.btnEncrypt.Size = new System.Drawing.Size(245, 45);
            this.btnEncrypt.Text = "🔒 Зашифровать";
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Click += new System.EventHandler(this.BtnEncrypt_Click);

            // btnClearEncrypt
            this.btnClearEncrypt = new System.Windows.Forms.Button();
            this.btnClearEncrypt.Location = new System.Drawing.Point(20, 375);
            this.btnClearEncrypt.Size = new System.Drawing.Size(245, 45);
            this.btnClearEncrypt.Text = "🗑️ Очистить всё";
            this.btnClearEncrypt.Name = "btnClearEncrypt";
            this.btnClearEncrypt.Click += new System.EventHandler(this.BtnClearEncrypt_Click);

            // lblEncryptResult
            this.lblEncryptResult = new System.Windows.Forms.Label();
            this.lblEncryptResult.AutoSize = true;
            this.lblEncryptResult.Location = new System.Drawing.Point(20, 20);
            this.lblEncryptResult.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblEncryptResult.Text = "Результат (HEX):";
            this.lblEncryptResult.Name = "lblEncryptResult";

            // txtEncryptResult
            this.txtEncryptResult = new System.Windows.Forms.TextBox();
            this.txtEncryptResult.Location = new System.Drawing.Point(20, 55);
            this.txtEncryptResult.Size = new System.Drawing.Size(510, 480);
            this.txtEncryptResult.Multiline = true;
            this.txtEncryptResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtEncryptResult.ReadOnly = true;
            this.txtEncryptResult.BackColor = System.Drawing.Color.White;
            this.txtEncryptResult.Name = "txtEncryptResult";

            // btnSaveEncryptResult
            this.btnSaveEncryptResult = new System.Windows.Forms.Button();
            this.btnSaveEncryptResult.Location = new System.Drawing.Point(20, 555);
            this.btnSaveEncryptResult.Size = new System.Drawing.Size(510, 50);
            this.btnSaveEncryptResult.Text = "💾 Сохранить результат";
            this.btnSaveEncryptResult.Name = "btnSaveEncryptResult";
            this.btnSaveEncryptResult.Click += new System.EventHandler(this.BtnSaveEncryptResult_Click);

            // === Decrypt Tab Controls ===
            // leftPanelDecrypt
            this.leftPanelDecrypt = new System.Windows.Forms.Panel();
            this.leftPanelDecrypt.Location = new System.Drawing.Point(15, 15);
            this.leftPanelDecrypt.Size = new System.Drawing.Size(550, 600);
            this.leftPanelDecrypt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.leftPanelDecrypt.Name = "leftPanelDecrypt";

            // rightPanelDecrypt
            this.rightPanelDecrypt = new System.Windows.Forms.Panel();
            this.rightPanelDecrypt.Location = new System.Drawing.Point(600, 15);
            this.rightPanelDecrypt.Size = new System.Drawing.Size(550, 600);
            this.rightPanelDecrypt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rightPanelDecrypt.Name = "rightPanelDecrypt";

            // lblDecryptInput
            this.lblDecryptInput = new System.Windows.Forms.Label();
            this.lblDecryptInput.AutoSize = true;
            this.lblDecryptInput.Location = new System.Drawing.Point(20, 20);
            this.lblDecryptInput.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblDecryptInput.Text = "Зашифрованный текст (HEX):";
            this.lblDecryptInput.Name = "lblDecryptInput";

            // txtDecryptInput
            this.txtDecryptInput = new System.Windows.Forms.TextBox();
            this.txtDecryptInput.Location = new System.Drawing.Point(20, 55);
            this.txtDecryptInput.Size = new System.Drawing.Size(510, 150);
            this.txtDecryptInput.Multiline = true;
            this.txtDecryptInput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDecryptInput.Name = "txtDecryptInput";

            // lblDecryptKey
            this.lblDecryptKey = new System.Windows.Forms.Label();
            this.lblDecryptKey.AutoSize = true;
            this.lblDecryptKey.Location = new System.Drawing.Point(20, 230);
            this.lblDecryptKey.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblDecryptKey.Text = "Ключ расшифрования:";
            this.lblDecryptKey.Name = "lblDecryptKey";

            // txtDecryptKey
            this.txtDecryptKey = new System.Windows.Forms.TextBox();
            this.txtDecryptKey.Location = new System.Drawing.Point(20, 260);
            this.txtDecryptKey.Size = new System.Drawing.Size(510, 40);
            this.txtDecryptKey.Name = "txtDecryptKey";

            // btnLoadDecryptFile
            this.btnLoadDecryptFile = new System.Windows.Forms.Button();
            this.btnLoadDecryptFile.Location = new System.Drawing.Point(20, 315);
            this.btnLoadDecryptFile.Size = new System.Drawing.Size(245, 45);
            this.btnLoadDecryptFile.Text = "📁 Загрузить HEX файл";
            this.btnLoadDecryptFile.Name = "btnLoadDecryptFile";
            this.btnLoadDecryptFile.Click += new System.EventHandler(this.BtnLoadDecryptFile_Click);

            // btnDecrypt
            this.btnDecrypt = new System.Windows.Forms.Button();
            this.btnDecrypt.Location = new System.Drawing.Point(280, 315);
            this.btnDecrypt.Size = new System.Drawing.Size(245, 45);
            this.btnDecrypt.Text = "🔓 Расшифровать";
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.Click += new System.EventHandler(this.BtnDecrypt_Click);

            // btnClearDecrypt
            this.btnClearDecrypt = new System.Windows.Forms.Button();
            this.btnClearDecrypt.Location = new System.Drawing.Point(20, 375);
            this.btnClearDecrypt.Size = new System.Drawing.Size(245, 45);
            this.btnClearDecrypt.Text = "🗑️ Очистить всё";
            this.btnClearDecrypt.Name = "btnClearDecrypt";
            this.btnClearDecrypt.Click += new System.EventHandler(this.BtnClearDecrypt_Click);

            // lblDecryptResult
            this.lblDecryptResult = new System.Windows.Forms.Label();
            this.lblDecryptResult.AutoSize = true;
            this.lblDecryptResult.Location = new System.Drawing.Point(20, 20);
            this.lblDecryptResult.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblDecryptResult.Text = "Результат:";
            this.lblDecryptResult.Name = "lblDecryptResult";

            // txtDecryptResult
            this.txtDecryptResult = new System.Windows.Forms.TextBox();
            this.txtDecryptResult.Location = new System.Drawing.Point(20, 55);
            this.txtDecryptResult.Size = new System.Drawing.Size(510, 480);
            this.txtDecryptResult.Multiline = true;
            this.txtDecryptResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDecryptResult.ReadOnly = true;
            this.txtDecryptResult.BackColor = System.Drawing.Color.White;
            this.txtDecryptResult.Name = "txtDecryptResult";

            // btnSaveDecryptResult
            this.btnSaveDecryptResult = new System.Windows.Forms.Button();
            this.btnSaveDecryptResult.Location = new System.Drawing.Point(20, 555);
            this.btnSaveDecryptResult.Size = new System.Drawing.Size(510, 50);
            this.btnSaveDecryptResult.Text = "💾 Сохранить результат";
            this.btnSaveDecryptResult.Name = "btnSaveDecryptResult";
            this.btnSaveDecryptResult.Click += new System.EventHandler(this.BtnSaveDecryptResult_Click);

            // Add controls to panels
            this.leftPanelEncrypt.Controls.Add(this.lblEncryptInput);
            this.leftPanelEncrypt.Controls.Add(this.txtEncryptInput);
            this.leftPanelEncrypt.Controls.Add(this.lblEncryptKey);
            this.leftPanelEncrypt.Controls.Add(this.txtEncryptKey);
            this.leftPanelEncrypt.Controls.Add(this.btnLoadEncryptFile);
            this.leftPanelEncrypt.Controls.Add(this.btnEncrypt);
            this.leftPanelEncrypt.Controls.Add(this.btnClearEncrypt);

            this.rightPanelEncrypt.Controls.Add(this.lblEncryptResult);
            this.rightPanelEncrypt.Controls.Add(this.txtEncryptResult);
            this.rightPanelEncrypt.Controls.Add(this.btnSaveEncryptResult);

            this.leftPanelDecrypt.Controls.Add(this.lblDecryptInput);
            this.leftPanelDecrypt.Controls.Add(this.txtDecryptInput);
            this.leftPanelDecrypt.Controls.Add(this.lblDecryptKey);
            this.leftPanelDecrypt.Controls.Add(this.txtDecryptKey);
            this.leftPanelDecrypt.Controls.Add(this.btnLoadDecryptFile);
            this.leftPanelDecrypt.Controls.Add(this.btnDecrypt);
            this.leftPanelDecrypt.Controls.Add(this.btnClearDecrypt);

            this.rightPanelDecrypt.Controls.Add(this.lblDecryptResult);
            this.rightPanelDecrypt.Controls.Add(this.txtDecryptResult);
            this.rightPanelDecrypt.Controls.Add(this.btnSaveDecryptResult);

            // Add panels to tabs
            this.tabEncrypt.Controls.Add(this.leftPanelEncrypt);
            this.tabEncrypt.Controls.Add(this.rightPanelEncrypt);
            this.tabDecrypt.Controls.Add(this.leftPanelDecrypt);
            this.tabDecrypt.Controls.Add(this.rightPanelDecrypt);

            // Add tabControl to form
            this.Controls.Add(this.tabControl);

            this.tabControl.ResumeLayout(false);
            this.tabEncrypt.ResumeLayout(false);
            this.tabDecrypt.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        #region Fields

        // Main controls
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabEncrypt;
        private System.Windows.Forms.TabPage tabDecrypt;

        // Encrypt Tab - Left Panel
        private System.Windows.Forms.Panel leftPanelEncrypt;
        private System.Windows.Forms.Label lblEncryptInput;
        private System.Windows.Forms.TextBox txtEncryptInput;
        private System.Windows.Forms.Label lblEncryptKey;
        private System.Windows.Forms.TextBox txtEncryptKey;
        private System.Windows.Forms.Button btnLoadEncryptFile;
        private System.Windows.Forms.Button btnEncrypt;
        private System.Windows.Forms.Button btnClearEncrypt;

        // Encrypt Tab - Right Panel
        private System.Windows.Forms.Panel rightPanelEncrypt;
        private System.Windows.Forms.Label lblEncryptResult;
        private System.Windows.Forms.TextBox txtEncryptResult;
        private System.Windows.Forms.Button btnSaveEncryptResult;

        // Decrypt Tab - Left Panel
        private System.Windows.Forms.Panel leftPanelDecrypt;
        private System.Windows.Forms.Label lblDecryptInput;
        private System.Windows.Forms.TextBox txtDecryptInput;
        private System.Windows.Forms.Label lblDecryptKey;
        private System.Windows.Forms.TextBox txtDecryptKey;
        private System.Windows.Forms.Button btnLoadDecryptFile;
        private System.Windows.Forms.Button btnDecrypt;
        private System.Windows.Forms.Button btnClearDecrypt;

        // Decrypt Tab - Right Panel
        private System.Windows.Forms.Panel rightPanelDecrypt;
        private System.Windows.Forms.Label lblDecryptResult;
        private System.Windows.Forms.TextBox txtDecryptResult;
        private System.Windows.Forms.Button btnSaveDecryptResult;

        #endregion
    }
}
