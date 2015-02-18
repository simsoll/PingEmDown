using Microsoft.Xna.Framework;
using PingEmDown.Components.Paddle;
using PingEmDown.Messaging.Caliburn.Micro;

namespace PingEmDown.Components.Ball
{
    public class FlyingBall : IBall
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly float _speed = 3;

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

        public Vector2 Velocity { get { return _speed * Direction; } }

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