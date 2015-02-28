using Microsoft.Xna.Framework;
using PingEmDown.Components.Paddle;
using PingEmDown.Components.Paddle.Messages;
using PingEmDown.Input.Messages;
using PingEmDown.Level.Messages;
using PingEmDown.Messaging.Caliburn.Micro;
using PingEmDown.Rectangle;

namespace PingEmDown.Components.Ball
{
    public class Ball : IBall, IHandle<ReleasingBall>
    {
        private readonly IEventAggregator _eventAggregator;
        private IBall _stickyState;
        private IBall _flyingState;
        private float _ballSpeed = 150;

        private IBall _currentState;

        public Ball(IEventAggregator eventAggregator, IBall stickyState, IBall flyingState)
        {
            _eventAggregator = eventAggregator;
            _stickyState = stickyState;
            _flyingState = flyingState;

            _currentState = _stickyState;
        }

        private void SetState(IBall state)
        {
            if (state == null)
            {
                return;
            }

            if (_currentState != null)
            {
                _currentState.Unload();
            }

            _currentState = state;
            _currentState.Load();

        }

        public void Update(GameTime gameTime)
        {
            _currentState.Update(gameTime);
        }

        public void Load()
        {
            _eventAggregator.Subscribe(this);
            _currentState.Load();
        }

        public void Unload()
        {
            _currentState.Unload();
            _eventAggregator.Unsubscribe(this);
        }

        public int Height
        {
            get { return _currentState.Height; }
        }

        public int Width
        {
            get { return _currentState.Width; }
        }

        public Vector2 Position
        {
            get { return _currentState.Position; }
            set { _currentState.Position = value; }
        }

        public Vector2 Velocity
        {
            get { return _currentState.Velocity; }
            set { _currentState.Velocity = value; }
        }

        public IRectangle Boundings
        {
            get { return _currentState.Boundings; }
        }

        public IRectangle BoundingsLastFrame
        {
            get { return _currentState.BoundingsLastFrame; }
        }

        public Color Color
        {
            get { return _currentState.Color; }
        }

        public float Rotation
        {
            get { return _currentState.Rotation; }
        }

        public void Handle(ReleasingBall message)
        {
            SetState(_flyingState);

            var paddle = message.Paddle;

            var x = paddle.Boundings.X + paddle.Boundings.Width / 2.0f - Width / 2.0f;
            var y = paddle.Boundings.Y - Height;

            _currentState.Position = new Vector2(x, y);
            _currentState.Velocity = Vector2.Normalize(paddle.Velocity + new Vector2(0, -1) * _ballSpeed) * _ballSpeed;
        }
    }
}
