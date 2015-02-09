using Microsoft.Xna.Framework;

namespace PingEmDown.Pixel
{
    public class Pixel : IPixel
    {
        public int Height { get; private set; }
        public int Width { get; private set; }
        public Vector2 Position { get; private set; }

        public Rectangle BoundingRectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
            }
        }

        public Pixel(int height, int width, Vector2 position, Color color, float rotation)
        {
            Rotation = rotation;
            Color = color;
            Position = position;
            Width = width;
            Height = height;
        }

        public Color Color { get; private set; }
        public float Rotation { get; private set; }

        public void Update(GameTime gameTime)
        {
        }
    }
}