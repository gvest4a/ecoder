using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using XorCipherApp.Core;
using XorCipherApp.Models;
using XorCipherApp.Services;
using XorCipherApp.UI;

namespace XorCipherApp
{
    /// <summary>
    /// Main form for XOR Cipher application with responsive design and grayscale theme
    /// </summary>
    public partial class MainForm : ResponsiveForm
    {
        private readonly IXorCipher _cipher;
        private readonly IFileService _fileService;
        private readonly CipherConfig _config;

        // UI Components - Encrypt Tab
        private TabControl tabControl = null!;
        private TabPage tabEncrypt = null!;
        private TabPage tabDecrypt = null!;
        
        private TextBox txtEncryptInput = null!;
        private TextBox txtEncryptKey = null!;
        private TextBox txtEncryptResult = null!;
        private Button btnEncrypt = null!;
        private Button btnLoadEncryptFile = null!;
        private Button btnSaveEncryptResult = null!;
        private Button btnClearEncrypt = null!;
        private Panel leftPanelEncrypt = null!;
        private Panel rightPanelEncrypt = null!;

        // UI Components - Decrypt Tab
        private TextBox txtDecryptInput = null!;
        private TextBox txtDecryptKey = null!;
        private TextBox txtDecryptResult = null!;
        private Button btnDecrypt = null!;
        private Button btnLoadDecryptFile = null!;
        private Button btnSaveDecryptResult = null!;
        private Button btnClearDecrypt = null!;
        private Panel leftPanelDecrypt = null!;
        private Panel rightPanelDecrypt = null!;

        public MainForm() : this(new XorCipher(), new FileService(), new CipherConfig())
        {
        }

        // Constructor for dependency injection (extensible architecture)
        public MainForm(IXorCipher cipher, IFileService fileService, CipherConfig config)
        {
            _cipher = cipher;
            _fileService = fileService;
            _config = config;
            
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            // Main form settings - Modern design with grayscale theme
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1100, 700);
            this.MinimumSize = new Size(900, 600);
            this.Text = "XOR Шифрование текста";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = ColorPalette.BgLight;
            this.Padding = new Padding(15);

            // TabControl with custom styling
            tabControl = new TabControl();
            tabControl.Dock = DockStyle.Fill;
            tabControl.Font = new Font("Segoe UI", 10F);
            tabControl.ItemSize = new Size(140, 40);
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
            
            // Background with gradient for selected tab
            using Brush brush = new SolidBrush(isSelected ? ColorPalette.PrimaryDark : ColorPalette.BgMedium);
            e.Graphics.FillRectangle(brush, tabBounds);
            
            // Text
            StringFormat stringFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            
            using Brush textBrush = new SolidBrush(isSelected ? Color.White : ColorPalette.TextSecondary);
            e.Graphics.DrawString(page.Text, new Font("Segoe UI Semibold", 10F), textBrush, tabBounds, stringFormat);
            
            // Highlight line for selected tab
            if (isSelected)
            {
                using Pen pen = new Pen(ColorPalette.PrimaryDark, 3);
                e.Graphics.DrawLine(pen, tabBounds.Left + 2, tabBounds.Bottom - 1, tabBounds.Right - 2, tabBounds.Bottom - 1);
            }
        }

