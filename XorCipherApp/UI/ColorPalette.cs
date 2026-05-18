using System.Drawing;

namespace XorCipherApp.UI
{
    /// <summary>
    /// Color palette using grayscale tones
    /// </summary>
    public static class ColorPalette
    {
        // Background colors
        public static readonly Color BgLight = Color.FromArgb(250, 250, 250);
        public static readonly Color BgMedium = Color.FromArgb(245, 245, 245);
        
        // Panel and surface colors
        public static readonly Color SurfaceWhite = Color.FromArgb(255, 255, 255);
        public static readonly Color SurfaceLight = Color.FromArgb(248, 248, 248);
        
        // Primary colors (grayscale)
        public static readonly Color PrimaryDark = Color.FromArgb(64, 64, 64);
        public static readonly Color PrimaryMedium = Color.FromArgb(96, 96, 96);
        public static readonly Color PrimaryLight = Color.FromArgb(128, 128, 128);
        public static readonly Color AccentLight = Color.FromArgb(180, 180, 180);
        
        // Text colors
        public static readonly Color TextPrimary = Color.FromArgb(33, 33, 33);
        public static readonly Color TextSecondary = Color.FromArgb(97, 97, 97);
        public static readonly Color TextDisabled = Color.FromArgb(158, 158, 158);
        
        // Status colors
        public static readonly Color Success = Color.FromArgb(76, 175, 80);
        public static readonly Color Error = Color.FromArgb(229, 115, 115);
        public static readonly Color Info = Color.FromArgb(96, 96, 96);
        
        // Border colors
        public static readonly Color BorderLight = Color.FromArgb(224, 224, 224);
        public static readonly Color BorderMedium = Color.FromArgb(189, 189, 189);
        public static readonly Color BorderFocus = Color.FromArgb(128, 128, 128);
    }
}
