using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PingEmDown.Collision;
using PingEmDown.Collision.Messages;
using PingEmDown.Components.Ball;
using PingEmDown.Components.Block;
using PingEmDown.Components.Paddle;
using PingEmDown.Components.Wall;
using PingEmDown.Input.Messages;
using PingEmDown.Level.Messages;
using PingEmDown.Messaging.Caliburn.Micro;

namespace PingEmDown.Level
{
    public class Level : ILevel, IHandle<BallCollidedWithBlock>
    {
        private readonly IEventAggregator _eventAggregator;
        public string Name { get { return "Level 1"; } }
        public IEnumerable<IWall> Walls { get; private set; }
        public IList<IBlock> Blocks { get; private set; }
        public IPaddle Paddle { get; private set; }
        public IBall Ball { get; private set; }

        public Level(IEventAggregator eventAggregator, IEnumerable<IWall> walls, IEnumerable<IBlock> blocks,
            IPaddle paddle, IBall ball)
        {
            _eventAggregator = eventAggregator;
            Walls = walls;
            Blocks = blocks.ToList();
            Paddle = paddle;
            Ball = ball;
        }

        public void Load()
        {
            _eventAggregator.Subscribe(this);
            Paddle.Load();
            Ball.Load();

            _eventAggregator.Publish(new LevelLoaded
            {
                Level = this
            });
        }

        public void Unload()
        {
            Ball.Unload();
            Paddle.Unload();
            _eventAggregator.Unsubscribe(this);

            _eventAggregator.Publish(new LevelLoaded
            {
                Level = this
            });
        }

        public void Update(GameTime gameTime)
        {
            Paddle.Update(gameTime);
            Ball.Update(gameTime);
        }

        public void Handle(BallCollidedWithBlock message)
        {
            Blocks.Remove(message.Block);
        }
    }
}