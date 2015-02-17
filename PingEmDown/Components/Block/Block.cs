using Microsoft.Xna.Framework;
using PingEmDown.Messaging.Caliburn.Micro;

namespace PingEmDown.Components.Block
{
    public class Block : IBlock
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public Vector2 Position { get; set; }

        public Rectangle Boundings
        {
            get { return new Rectangle((int) Position.X, (int) Position.Y, Width, Height); }
        }

        public Block(int height, int width, Vector2 position, Color color, float rotation)
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
