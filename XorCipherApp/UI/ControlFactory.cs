using System.Drawing;
using System.Windows.Forms;

namespace XorCipherApp.UI
{
    /// <summary>
    /// Helper class for creating styled UI controls with improved scrollbars
    /// </summary>
    public static class ControlFactory
    {
        /// <summary>
        /// Creates a modern panel with subtle shadow effect
        /// </summary>
        public static Panel CreateModernPanel(int x, int y, int width, int height)
        {
            Panel panel = new Panel();
            panel.Location = new Point(x, y);
            panel.Size = new Size(width, height);
            panel.BackColor = ColorPalette.SurfaceWhite;
            panel.BorderStyle = BorderStyle.None;
            panel.Padding = new Padding(20);
            
            // Add subtle border
            panel.Paint += (s, e) =>
            {
                using var pen = new Pen(ColorPalette.BorderLight, 1);
                e.Graphics.DrawRectangle(pen, 0, 0, panel.Width - 1, panel.Height - 1);
            };
            
            return panel;
        }

        /// <summary>
        /// Creates a styled label
        /// </summary>
        public static Label CreateStyledLabel(string text, int x, int y, float fontSize = 11F, bool bold = false)
        {
            Label label = new Label();
            label.Text = text;
            label.Location = new Point(x, y);
            label.AutoSize = true;
            label.Font = new Font("Segoe UI", fontSize, bold ? FontStyle.Bold : FontStyle.Regular);
            label.ForeColor = ColorPalette.TextPrimary;
            return label;
        }

        /// <summary>
        /// Creates a modern textbox with custom scrollbar styling
        /// </summary>
        public static TextBox CreateModernTextBox(int x, int y, int width, int height, bool readOnly)
        {
            TextBox textBox = new TextBox();
            textBox.Location = new Point(x, y);
            textBox.Size = new Size(width, height);
            textBox.Multiline = true;
            textBox.ScrollBars = ScrollBars.Vertical;
            textBox.ReadOnly = readOnly;
            textBox.Font = new Font("Consolas", 10F);
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.BackColor = ColorPalette.SurfaceLight;
            textBox.ForeColor = ColorPalette.TextPrimary;
            
            // Set padding using SendMessage for better control
            const int EM_SETMARGINS = 0xD3;
            const int EC_LEFTMARGIN = 0x1;
            const int EC_RIGHTMARGIN = 0x2;
            
            // Focus events for visual feedback
            textBox.GotFocus += (s, e) =>
            {
                var tb = (TextBox)s!;
                tb.BackColor = ColorPalette.SurfaceWhite;
                tb.BorderStyle = BorderStyle.FixedSingle;
            };
            
            textBox.LostFocus += (s, e) =>
            {
                var tb = (TextBox)s!;
                tb.BackColor = ColorPalette.SurfaceLight;
                tb.BorderStyle = BorderStyle.FixedSingle;
            };
            
            return textBox;
        }

        /// <summary>
        /// Creates a modern button with hover effects
        /// </summary>
        public static Button CreateModernButton(string text, int x, int y, int width, int height, Color baseColor)
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
            button.FlatAppearance.BorderColor = baseColor;
            
            // Add rounded corners effect using Region (optional enhancement)
            button.FlatAppearance.BorderSize = 0;
            
            return button;
        }

        /// <summary>
        /// Custom TextBox with improved scrollbar appearance
        /// </summary>
        public class StyledTextBox : TextBox
        {
            public StyledTextBox()
            {
                this.Multiline = true;
                this.ScrollBars = ScrollBars.Vertical;
                this.Font = new Font("Consolas", 10F);
                this.BorderStyle = BorderStyle.FixedSingle;
                this.BackColor = ColorPalette.SurfaceLight;
                this.ForeColor = ColorPalette.TextPrimary;
            }

            protected override void OnGotFocus(EventArgs e)
            {
                base.OnGotFocus(e);
                this.BackColor = ColorPalette.SurfaceWhite;
            }

            protected override void OnLostFocus(EventArgs e)
            {
                base.OnLostFocus(e);
                this.BackColor = ColorPalette.SurfaceLight;
            }
        }
    }
}
