using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using PingEmDown.Components.Paddle.Messages;
using PingEmDown.Input.Messages;
using PingEmDown.Level.Messages;
using PingEmDown.Messaging.Caliburn.Micro;
using PingEmDown.Rectangle;

namespace PingEmDown.Components.Paddle
{
    public class Paddle : IPaddle, IHandle<Move>, IHandle<ReleaseBall>
    {
        private readonly IEventAggregator _eventAggregator;
        private Vector2 previousPosition;

        private readonly float _speed = 5;
        private readonly float _movementTolerance = 0.05f;
        private readonly float _velocityDamping = 0.95f;

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
            Velocity *= _velocityDamping;
            Position += Velocity;

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
    }
}
