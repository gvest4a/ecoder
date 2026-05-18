using System;
using System.Drawing;
using System.Windows.Forms;

namespace XorCipherApp.UI
{
    /// <summary>
    /// Panel with shadow effect for modern look
    /// </summary>
    public class ShadowPanel : Panel
    {
        public ShadowPanel()
        {
            BackColor = ColorPalette.SurfaceWhite;
            
            // Enable double buffering to reduce flicker
            SetStyle(ControlStyles.OptimizedDoubleBuffer | 
                     ControlStyles.AllPaintingInWmPaint | 
                     ControlStyles.UserPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
            // Draw subtle border
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle, 
                ColorPalette.BorderLight, ButtonBorderStyle.Solid);
            
            // Optional: Add subtle shadow effect
            using (var shadowBrush = new SolidBrush(Color.FromArgb(20, 0, 0, 0)))
            {
                // Top shadow
                e.Graphics.FillRectangle(shadowBrush, 0, 0, Width, 2);
                // Left shadow
                e.Graphics.FillRectangle(shadowBrush, 0, 0, 2, Height);
            }
        }
    }
}
