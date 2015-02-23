﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PingEmDown.Collision;
using PingEmDown.Draw;
using PingEmDown.Messaging.Caliburn.Micro;

namespace PingEmDown.Level
{
    public class LevelManager : ILevelManager
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ICollisionDetector _collisionDetector;
        private readonly ILevel _level;
        private readonly IDrawer _drawer;

        public LevelManager(IEventAggregator eventAggregator, ILevelFactory levelFactory, ICollisionDetector collisionDetector, IDrawer drawer)
        {
            _eventAggregator = eventAggregator;
            _collisionDetector = collisionDetector;
            _level = levelFactory.CreateLevel();
            _drawer = drawer;
        }

        public void Load()
        {
            _eventAggregator.Subscribe(this);
            _collisionDetector.Load();
            _level.Load();
        }

        public void Unload()
        {
            _level.Unload();
            _collisionDetector.Unload();
            _eventAggregator.Unsubscribe(this);
        }

        public void Update(GameTime gameTime)
        {
            _level.Update(gameTime);
        }

        public void Draw()
        {
            foreach (var wall in _level.Walls)
            {
                _drawer.Draw(wall.Boundings, wall.Color, wall.Rotation);
            }

            foreach (var block in _level.Blocks)
            {
                _drawer.Draw(block.Boundings, block.Color, block.Rotation);
            }

            _drawer.Draw(_level.Paddle.Boundings, _level.Paddle.Color, _level.Paddle.Rotation);
            _drawer.Draw(_level.Ball.Boundings, _level.Ball.Color, _level.Ball.Rotation);
        }
    }
}
