using Microsoft.Xna.Framework;
using PingEmDown.Components.Ball.Messages;
using PingEmDown.Components.Paddle;
using PingEmDown.Messaging.Caliburn.Micro;
using PingEmDown.Rectangle;

namespace PingEmDown.Components.Ball
{
    public class FlyingBall : IBall
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly float _speed = 6;

        public FlyingBall(IEventAggregator eventAggregator, int height, int width, Vector2 position, Color color, float rotation)
        {
            _eventAggregator = eventAggregator;
            Rotation = rotation;
            Color = color;
            Position = position;
            Width = width;
            Height = height;

            Direction = new Vector2(0, -1);
        }

        public void Update(GameTime gameTime)
        {
            Position += Velocity;

            _eventAggregator.Publish(new BallMoved
            {
                Ball = this
            });
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

        public Vector2 Direction { get; set; }

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
    }
}