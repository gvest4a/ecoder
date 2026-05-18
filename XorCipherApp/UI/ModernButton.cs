using System;
using System.Drawing;
using System.Windows.Forms;

namespace XorCipherApp.UI
{
    /// <summary>
    /// Modern button with hover effects and grayscale styling
    /// </summary>
    public class ModernButton : Button
    {
        private Color _baseColor;
        private bool _isHovered = false;

        public ModernButton(string text, Color baseColor)
        {
            _baseColor = baseColor;
            
            Text = text;
            Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            ForeColor = Color.White;
            BackColor = _baseColor;
            FlatStyle = FlatStyle.Flat;
            Cursor = Cursors.Hand;
            
            // Remove default border
            FlatAppearance.BorderSize = 0;
            FlatAppearance.MouseOverBackColor = LightenColor(_baseColor, 0.1F);
            FlatAppearance.MouseDownBackColor = DarkenColor(_baseColor, 0.1F);
            
            // Events for hover effect
            MouseEnter += (s, e) => 
            {
                _isHovered = true;
                BackColor = LightenColor(_baseColor, 0.15F);
                Invalidate();
            };
            
            MouseLeave += (s, e) => 
            {
                _isHovered = false;
                BackColor = _baseColor;
                Invalidate();
            };
        }

        private static Color LightenColor(Color color, float factor)
        {
            return Color.FromArgb(
                color.A,
                Math.Min(255, (int)(color.R * (1 + factor))),
                Math.Min(255, (int)(color.G * (1 + factor))),
                Math.Min(255, (int)(color.B * (1 + factor)))
            );
        }

        private static Color DarkenColor(Color color, float factor)
        {
            return Color.FromArgb(
                color.A,
                Math.Max(0, (int)(color.R * (1 - factor))),
                Math.Max(0, (int)(color.G * (1 - factor))),
                Math.Max(0, (int)(color.B * (1 - factor)))
            );
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
            // Draw rounded corners or custom effects if needed
            if (_isHovered)
            {
                ControlPaint.DrawBorder(e.Graphics, ClientRectangle, 
                    ColorPalette.BorderFocus, ButtonBorderStyle.Solid);
            }
        }
    }
}
