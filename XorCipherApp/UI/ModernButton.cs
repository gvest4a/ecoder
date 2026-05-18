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
        private readonly ColorPalette _palette;
        private Color _baseColor;
        private bool _isHovered = false;

        public ModernButton(ColorPalette palette, string text)
        {
            _palette = palette;
            _baseColor = _palette.PrimaryDark;
            
            Text = text;
            Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            ForeColor = Color.White;
            BackColor = _baseColor;
            FlatStyle = FlatStyle.Flat;
            Cursor = Cursors.Hand;
            
            // Remove default border
            FlatAppearance.BorderSize = 0;
            FlatAppearance.MouseOverBackColor = ControlPaint.Lighter(_baseColor, 0.1F);
            FlatAppearance.MouseDownBackColor = ControlPaint.Darker(_baseColor, 0.1F);
            
            // Events for hover effect
            MouseEnter += (s, e) => 
            {
                _isHovered = true;
                BackColor = ControlPaint.Lighter(_baseColor, 0.15F);
                Invalidate();
            };
            
            MouseLeave += (s, e) => 
            {
                _isHovered = false;
                BackColor = _baseColor;
                Invalidate();
            };
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
            // Draw rounded corners or custom effects if needed
            if (_isHovered)
            {
                ControlPaint.DrawBorder(e.Graphics, ClientRectangle, 
                    _palette.PrimaryLight, ButtonBorderStyle.Solid);
            }
        }
    }
}
