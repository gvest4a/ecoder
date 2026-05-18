using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace XorCipherApp
{
    public partial class MainForm : Form
    {
        private TabControl tabControl = null!;
        private TabPage tabEncrypt = null!;
        private TabPage tabDecrypt = null!;
        
        // Encrypt controls
        private TextBox txtEncryptInput = null!;
        private TextBox txtEncryptKey = null!;
        private TextBox txtEncryptResult = null!;
        private Button btnEncrypt = null!;
        private Button btnLoadEncryptFile = null!;
        private Button btnSaveEncryptResult = null!;
        private Button btnClearEncrypt = null!;

        // Decrypt controls
        private TextBox txtDecryptInput = null!;
        private TextBox txtDecryptKey = null!;
        private TextBox txtDecryptResult = null!;
        private Button btnDecrypt = null!;
        private Button btnLoadDecryptFile = null!;
        private Button btnSaveDecryptResult = null!;
        private Button btnClearDecrypt = null!;

        public MainForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            // Main form settings - Modern design
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(900, 650);
            this.Text = "XOR Шифрование текста";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(245, 247, 250);

            // TabControl
            tabControl = new TabControl();
            tabControl.Location = new Point(20, 20);
            tabControl.Size = new Size(860, 580);
            tabControl.Font = new Font("Segoe UI", 10F);
            
            // Create tabs
            tabEncrypt = new TabPage("Шифрование");
            tabDecrypt = new TabPage("Расшифрование");
            
            tabControl.TabPages.Add(tabEncrypt);
            tabControl.TabPages.Add(tabDecrypt);
            
            this.Controls.Add(tabControl);

            // Setup Encrypt Tab
            SetupEncryptTab();
            
            // Setup Decrypt Tab
            SetupDecryptTab();

            this.ResumeLayout(false);
        }

        private void SetupEncryptTab()
        {
            tabEncrypt.BackColor = Color.FromArgb(245, 247, 250);
            tabEncrypt.AutoScroll = true;

            // Label for input text
            Label lblInput = CreateStyledLabel("Исходный текст:", 20, 20);
            tabEncrypt.Controls.Add(lblInput);

            // TextBox for input text
            txtEncryptInput = CreateStyledTextBox(20, 50, 780, 120, false);
            tabEncrypt.Controls.Add(txtEncryptInput);

            // Label for key
            Label lblKey = CreateStyledLabel("Ключ шифрования:", 20, 185);
            tabEncrypt.Controls.Add(lblKey);

            // TextBox for key
            txtEncryptKey = CreateStyledTextBox(20, 215, 350, 30, false);
            txtEncryptKey.Text = "SecretKey123";
            tabEncrypt.Controls.Add(txtEncryptKey);

            // Button panel
            int buttonY = 260;
            int buttonSpacing = 140;
            
            btnLoadEncryptFile = CreateStyledButton("📁 Загрузить из файла", 20, buttonY, 180, 40);
            btnLoadEncryptFile.Click += BtnLoadEncryptFile_Click;
            tabEncrypt.Controls.Add(btnLoadEncryptFile);

            btnEncrypt = CreateStyledButton("🔒 Зашифровать", 210, buttonY, 160, 40);
            btnEncrypt.BackColor = Color.FromArgb(76, 175, 80);
            btnEncrypt.ForeColor = Color.White;
            btnEncrypt.Click += BtnEncrypt_Click;
            tabEncrypt.Controls.Add(btnEncrypt);

            btnClearEncrypt = CreateStyledButton("🗑️ Очистить", 380, buttonY, 140, 40);
            btnClearEncrypt.BackColor = Color.FromArgb(244, 67, 54);
            btnClearEncrypt.ForeColor = Color.White;
            btnClearEncrypt.Click += BtnClearEncrypt_Click;
            tabEncrypt.Controls.Add(btnClearEncrypt);

            // Label for result
            Label lblResult = CreateStyledLabel("Результат (HEX):", 20, 320);
            tabEncrypt.Controls.Add(lblResult);

            // TextBox for result
            txtEncryptResult = CreateStyledTextBox(20, 350, 780, 150, true);
            tabEncrypt.Controls.Add(txtEncryptResult);

            // Save button
            btnSaveEncryptResult = CreateStyledButton("💾 Сохранить результат", 20, 520, 200, 40);
            btnSaveEncryptResult.BackColor = Color.FromArgb(33, 150, 243);
            btnSaveEncryptResult.ForeColor = Color.White;
            btnSaveEncryptResult.Click += BtnSaveEncryptResult_Click;
            tabEncrypt.Controls.Add(btnSaveEncryptResult);
        }

        private void SetupDecryptTab()
        {
            tabDecrypt.BackColor = Color.FromArgb(245, 247, 250);
            tabDecrypt.AutoScroll = true;

            // Label for input text
            Label lblInput = CreateStyledLabel("Зашифрованный текст (HEX):", 20, 20);
            tabDecrypt.Controls.Add(lblInput);

            // TextBox for input text
            txtDecryptInput = CreateStyledTextBox(20, 50, 780, 120, false);
            tabDecrypt.Controls.Add(txtDecryptInput);

            // Label for key
            Label lblKey = CreateStyledLabel("Ключ расшифрования:", 20, 185);
            tabDecrypt.Controls.Add(lblKey);

            // TextBox for key
            txtDecryptKey = CreateStyledTextBox(20, 215, 350, 30, false);
            txtDecryptKey.Text = "SecretKey123";
            tabDecrypt.Controls.Add(txtDecryptKey);

            // Button panel
            int buttonY = 260;
            
            btnLoadDecryptFile = CreateStyledButton("📁 Загрузить HEX файл", 20, buttonY, 180, 40);
            btnLoadDecryptFile.Click += BtnLoadDecryptFile_Click;
            tabDecrypt.Controls.Add(btnLoadDecryptFile);

            btnDecrypt = CreateStyledButton("🔓 Расшифровать", 210, buttonY, 160, 40);
            btnDecrypt.BackColor = Color.FromArgb(76, 175, 80);
            btnDecrypt.ForeColor = Color.White;
            btnDecrypt.Click += BtnDecrypt_Click;
            tabDecrypt.Controls.Add(btnDecrypt);

            btnClearDecrypt = CreateStyledButton("🗑️ Очистить", 380, buttonY, 140, 40);
            btnClearDecrypt.BackColor = Color.FromArgb(244, 67, 54);
            btnClearDecrypt.ForeColor = Color.White;
            btnClearDecrypt.Click += BtnClearDecrypt_Click;
            tabDecrypt.Controls.Add(btnClearDecrypt);

            // Label for result
            Label lblResult = CreateStyledLabel("Результат:", 20, 320);
            tabDecrypt.Controls.Add(lblResult);

            // TextBox for result
            txtDecryptResult = CreateStyledTextBox(20, 350, 780, 150, true);
            tabDecrypt.Controls.Add(txtDecryptResult);

            // Save button
            btnSaveDecryptResult = CreateStyledButton("💾 Сохранить результат", 20, 520, 200, 40);
            btnSaveDecryptResult.BackColor = Color.FromArgb(33, 150, 243);
            btnSaveDecryptResult.ForeColor = Color.White;
            btnSaveDecryptResult.Click += BtnSaveDecryptResult_Click;
            tabDecrypt.Controls.Add(btnSaveDecryptResult);
        }

        private Label CreateStyledLabel(string text, int x, int y)
        {
            Label label = new Label();
            label.Text = text;
            label.Location = new Point(x, y);
            label.AutoSize = true;
            label.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            label.ForeColor = Color.FromArgb(55, 65, 81);
            return label;
        }

        private TextBox CreateStyledTextBox(int x, int y, int width, int height, bool readOnly)
        {
            TextBox textBox = new TextBox();
            textBox.Location = new Point(x, y);
            textBox.Size = new Size(width, height);
            textBox.Multiline = true;
            textBox.ScrollBars = ScrollBars.Vertical;
            textBox.ReadOnly = readOnly;
            textBox.Font = new Font("Consolas", 10F);
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.BackColor = Color.White;
            return textBox;
        }

        private Button CreateStyledButton(string text, int x, int y, int width, int height)
        {
            Button button = new Button();
            button.Text = text;
            button.Location = new Point(x, y);
            button.Size = new Size(width, height);
            button.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Cursor = Cursors.Hand;
            return button;
        }

        private string XorCipher(string text, string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                MessageBox.Show("Ключ не может быть пустым!", "Ошибка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return string.Empty;
            }

            byte[] textBytes = Encoding.UTF8.GetBytes(text);
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            
            byte[] resultBytes = new byte[textBytes.Length];
            
            for (int i = 0; i < textBytes.Length; i++)
            {
                resultBytes[i] = (byte)(textBytes[i] ^ keyBytes[i % keyBytes.Length]);
            }
            
            // Convert to hex string for display
            StringBuilder sb = new StringBuilder();
            foreach (byte b in resultBytes)
            {
                sb.Append(b.ToString("X2"));
            }
            
            return sb.ToString();
        }

        private string XorDecipher(string hexText, string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                MessageBox.Show("Ключ не может быть пустым!", "Ошибка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return string.Empty;
            }

            try
            {
                // Convert from hex string to bytes
                hexText = hexText.Replace(" ", "").Replace("\n", "").Replace("\r", "");
                byte[] textBytes = new byte[hexText.Length / 2];
                for (int i = 0; i < hexText.Length; i += 2)
                {
                    textBytes[i / 2] = Convert.ToByte(hexText.Substring(i, 2), 16);
                }
                
                byte[] keyBytes = Encoding.UTF8.GetBytes(key);
                byte[] resultBytes = new byte[textBytes.Length];
                
                for (int i = 0; i < textBytes.Length; i++)
                {
                    resultBytes[i] = (byte)(textBytes[i] ^ keyBytes[i % keyBytes.Length]);
                }
                
                return Encoding.UTF8.GetString(resultBytes);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при расшифровке: {ex.Message}", "Ошибка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }

        private void BtnEncrypt_Click(object? sender, EventArgs e)
        {
            string inputText = txtEncryptInput.Text;
            string key = txtEncryptKey.Text;
            
            if (string.IsNullOrEmpty(inputText))
            {
                MessageBox.Show("Введите текст для шифрования!", "Предупреждение", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            string result = XorCipher(inputText, key);
            if (!string.IsNullOrEmpty(result))
            {
                txtEncryptResult.Text = result;
            }
        }

        private void BtnDecrypt_Click(object? sender, EventArgs e)
        {
            string inputText = txtDecryptInput.Text.Trim();
            string key = txtDecryptKey.Text;
            
            if (string.IsNullOrEmpty(inputText))
            {
                MessageBox.Show("Введите зашифрованный текст (в формате HEX)!", "Предупреждение", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            string result = XorDecipher(inputText, key);
            if (!string.IsNullOrEmpty(result))
            {
                txtDecryptResult.Text = result;
            }
        }

        private void BtnLoadEncryptFile_Click(object? sender, EventArgs e)
        {
            using OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            openFileDialog.Title = "Выберите файл для загрузки";
            
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string content = File.ReadAllText(openFileDialog.FileName, Encoding.UTF8);
                    txtEncryptInput.Text = content;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при чтении файла: {ex.Message}", "Ошибка", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnLoadDecryptFile_Click(object? sender, EventArgs e)
        {
            using OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Файлы (*.hex;*.txt)|*.hex;*.txt|Все файлы (*.*)|*.*";
            openFileDialog.Title = "Выберите HEX файл для расшифровки";
            
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string content = File.ReadAllText(openFileDialog.FileName, Encoding.UTF8);
                    txtDecryptInput.Text = content.Trim();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при чтении файла: {ex.Message}", "Ошибка", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnSaveEncryptResult_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEncryptResult.Text))
            {
                MessageBox.Show("Нет результата для сохранения!", "Предупреждение", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            using SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "HEX файлы (*.hex)|*.hex|Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            saveFileDialog.Title = "Сохранить результат шифрования";
            saveFileDialog.FileName = "encrypted.hex";
            
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    File.WriteAllText(saveFileDialog.FileName, txtEncryptResult.Text, Encoding.UTF8);
                    MessageBox.Show("Результат успешно сохранен!", "Успех", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}", "Ошибка", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnSaveDecryptResult_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDecryptResult.Text))
            {
                MessageBox.Show("Нет результата для сохранения!", "Предупреждение", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            using SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            saveFileDialog.Title = "Сохранить результат расшифровки";
            saveFileDialog.FileName = "decrypted.txt";
            
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    File.WriteAllText(saveFileDialog.FileName, txtDecryptResult.Text, Encoding.UTF8);
                    MessageBox.Show("Результат успешно сохранен!", "Успех", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}", "Ошибка", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnClearEncrypt_Click(object? sender, EventArgs e)
        {
            txtEncryptInput.Clear();
            txtEncryptResult.Clear();
            txtEncryptKey.Text = "SecretKey123";
        }

        private void BtnClearDecrypt_Click(object? sender, EventArgs e)
        {
            txtDecryptInput.Clear();
            txtDecryptResult.Clear();
            txtDecryptKey.Text = "SecretKey123";
        }
    }
}
