using Microsoft.Xna.Framework;
using PingEmDown.Messaging.Caliburn.Micro;

namespace PingEmDown.Components.Ball
{
    public class AttachedBall : IBall
    {
        public AttachedBall(int height, int width, Vector2 position, Color color, float rotation)
        {
            Rotation = rotation;
            Color = color;
            Position = position;
            Width = width;
            Height = height;
        }

        public void Update(GameTime gameTime)
        {
        }

        public int Height { get; private set; }

        public int Width { get; private set; }

        public Vector2 Position { get; private set; }

        public Rectangle Boundings
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
            }
        }

        public Color Color { get; private set; }

        public float Rotation { get; private set; }
    }
}
