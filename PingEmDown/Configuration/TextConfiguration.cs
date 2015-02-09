using Microsoft.Xna.Framework;

namespace PingEmDown.Configuration
{
    public class TextConfiguration : ITextConfiguration
    {
        public Color TextColor { get; private set; }
        public int TextSize { get; private set; }

        public TextConfiguration(Color textColor, int textSize)
        {
            TextSize = textSize;
            TextColor = textColor;
        }
    }
}