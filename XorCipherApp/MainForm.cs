using System;
using System.Drawing;
using System.Drawing.Drawing2D;
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
        private Panel leftPanelEncrypt = null!;
        private Panel rightPanelEncrypt = null!;

        // Decrypt controls
        private TextBox txtDecryptInput = null!;
        private TextBox txtDecryptKey = null!;
        private TextBox txtDecryptResult = null!;
        private Button btnDecrypt = null!;
        private Button btnLoadDecryptFile = null!;
        private Button btnSaveDecryptResult = null!;
        private Button btnClearDecrypt = null!;
        private Panel leftPanelDecrypt = null!;
        private Panel rightPanelDecrypt = null!;

        // Color palette - Muted Purple
        private readonly Color bgColor = Color.FromArgb(245, 242, 250);
        private readonly Color panelColor = Color.FromArgb(255, 255, 255);
        private readonly Color primaryColor = Color.FromArgb(108, 92, 153);
        private readonly Color primaryDark = Color.FromArgb(88, 72, 123);
        private readonly Color accentColor = Color.FromArgb(149, 117, 205);
        private readonly Color textColor = Color.FromArgb(62, 52, 81);
        private readonly Color successColor = Color.FromArgb(76, 175, 80);
        private readonly Color dangerColor = Color.FromArgb(229, 115, 115);
        private readonly Color infoColor = Color.FromArgb(66, 165, 245);

        public MainForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            // Main form settings - Modern design with rounded corners simulation
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1100, 700);
            this.Text = "XOR Шифрование текста";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = bgColor;
            this.Padding = new Padding(20);

            // TabControl with custom styling
            tabControl = new TabControl();
            tabControl.Dock = DockStyle.Fill;
            tabControl.Font = new Font("Segoe UI", 10F);
            tabControl.ItemSize = new Size(120, 35);
            tabControl.SizeMode = TabSizeMode.Fixed;
            
            // Custom draw for tabs
            tabControl.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl.DrawItem += TabControl_DrawItem;
            
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

        private void TabControl_DrawItem(object? sender, DrawItemEventArgs e)
        {
            TabControl tabControl = (TabControl)sender!;
            TabPage page = tabControl.TabPages[e.Index];
            
            Rectangle tabBounds = e.Bounds;
            bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            
            // Background
            using Brush brush = new SolidBrush(isSelected ? primaryColor : Color.FromArgb(230, 225, 235));
            e.Graphics.FillRectangle(brush, tabBounds);
            
            // Text
            StringFormat stringFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            
            using Brush textBrush = new SolidBrush(isSelected ? Color.White : textColor);
            e.Graphics.DrawString(page.Text, new Font("Segoe UI Semibold", 10F), textBrush, tabBounds, stringFormat);
            
            // Rounded top corners for selected tab
            if (isSelected)
            {
                using Pen pen = new Pen(primaryColor, 2);
                e.Graphics.DrawLine(pen, tabBounds.Left, tabBounds.Bottom - 1, tabBounds.Right, tabBounds.Bottom - 1);
            }
        }

        private void SetupEncryptTab()
        {
            tabEncrypt.BackColor = bgColor;
            tabEncrypt.AutoScroll = true;
            tabEncrypt.Padding = new Padding(0);

            // Left Panel - Input and Controls
            leftPanelEncrypt = CreateModernPanel(20, 20, 500, 600);
            tabEncrypt.Controls.Add(leftPanelEncrypt);

            // Right Panel - Output
            rightPanelEncrypt = CreateModernPanel(540, 20, 500, 600);
            tabEncrypt.Controls.Add(rightPanelEncrypt);

            // === LEFT PANEL CONTENT ===
            int yOffset = 25;
            
            Label lblInput = CreateStyledLabel("Исходный текст:", 20, yOffset, 12F);
            leftPanelEncrypt.Controls.Add(lblInput);
            yOffset += 35;

            txtEncryptInput = CreateModernTextBox(20, yOffset, 460, 140, false);
            leftPanelEncrypt.Controls.Add(txtEncryptInput);
            yOffset += 160;

            Label lblKey = CreateStyledLabel("Ключ шифрования:", 20, yOffset, 11F);
            leftPanelEncrypt.Controls.Add(lblKey);
            yOffset += 30;

            txtEncryptKey = CreateModernTextBox(20, yOffset, 460, 40, false);
            txtEncryptKey.Text = "SecretKey123";
            leftPanelEncrypt.Controls.Add(txtEncryptKey);
            yOffset += 60;

            // Buttons in left panel
            btnLoadEncryptFile = CreateModernButton("📁 Загрузить из файла", 20, yOffset, 200, 42, infoColor);
            btnLoadEncryptFile.Click += BtnLoadEncryptFile_Click;
            leftPanelEncrypt.Controls.Add(btnLoadEncryptFile);

            btnEncrypt = CreateModernButton("🔒 Зашифровать", 240, yOffset, 200, 42, successColor);
            btnEncrypt.Click += BtnEncrypt_Click;
            leftPanelEncrypt.Controls.Add(btnEncrypt);
            yOffset += 55;

            btnClearEncrypt = CreateModernButton("🗑️ Очистить всё", 20, yOffset, 200, 42, dangerColor);
            btnClearEncrypt.Click += BtnClearEncrypt_Click;
            leftPanelEncrypt.Controls.Add(btnClearEncrypt);

            // === RIGHT PANEL CONTENT ===
            Label lblResult = CreateStyledLabel("Результат (HEX):", 20, 25, 12F);
            rightPanelEncrypt.Controls.Add(lblResult);

            txtEncryptResult = CreateModernTextBox(20, 60, 460, 420, true);
            rightPanelEncrypt.Controls.Add(txtEncryptResult);

            btnSaveEncryptResult = CreateModernButton("💾 Сохранить результат", 20, 500, 460, 45, primaryColor);
            btnSaveEncryptResult.Click += BtnSaveEncryptResult_Click;
            rightPanelEncrypt.Controls.Add(btnSaveEncryptResult);
        }

        private void SetupDecryptTab()
        {
            tabDecrypt.BackColor = bgColor;
            tabDecrypt.AutoScroll = true;
            tabDecrypt.Padding = new Padding(0);

            // Left Panel - Input and Controls
            leftPanelDecrypt = CreateModernPanel(20, 20, 500, 600);
            tabDecrypt.Controls.Add(leftPanelDecrypt);

            // Right Panel - Output
            rightPanelDecrypt = CreateModernPanel(540, 20, 500, 600);
            tabDecrypt.Controls.Add(rightPanelDecrypt);

            // === LEFT PANEL CONTENT ===
            int yOffset = 25;
            
            Label lblInput = CreateStyledLabel("Зашифрованный текст (HEX):", 20, yOffset, 12F);
            leftPanelDecrypt.Controls.Add(lblInput);
            yOffset += 35;

            txtDecryptInput = CreateModernTextBox(20, yOffset, 460, 140, false);
            leftPanelDecrypt.Controls.Add(txtDecryptInput);
            yOffset += 160;

            Label lblKey = CreateStyledLabel("Ключ расшифрования:", 20, yOffset, 11F);
            leftPanelDecrypt.Controls.Add(lblKey);
            yOffset += 30;

            txtDecryptKey = CreateModernTextBox(20, yOffset, 460, 40, false);
            txtDecryptKey.Text = "SecretKey123";
            leftPanelDecrypt.Controls.Add(txtDecryptKey);
            yOffset += 60;

            // Buttons in left panel
            btnLoadDecryptFile = CreateModernButton("📁 Загрузить HEX файл", 20, yOffset, 200, 42, infoColor);
            btnLoadDecryptFile.Click += BtnLoadDecryptFile_Click;
            leftPanelDecrypt.Controls.Add(btnLoadDecryptFile);

            btnDecrypt = CreateModernButton("🔓 Расшифровать", 240, yOffset, 200, 42, successColor);
            btnDecrypt.Click += BtnDecrypt_Click;
            leftPanelDecrypt.Controls.Add(btnDecrypt);
            yOffset += 55;

            btnClearDecrypt = CreateModernButton("🗑️ Очистить всё", 20, yOffset, 200, 42, dangerColor);
            btnClearDecrypt.Click += BtnClearDecrypt_Click;
            leftPanelDecrypt.Controls.Add(btnClearDecrypt);

            // === RIGHT PANEL CONTENT ===
            Label lblResult = CreateStyledLabel("Результат:", 20, 25, 12F);
            rightPanelDecrypt.Controls.Add(lblResult);

            txtDecryptResult = CreateModernTextBox(20, 60, 460, 420, true);
            rightPanelDecrypt.Controls.Add(txtDecryptResult);

            btnSaveDecryptResult = CreateModernButton("💾 Сохранить результат", 20, 500, 460, 45, primaryColor);
            btnSaveDecryptResult.Click += BtnSaveDecryptResult_Click;
            rightPanelDecrypt.Controls.Add(btnSaveDecryptResult);
        }

        private Panel CreateModernPanel(int x, int y, int width, int height)
        {
            Panel panel = new Panel();
            panel.Location = new Point(x, y);
            panel.Size = new Size(width, height);
            panel.BackColor = panelColor;
            panel.BorderStyle = BorderStyle.None;
            
            // Shadow effect simulation using Padding and BackColor
            panel.Padding = new Padding(15);
            
            return panel;
        }

        private Label CreateStyledLabel(string text, int x, int y, float fontSize = 11F)
        {
            Label label = new Label();
            label.Text = text;
            label.Location = new Point(x, y);
            label.AutoSize = true;
            label.Font = new Font("Segoe UI Semibold", fontSize, FontStyle.Regular);
            label.ForeColor = textColor;
            return label;
        }

        private TextBox CreateModernTextBox(int x, int y, int width, int height, bool readOnly)
        {
            TextBox textBox = new TextBox();
            textBox.Location = new Point(x, y);
            textBox.Size = new Size(width, height);
            textBox.Multiline = true;
            textBox.ScrollBars = ScrollBars.Vertical;
            textBox.ReadOnly = readOnly;
            textBox.Font = new Font("Consolas", 10F);
            textBox.BorderStyle = BorderStyle.None;
            textBox.BackColor = Color.FromArgb(250, 248, 252);
            textBox.Padding = new Padding(10);
            
            // Add a subtle border
            textBox.GotFocus += (s, e) => {
                var tb = (TextBox)s!;
                tb.BackColor = Color.White;
            };
            textBox.LostFocus += (s, e) => {
                var tb = (TextBox)s!;
                tb.BackColor = Color.FromArgb(250, 248, 252);
            };
            
            return textBox;
        }

        private Button CreateModernButton(string text, int x, int y, int width, int height, Color baseColor)
        {
            Button button = new Button();
            button.Text = text;
            button.Location = new Point(x, y);
            button.Size = new Size(width, height);
            button.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular);
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Cursor = Cursors.Hand;
            button.BackColor = baseColor;
            button.ForeColor = Color.White;
            button.FlatAppearance.MouseOverBackColor = ControlPaint.Light(baseColor, 0.1f);
            button.FlatAppearance.MouseDownBackColor = ControlPaint.Dark(baseColor, 0.1f);
            
            // Round corners simulation (requires custom drawing or using Region, keeping it simple for now)
            button.FlatAppearance.BorderColor = baseColor;
            
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
