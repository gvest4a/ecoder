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
    /// Main form for XOR Cipher application with fixed layout and grayscale theme
    /// </summary>
    public partial class MainForm : Form
    {
        private readonly IXorCipher _cipher;
        private readonly IFileService _fileService;
        private readonly CipherConfig _config;

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
            ApplyCustomStyles();
        }

        private void ApplyCustomStyles()
        {
            // Apply custom styles from ColorPalette and ControlFactory
            BackColor = ColorPalette.BgLight;
            
            // Style Encrypt Tab panels
            leftPanelEncrypt.BackColor = ColorPalette.SurfaceWhite;
            rightPanelEncrypt.BackColor = ColorPalette.SurfaceWhite;
            
            // Style Decrypt Tab panels
            leftPanelDecrypt.BackColor = ColorPalette.SurfaceWhite;
            rightPanelDecrypt.BackColor = ColorPalette.SurfaceWhite;
            
            // Apply button styles
            StyleButton(btnLoadEncryptFile);
            StyleButton(btnEncrypt);
            StyleButton(btnClearEncrypt);
            StyleButton(btnSaveEncryptResult);
            StyleButton(btnLoadDecryptFile);
            StyleButton(btnDecrypt);
            StyleButton(btnClearDecrypt);
            StyleButton(btnSaveDecryptResult);
            
            // Apply textbox styles
            StyleTextBox(txtEncryptInput);
            StyleTextBox(txtEncryptKey);
            StyleTextBox(txtEncryptResult);
            StyleTextBox(txtDecryptInput);
            StyleTextBox(txtDecryptKey);
            StyleTextBox(txtDecryptResult);
        }

        private void StyleButton(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = ColorPalette.PrimaryDark;
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            btn.Cursor = Cursors.Hand;
        }

        private void StyleTextBox(TextBox txt)
        {
            txt.BorderStyle = BorderStyle.FixedSingle;
            txt.Font = new Font("Segoe UI", 10F);
            txt.Padding = new Padding(5);
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
