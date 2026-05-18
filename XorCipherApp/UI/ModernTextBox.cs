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
        private bool _isFocused = false;

        public ModernTextBox()
        {
            // Basic styling
            Font = new Font("Segoe UI", 10F);
            ForeColor = ColorPalette.TextPrimary;
            BackColor = ColorPalette.SurfaceWhite;
            BorderStyle = BorderStyle.FixedSingle;
            
            // Improved scrollbar appearance
            ScrollBars = ScrollBars.Vertical;
            
            // Events for focus handling
            GotFocus += (s, e) => 
            {
                _isFocused = true;
                BackColor = ColorPalette.BgLight;
                Invalidate();
            };
            
            LostFocus += (s, e) => 
            {
                _isFocused = false;
                BackColor = ColorPalette.SurfaceWhite;
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
                    ColorPalette.BorderFocus, 2, ButtonBorderStyle.Solid,
                    ColorPalette.BorderFocus, 2, ButtonBorderStyle.Solid,
                    ColorPalette.BorderFocus, 2, ButtonBorderStyle.Solid,
                    ColorPalette.BorderFocus, 2, ButtonBorderStyle.Solid);
            }
            else
            {
                ControlPaint.DrawBorder(e.Graphics, ClientRectangle, 
                    ColorPalette.BorderLight, 1, ButtonBorderStyle.Solid,
                    ColorPalette.BorderLight, 1, ButtonBorderStyle.Solid,
                    ColorPalette.BorderLight, 1, ButtonBorderStyle.Solid,
                    ColorPalette.BorderLight, 1, ButtonBorderStyle.Solid);
            }
        }
    }
}
