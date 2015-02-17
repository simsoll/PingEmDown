using Microsoft.Xna.Framework;
using PingEmDown.Input.Messages;
using PingEmDown.Messaging.Caliburn.Micro;

namespace PingEmDown.Components.Ball
{
    public class Ball : IBall, IHandle<ReleaseBall>
    {
        private IBall _currentState;
        private IBall _attachedState;
        private IBall _releasedState;

        public Ball(IBall attachedState, IBall releasedState)
        {
            _attachedState = attachedState;
            _releasedState = releasedState;

            _currentState = _attachedState;
        }

        private void SetState(IBall state)
        {
            _currentState = state;
        }

        public void Update(GameTime gameTime)
        {
            _currentState.Update(gameTime);
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
        }

        public Rectangle Boundings
        {
            get { return _currentState.Boundings; }
        }

        public Color Color
        {
            get { return _currentState.Color; }
        }

        public float Rotation
        {
            get { return _currentState.Rotation; }
        }

        public void Handle(ReleaseBall message)
        {
            SetState(_releasedState);
        }
    }
}
