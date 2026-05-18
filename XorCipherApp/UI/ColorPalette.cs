using System.Drawing;

namespace XorCipherApp.UI
{
    /// <summary>
    /// Color palette using grayscale tones
    /// </summary>
    public class ColorPalette
    {
        // Background colors
        public readonly Color BgLight;
        public readonly Color BgMedium;
        
        // Panel and surface colors
        public readonly Color SurfaceWhite;
        public readonly Color SurfaceLight;
        
        // Primary colors (grayscale)
        public readonly Color PrimaryDark;
        public readonly Color PrimaryMedium;
        public readonly Color PrimaryLight;
        
        // Accent colors
        public readonly Color AccentLight;
        
        // Text colors
        public readonly Color TextPrimary;
        public readonly Color TextSecondary;
        public readonly Color TextDisabled;
        
        // Status colors
        public readonly Color Success;
        public readonly Color Error;
        public readonly Color Info;
        
        // Border colors
        public readonly Color BorderLight;
        public readonly Color BorderMedium;

        public ColorPalette()
        {
            BgLight = Color.FromArgb(250, 250, 250);
            BgMedium = Color.FromArgb(245, 245, 245);
            SurfaceWhite = Color.FromArgb(255, 255, 255);
            SurfaceLight = Color.FromArgb(248, 248, 248);
            PrimaryDark = Color.FromArgb(64, 64, 64);
            PrimaryMedium = Color.FromArgb(96, 96, 96);
            PrimaryLight = Color.FromArgb(128, 128, 128);
            AccentLight = Color.FromArgb(180, 180, 180);
            TextPrimary = Color.FromArgb(33, 33, 33);
            TextSecondary = Color.FromArgb(97, 97, 97);
            TextDisabled = Color.FromArgb(158, 158, 158);
            Success = Color.FromArgb(76, 175, 80);
            Error = Color.FromArgb(229, 115, 115);
            Info = Color.FromArgb(96, 96, 96);
            BorderLight = Color.FromArgb(224, 224, 224);
            BorderMedium = Color.FromArgb(189, 189, 189);
        }
    }
}
