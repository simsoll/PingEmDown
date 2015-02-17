using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PingEmDown.Components.Ball;
using PingEmDown.Components.Block;
using PingEmDown.Components.Paddle;
using PingEmDown.Components.Wall;
using PingEmDown.Input.Messages;
using PingEmDown.Level.Messages;
using PingEmDown.Messaging.Caliburn.Micro;

namespace PingEmDown.Level
{
    public class Level : ILevel, IHandle<Move>
    {
        private readonly IEventAggregator _eventAggregator;
        public IEnumerable<IWall> Walls { get; private set; }
        public IEnumerable<IBlock> Blocks { get; private set; }
        public IPaddle Paddle { get; private set; }
        public IBall Ball { get; private set; }

        public Level(IEventAggregator eventAggregator, IEnumerable<IWall> walls, IEnumerable<IBlock> blocks,
            IPaddle paddle, IBall ball)
        {
            _eventAggregator = eventAggregator;
            Walls = walls;
            Blocks = blocks;
            Paddle = paddle;
            Ball = ball;
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
            Ball.Update(gameTime);
        }

        public void Handle(Move message)
        {
            Paddle.Move(message.Direction);

            _eventAggregator.Publish(new PaddleMoved
            {
                Paddle = Paddle
            });
        }
    }
}