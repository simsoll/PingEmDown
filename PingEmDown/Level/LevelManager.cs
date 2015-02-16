using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PingEmDown.Draw;
using PingEmDown.Messaging.Caliburn.Micro;

namespace PingEmDown.Level
{
    public class LevelManager : ILevelManager
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ILevel _level;
        private readonly IDrawer _drawer;

        public LevelManager(IEventAggregator eventAggregator, ILevelFactory levelFactory, IDrawer drawer)
        {
            _eventAggregator = eventAggregator;
            _level = levelFactory.CreateLevel();
            _drawer = drawer;
        }

        public void Load()
        {
            _eventAggregator.Subscribe(this);
            _level.Load();
        }

        public void Unload()
        {
            _level.Unload();
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
                _drawer.Draw(wall);
            }

            foreach (var block in _level.Blocks)
            {
                _drawer.Draw(block);
            }

            _drawer.Draw(_level.Paddle);
            _drawer.Draw(_level.Ball);
        }
    }
}
