using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace XorCipherApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            // Main form settings
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Text = "XOR Шифрование текста";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Label for input text
            Label lblInput = new Label();
            lblInput.Text = "Исходный текст:";
            lblInput.Location = new System.Drawing.Point(20, 20);
            lblInput.AutoSize = true;
            this.Controls.Add(lblInput);

            // TextBox for input text
            txtInput = new TextBox();
            txtInput.Location = new System.Drawing.Point(20, 45);
            txtInput.Size = new System.Drawing.Size(760, 100);
            txtInput.Multiline = true;
            txtInput.ScrollBars = ScrollBars.Vertical;
            this.Controls.Add(txtInput);

            // Label for key
            Label lblKey = new Label();
            lblKey.Text = "Ключ шифрования:";
            lblKey.Location = new System.Drawing.Point(20, 160);
            lblKey.AutoSize = true;
            this.Controls.Add(lblKey);

            // TextBox for key
            txtKey = new TextBox();
            txtKey.Location = new System.Drawing.Point(20, 185);
            txtKey.Size = new System.Drawing.Size(300, 23);
            txtKey.Text = "secret";
            this.Controls.Add(txtKey);

            // Button for encrypt
            btnEncrypt = new Button();
            btnEncrypt.Text = "Зашифровать";
            btnEncrypt.Location = new System.Drawing.Point(20, 220);
            btnEncrypt.Size = new System.Drawing.Size(120, 30);
            btnEncrypt.Click += BtnEncrypt_Click;
            this.Controls.Add(btnEncrypt);

            // Button for decrypt
            btnDecrypt = new Button();
            btnDecrypt.Text = "Расшифровать";
            btnDecrypt.Location = new System.Drawing.Point(150, 220);
            btnDecrypt.Size = new System.Drawing.Size(120, 30);
            btnDecrypt.Click += BtnDecrypt_Click;
            this.Controls.Add(btnDecrypt);

            // Button for load file
            btnLoadFile = new Button();
            btnLoadFile.Text = "Загрузить из файла";
            btnLoadFile.Location = new System.Drawing.Point(280, 220);
            btnLoadFile.Size = new System.Drawing.Size(130, 30);
            btnLoadFile.Click += BtnLoadFile_Click;
            this.Controls.Add(btnLoadFile);

            // Label for result
            Label lblResult = new Label();
            lblResult.Text = "Результат:";
            lblResult.Location = new System.Drawing.Point(20, 270);
            lblResult.AutoSize = true;
            this.Controls.Add(lblResult);

            // TextBox for result
            txtResult = new TextBox();
            txtResult.Location = new System.Drawing.Point(20, 295);
            txtResult.Size = new System.Drawing.Size(760, 150);
            txtResult.Multiline = true;
            txtResult.ScrollBars = ScrollBars.Vertical;
            txtResult.ReadOnly = true;
            this.Controls.Add(txtResult);

            // Button for save result
            btnSaveResult = new Button();
            btnSaveResult.Text = "Сохранить результат в файл";
            btnSaveResult.Location = new System.Drawing.Point(20, 460);
            btnSaveResult.Size = new System.Drawing.Size(180, 30);
            btnSaveResult.Click += BtnSaveResult_Click;
            this.Controls.Add(btnSaveResult);

            // Button for clear
            btnClear = new Button();
            btnClear.Text = "Очистить";
            btnClear.Location = new System.Drawing.Point(210, 460);
            btnClear.Size = new System.Drawing.Size(100, 30);
            btnClear.Click += BtnClear_Click;
            this.Controls.Add(btnClear);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private TextBox txtInput = null!;
        private TextBox txtKey = null!;
        private TextBox txtResult = null!;
        private Button btnEncrypt = null!;
        private Button btnDecrypt = null!;
        private Button btnLoadFile = null!;
        private Button btnSaveResult = null!;
        private Button btnClear = null!;

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
            string inputText = txtInput.Text;
            string key = txtKey.Text;
            
            if (string.IsNullOrEmpty(inputText))
            {
                MessageBox.Show("Введите текст для шифрования!", "Предупреждение", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            string result = XorCipher(inputText, key);
            if (!string.IsNullOrEmpty(result))
            {
                txtResult.Text = result;
            }
        }

        private void BtnDecrypt_Click(object? sender, EventArgs e)
        {
            string inputText = txtInput.Text.Trim();
            string key = txtKey.Text;
            
            if (string.IsNullOrEmpty(inputText))
            {
                MessageBox.Show("Введите зашифрованный текст (в формате HEX)!", "Предупреждение", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            string result = XorDecipher(inputText, key);
            if (!string.IsNullOrEmpty(result))
            {
                txtResult.Text = result;
            }
        }

        private void BtnLoadFile_Click(object? sender, EventArgs e)
        {
            using OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            openFileDialog.Title = "Выберите файл для загрузки";
            
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string content = File.ReadAllText(openFileDialog.FileName, Encoding.UTF8);
                    txtInput.Text = content;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при чтении файла: {ex.Message}", "Ошибка", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnSaveResult_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtResult.Text))
            {
                MessageBox.Show("Нет результата для сохранения!", "Предупреждение", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            using SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            saveFileDialog.Title = "Сохранить результат";
            saveFileDialog.FileName = "result.txt";
            
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    File.WriteAllText(saveFileDialog.FileName, txtResult.Text, Encoding.UTF8);
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

        private void BtnClear_Click(object? sender, EventArgs e)
        {
            txtInput.Clear();
            txtResult.Clear();
            txtKey.Text = "secret";
        }
    }
}
