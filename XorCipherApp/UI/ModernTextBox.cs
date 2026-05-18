using System;
using System.Drawing;
using System.Windows.Forms;

namespace XorCipherApp.UI
{
    /// <summary>
    /// Modern textbox with custom styling and improved scrollbars
    /// </summary>
    public class ModernTextBox : TextBox
    {
        private readonly ColorPalette _palette;
        private bool _isFocused = false;

        public ModernTextBox(ColorPalette palette)
        {
            _palette = palette;
            
            // Basic styling
            Font = new Font("Segoe UI", 10F);
            ForeColor = _palette.TextPrimary;
            BackColor = _palette.SurfaceWhite;
            BorderStyle = BorderStyle.FixedSingle;
            
            // Improved scrollbar appearance
            ScrollBars = ScrollBars.Vertical;
            
            // Events for focus handling
            GotFocus += (s, e) => 
            {
                _isFocused = true;
                BackColor = _palette.BgLight;
                Invalidate();
            };
            
            LostFocus += (s, e) => 
            {
                _isFocused = false;
                BackColor = _palette.SurfaceWhite;
                Invalidate();
            };
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
            // Draw custom border if needed
            if (_isFocused)
            {
                ControlPaint.DrawBorder(e.Graphics, ClientRectangle, 
                    _palette.PrimaryMedium, ButtonBorderStyle.Solid);
            }
            else
            {
                ControlPaint.DrawBorder(e.Graphics, ClientRectangle, 
                    _palette.BorderLight, ButtonBorderStyle.Solid);
            }
        }
    }
}