        private void SetupEncryptTab()
        {
            tabEncrypt.BackColor = ColorPalette.BgLight;
            tabEncrypt.AutoScroll = true;
            tabEncrypt.Padding = new Padding(15);

            // Left Panel - Input and Controls
            leftPanelEncrypt = ControlFactory.CreateModernPanel(15, 15, 480, 580);
            tabEncrypt.Controls.Add(leftPanelEncrypt);

            // Right Panel - Output
            rightPanelEncrypt = ControlFactory.CreateModernPanel(520, 15, 480, 580);
            tabEncrypt.Controls.Add(rightPanelEncrypt);

            // === LEFT PANEL CONTENT ===
            int yOffset = 20;
            
            Label lblInput = ControlFactory.CreateStyledLabel("Исходный текст:", 20, yOffset, 12F, true);
            leftPanelEncrypt.Controls.Add(lblInput);
            yOffset += 35;

            txtEncryptInput = ControlFactory.CreateModernTextBox(20, yOffset, 440, 150, false);
            leftPanelEncrypt.Controls.Add(txtEncryptInput);
            yOffset += 175;

            Label lblKey = ControlFactory.CreateStyledLabel("Ключ шифрования:", 20, yOffset, 11F, true);
            leftPanelEncrypt.Controls.Add(lblKey);
            yOffset += 30;

            txtEncryptKey = ControlFactory.CreateModernTextBox(20, yOffset, 440, 40, false);
            txtEncryptKey.Text = _config.DefaultKey;
            leftPanelEncrypt.Controls.Add(txtEncryptKey);
            yOffset += 65;

            // Buttons in left panel
            btnLoadEncryptFile = ControlFactory.CreateModernButton("📁 Загрузить из файла", 20, yOffset, 195, 45, ColorPalette.Info);
            btnLoadEncryptFile.Click += BtnLoadEncryptFile_Click;
            leftPanelEncrypt.Controls.Add(btnLoadEncryptFile);

            btnEncrypt = ControlFactory.CreateModernButton("🔒 Зашифровать", 235, yOffset, 195, 45, ColorPalette.PrimaryMedium);
            btnEncrypt.Click += BtnEncrypt_Click;
            leftPanelEncrypt.Controls.Add(btnEncrypt);
            yOffset += 60;

            btnClearEncrypt = ControlFactory.CreateModernButton("🗑️ Очистить всё", 20, yOffset, 195, 45, ColorPalette.Error);
            btnClearEncrypt.Click += BtnClearEncrypt_Click;
            leftPanelEncrypt.Controls.Add(btnClearEncrypt);

            // === RIGHT PANEL CONTENT ===
            Label lblResult = ControlFactory.CreateStyledLabel("Результат (HEX):", 20, 20, 12F, true);
            rightPanelEncrypt.Controls.Add(lblResult);

            txtEncryptResult = ControlFactory.CreateModernTextBox(20, 55, 440, 430, true);
            txtEncryptResult.BackColor = ColorPalette.SurfaceWhite;
            rightPanelEncrypt.Controls.Add(txtEncryptResult);

            btnSaveEncryptResult = ControlFactory.CreateModernButton("💾 Сохранить результат", 20, 510, 440, 50, ColorPalette.PrimaryDark);
            btnSaveEncryptResult.Click += BtnSaveEncryptResult_Click;
            rightPanelEncrypt.Controls.Add(btnSaveEncryptResult);
        }

        private void SetupDecryptTab()
        {
            tabDecrypt.BackColor = ColorPalette.BgLight;
            tabDecrypt.AutoScroll = true;
            tabDecrypt.Padding = new Padding(15);

            // Left Panel - Input and Controls
            leftPanelDecrypt = ControlFactory.CreateModernPanel(15, 15, 480, 580);
            tabDecrypt.Controls.Add(leftPanelDecrypt);

            // Right Panel - Output
            rightPanelDecrypt = ControlFactory.CreateModernPanel(520, 15, 480, 580);
            tabDecrypt.Controls.Add(rightPanelDecrypt);

            // === LEFT PANEL CONTENT ===
            int yOffset = 20;
            
            Label lblInput = ControlFactory.CreateStyledLabel("Зашифрованный текст (HEX):", 20, yOffset, 12F, true);
            leftPanelDecrypt.Controls.Add(lblInput);
            yOffset += 35;

            txtDecryptInput = ControlFactory.CreateModernTextBox(20, yOffset, 440, 150, false);
            leftPanelDecrypt.Controls.Add(txtDecryptInput);
            yOffset += 175;

            Label lblKey = ControlFactory.CreateStyledLabel("Ключ расшифрования:", 20, yOffset, 11F, true);
            leftPanelDecrypt.Controls.Add(lblKey);
            yOffset += 30;

            txtDecryptKey = ControlFactory.CreateModernTextBox(20, yOffset, 440, 40, false);
            txtDecryptKey.Text = _config.DefaultKey;
            leftPanelDecrypt.Controls.Add(txtDecryptKey);
            yOffset += 65;

            // Buttons in left panel
            btnLoadDecryptFile = ControlFactory.CreateModernButton("📁 Загрузить HEX файл", 20, yOffset, 195, 45, ColorPalette.Info);
            btnLoadDecryptFile.Click += BtnLoadDecryptFile_Click;
            leftPanelDecrypt.Controls.Add(btnLoadDecryptFile);

            btnDecrypt = ControlFactory.CreateModernButton("🔓 Расшифровать", 235, yOffset, 195, 45, ColorPalette.PrimaryMedium);
            btnDecrypt.Click += BtnDecrypt_Click;
            leftPanelDecrypt.Controls.Add(btnDecrypt);
            yOffset += 60;

            btnClearDecrypt = ControlFactory.CreateModernButton("🗑️ Очистить всё", 20, yOffset, 195, 45, ColorPalette.Error);
            btnClearDecrypt.Click += BtnClearDecrypt_Click;
            leftPanelDecrypt.Controls.Add(btnClearDecrypt);

            // === RIGHT PANEL CONTENT ===
            Label lblResult = ControlFactory.CreateStyledLabel("Результат:", 20, 20, 12F, true);
            rightPanelDecrypt.Controls.Add(lblResult);

            txtDecryptResult = ControlFactory.CreateModernTextBox(20, 55, 440, 430, true);
            txtDecryptResult.BackColor = ColorPalette.SurfaceWhite;
            rightPanelDecrypt.Controls.Add(txtDecryptResult);

            btnSaveDecryptResult = ControlFactory.CreateModernButton("💾 Сохранить результат", 20, 510, 440, 50, ColorPalette.PrimaryDark);
            btnSaveDecryptResult.Click += BtnSaveDecryptResult_Click;
            rightPanelDecrypt.Controls.Add(btnSaveDecryptResult);
        }

