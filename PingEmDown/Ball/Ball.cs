using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PingEmDown.Component;

namespace PingEmDown.Ball
{
    public class Ball : IComponent
    {
        private readonly IComponent _component;

        public Ball(IComponent component)
        {
            _component = component;
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
    }
}
