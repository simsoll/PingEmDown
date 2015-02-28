using System;
using System.Collections.Generic;
using System.Xml.Schema;
using Microsoft.Xna.Framework;
using PingEmDown.Collision;
using PingEmDown.Collision.Messages;
using PingEmDown.Components.Paddle.Messages;
using PingEmDown.Input.Messages;
using PingEmDown.Level.Messages;
using PingEmDown.Messaging.Caliburn.Micro;
using PingEmDown.Rectangle;

namespace PingEmDown.Components.Paddle
{
    public class Paddle : IPaddle, IHandle<Move>, IHandle<ReleaseBall>, IHandle<BallCollidedWithPaddle>
    {
        private readonly IEventAggregator _eventAggregator;
        private Vector2 previousPosition;

        private readonly float _speed = 250;
        private readonly float _movementTolerance = 0.05f;
        private readonly float _velocityDamping = 0.90f;

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
            var elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Velocity *= _velocityDamping;
            Position += Velocity * elapsed;

            if (Math.Abs(Velocity.X) > _movementTolerance)
            {
                _eventAggregator.Publish(new PaddleMoved
                {
                    Paddle = this
                });
            }
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

        public void Handle(Move message)
        {
            Velocity = _speed*message.Direction;
        }

        public void Handle(ReleaseBall message)
        {
            _eventAggregator.Publish(new ReleasingBall
            {
                Paddle = this
            });
        }

        public void Handle(BallCollidedWithPaddle message)
        {
            if (message.Paddle != this)
            {
                return;
            }

            var speed = message.Ball.Velocity.Length();
            var paddleHittingPointX = message.Ball.Position.X + message.Ball.Width/2.0f;

            var ballSpeed = message.Ball.Velocity.Length();

            var leftInterpolation = -ballSpeed;
            var rightInterpolation = ballSpeed;
            var newDirectionX = leftInterpolation +
                                (rightInterpolation - leftInterpolation)/Width*
                                (paddleHittingPointX - Position.X);

            message.Ball.Velocity = Vector2.Normalize(new Vector2(newDirectionX, message.Ball.Velocity.Y))*speed;
        }
    }
}
