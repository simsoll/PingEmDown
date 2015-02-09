using Microsoft.Xna.Framework;
using PingEmDown.Draw;

namespace PingEmDown.Pixel
{
    public interface IPixel
    {
        int Height { get; }
        int Width { get; }
        Vector2 Position { get; }
        Rectangle BoundingRectangle { get; }
        Color Color { get; }
        float Rotation { get; }

        void Update(GameTime gameTime);
    }
}