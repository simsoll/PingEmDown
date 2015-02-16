using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PingEmDown.Component;
using PingEmDown.Messaging.Caliburn.Micro;

namespace PingEmDown.Level
{
    public class Level : ILevel
    {
        public IEnumerable<IComponent> Walls { get; private set; }
        public IEnumerable<IComponent> Blocks { get; private set; }
        public IComponent Paddle { get; private set; }
        public IComponent Ball { get; private set; }

        public Level(IEnumerable<IComponent> walls, IEnumerable<IComponent> blocks, IComponent paddle, IComponent ball)
        {
            Walls = walls;
            Blocks = blocks;
            Paddle = paddle;
            Ball = ball;
        }

        public void Load()
        {
            foreach (var component in Blocks)
            {
                component.Load();
            }

            Paddle.Load();
            Ball.Load();
        }

        public void Unload()
        {
            Ball.Unload();
            Paddle.Unload();
            foreach (var component in Blocks)
            {
                component.Unload();
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (var block in Blocks)
            {
                block.Update(gameTime);
            }

            Paddle.Update(gameTime);
            Ball.Update(gameTime);
        }
    }
}