        protected override void OnScaleChanged(EventArgs e)
        {
            base.OnScaleChanged(e);
            // Apply scaling to panels and controls based on window size
            // This enables responsive behavior
            var scaleFactor = ScaleFactor;
            
            // Adjust panel sizes proportionally
            if (leftPanelEncrypt != null && rightPanelEncrypt != null)
            {
                int panelWidth = (int)(480 * scaleFactor);
                int panelHeight = (int)(580 * scaleFactor);
                int gap = (int)(20 * scaleFactor);
                
                leftPanelEncrypt.Size = new Size(panelWidth, panelHeight);
                rightPanelEncrypt.Size = new Size(panelWidth, panelHeight);
                rightPanelEncrypt.Location = new Point((int)((480 + gap) * scaleFactor), (int)(15 * scaleFactor));
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

            try
            {
                string result = _cipher.Encrypt(inputText, key);
                txtEncryptResult.Text = result;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            try
            {
                string result = _cipher.Decrypt(inputText, key);
                txtDecryptResult.Text = result;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при расшифровке: {ex.Message}", "Ошибка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnLoadEncryptFile_Click(object? sender, EventArgs e)
        {
            using OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = _config.InputFileFilter;
            openFileDialog.Title = "Выберите файл для загрузки";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string content = await _fileService.ReadFileAsync(openFileDialog.FileName);
                    txtEncryptInput.Text = content;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при чтении файла: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void BtnLoadDecryptFile_Click(object? sender, EventArgs e)
        {
            using OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = _config.HexFileFilter;
            openFileDialog.Title = "Выберите HEX файл для расшифровки";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string content = await _fileService.ReadFileAsync(openFileDialog.FileName);
                    txtDecryptInput.Text = content.Trim();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при чтении файла: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void BtnSaveEncryptResult_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEncryptResult.Text))
            {
                MessageBox.Show("Нет результата для сохранения!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = $"HEX файлы (*.hex)|*.hex|{_config.InputFileFilter}";
            saveFileDialog.Title = "Сохранить результат шифрования";
            saveFileDialog.FileName = _config.OutputHexFileName;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    await _fileService.WriteFileAsync(saveFileDialog.FileName, txtEncryptResult.Text);
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

        private async void BtnSaveDecryptResult_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDecryptResult.Text))
            {
                MessageBox.Show("Нет результата для сохранения!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = _config.InputFileFilter;
            saveFileDialog.Title = "Сохранить результат расшифровки";
            saveFileDialog.FileName = _config.OutputTextFileName;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    await _fileService.WriteFileAsync(saveFileDialog.FileName, txtDecryptResult.Text);
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
            txtEncryptKey.Text = _config.DefaultKey;
        }

        private void BtnClearDecrypt_Click(object? sender, EventArgs e)
        {
            txtDecryptInput.Clear();
            txtDecryptResult.Clear();
            txtDecryptKey.Text = _config.DefaultKey;
        }
    }
}
