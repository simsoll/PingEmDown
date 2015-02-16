using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Xna.Framework;
using PingEmDown.Component;
using PingEmDown.Input.Messages;
using PingEmDown.Messaging.Caliburn.Micro;
using PingEmDown.Paddle.Messages;

namespace PingEmDown.Paddle
{
    public class Paddle : IComponent, IHandle<Move>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IComponent _component;

        private float maxSpeed = 5;

        public Paddle(IEventAggregator eventAggregator, IComponent component)
        {
            _eventAggregator = eventAggregator;
            _component = component;
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
            _component.Update(gameTime);
        }

        public int Height
        {
            get { return _component.Height; }
            set { _component.Height = value; }
        }

        public int Width
        {
            get { return _component.Width; }
            set { _component.Width = value; }
        }

        public Vector2 Position
        {
            get { return _component.Position; }
            set { _component.Position = value; }
        }

        public Rectangle Boundings
        {
            get { return _component.Boundings; }
        }

        public Color Color
        {
            get { return _component.Color; }
            set { _component.Color = value; }
        }

        public float Rotation
        {
            get { return _component.Rotation; }
            set { _component.Rotation = value; }
        }

        public void Handle(Move message)
        {
            Position += maxSpeed*message.Direction;

            _eventAggregator.Publish(new PaddleMoved
            {
                Paddle = this
            });
        }
    }
}
