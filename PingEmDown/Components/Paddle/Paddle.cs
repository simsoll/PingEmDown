using Microsoft.Xna.Framework;
using PingEmDown.Input.Messages;
using PingEmDown.Level.Messages;
using PingEmDown.Messaging.Caliburn.Micro;

namespace PingEmDown.Components.Paddle
{
    public class Paddle : IPaddle
    {
        private readonly IEventAggregator _eventAggregator;

        private const float MaxSpeed = 5;

        public Paddle(IEventAggregator eventAggregator, int height, int width, Vector2 position, Color color)
        {
            _eventAggregator = eventAggregator;

            Color = color;
            Position = position;
            Width = width;
            Height = height;
        }


        public void Load()
        {
            _eventAggregator.Subscribe(this);
        }

        public void Unload()
        {
            _eventAggregator.Unsubscribe(this);
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

        public void Move(Vector2 direction)
        {
            Position += MaxSpeed*direction;
        }
    }
}
