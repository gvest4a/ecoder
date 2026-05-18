using System.Windows.Forms;

namespace XorCipherApp.UI
{
    /// <summary>
    /// Base form with common functionality and responsive design support
    /// </summary>
    public class ResponsiveForm : Form
    {
        protected float ScaleFactor { get; private set; } = 1.0f;
        protected SizeF OriginalDesignSize { get; } = new SizeF(1100, 700);

        public ResponsiveForm()
        {
            this.Resize += ResponsiveForm_Resize;
            ApplyResponsiveDesign();
        }

        private void ResponsiveForm_Resize(object? sender, EventArgs e)
        {
            ApplyResponsiveDesign();
        }

        protected virtual void ApplyResponsiveDesign()
        {
            // Calculate scale factor based on current screen size
            var screenSize = Screen.FromControl(this).WorkingArea.Size;
            ScaleFactor = Math.Min(
                Math.Min(screenSize.Width / OriginalDesignSize.Width, 1.5f),
                Math.Min(screenSize.Height / OriginalDesignSize.Height, 1.5f)
            );
            
            // Minimum scale to prevent content from becoming too small
            ScaleFactor = Math.Max(ScaleFactor, 0.8f);
            
            OnScaleChanged(new EventArgs());
        }

        protected virtual void OnScaleChanged(EventArgs e)
        {
            // Override in derived classes to apply scaling to controls
        }

        protected Point ScalePoint(Point original)
        {
            return new Point(
                (int)(original.X * ScaleFactor),
                (int)(original.Y * ScaleFactor)
            );
        }

        protected Size ScaleSize(Size original)
        {
            return new Size(
                (int)(original.Width * ScaleFactor),
                (int)(original.Height * ScaleFactor)
            );
        }

        protected Font ScaleFont(Font original)
        {
            return new Font(original.FontFamily, original.Size * ScaleFactor, original.Style);
        }
    }
}
