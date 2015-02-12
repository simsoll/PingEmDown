using Microsoft.Xna.Framework;

namespace PingEmDown.Component
{
    public class Component : IComponent
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public Vector2 Position { get; set; }

        public Rectangle Boundings
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
            }
        }

        public Component(int height, int width, Vector2 position, Color color, float rotation)
        {
            Rotation = rotation;
            Color = color;
            Position = position;
            Width = width;
            Height = height;
        }

        public Color Color { get; set; }
        public float Rotation { get; set; }

        public void Update(GameTime gameTime)
        {
        }
    }
}