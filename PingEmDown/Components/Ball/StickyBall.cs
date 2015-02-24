using Microsoft.Xna.Framework;
using PingEmDown.Components.Paddle;
using PingEmDown.Level.Messages;
using PingEmDown.Messaging.Caliburn.Micro;
using PingEmDown.Rectangle;

namespace PingEmDown.Components.Ball
{
    public class StickyBall : IBall, IHandle<PaddleMoved>
    {
        private readonly IEventAggregator _eventAggregator;

        public StickyBall(IEventAggregator eventAggregator, int height, int width, Vector2 position, Color color, float rotation)
        {
            _eventAggregator = eventAggregator;
            Rotation = rotation;
            Color = color;
            Position = position;
            Width = width;
            Height = height;

            Velocity = Vector2.Zero;
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Load()
        {
            _eventAggregator.Subscribe(this);
        }

        public void Unload()
        {
            _eventAggregator.Unsubscribe(this);
        }

        public int Height { get; private set; }

        public int Width { get; private set; }

        public Vector2 Position { get; set; }

        public Vector2 Velocity { get; set; }

        public IRectangle Boundings
        {
            get
            {
                return new Rectangle.Rectangle(Position.X, Position.Y, Width, Height);
            }
        }

        public Color Color { get; private set; }

        public float Rotation { get; private set; }
        
        public void Handle(PaddleMoved message)
        {
            var paddle = message.Paddle;

            var x = paddle.Boundings.X + paddle.Boundings.Width / 2.0f - Width / 2.0f;
            var y = paddle.Boundings.Y - Height;

            Position = new Vector2(x, y);
        }
    }
}
