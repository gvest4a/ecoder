using System.Drawing;
using System.Windows.Forms;

namespace XorCipherApp.UI
{
    /// <summary>
    /// Helper class for creating styled UI controls with modern design
    /// </summary>
    public static class ControlFactory
    {
        /// <summary>
        /// Creates a modern panel with subtle border
        /// </summary>
        public static Panel CreateModernPanel(int x, int y, int width, int height, ColorPalette palette)
        {
            Panel panel = new ShadowPanel(palette);
            panel.Location = new Point(x, y);
            panel.Size = new Size(width, height);
            panel.Dock = DockStyle.None;
            panel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            
            return panel;
        }

        /// <summary>
        /// Creates a styled label
        /// </summary>
        public static Label CreateStyledLabel(string text, int x, int y, float fontSize = 11F, bool bold = false, ColorPalette? palette = null)
        {
            palette ??= new ColorPalette();
            Label label = new Label();
            label.Text = text;
            label.Location = new Point(x, y);
            label.AutoSize = true;
            label.Font = new Font("Segoe UI", fontSize, bold ? FontStyle.Bold : FontStyle.Regular);
            label.ForeColor = palette.TextPrimary;
            return label;
        }

        /// <summary>
        /// Creates a modern textbox with custom scrollbar styling
        /// </summary>
        public static ModernTextBox CreateModernTextBox(int x, int y, int width, int height, bool readOnly, ColorPalette palette)
        {
            var textBox = new ModernTextBox(palette)
            {
                Location = new Point(x, y),
                Size = new Size(width, height),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                ReadOnly = readOnly,
                Dock = DockStyle.None,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
            };
            
            return textBox;
        }

        /// <summary>
        /// Creates a modern button with hover effects
        /// </summary>
        public static ModernButton CreateModernButton(string text, int x, int y, int width, int height, Color baseColor, ColorPalette palette)
        {
            var button = new ModernButton(palette, text)
            {
                Location = new Point(x, y),
                Size = new Size(width, height),
                BackColor = baseColor,
                Dock = DockStyle.None,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            
            return button;
        }
    }
}
