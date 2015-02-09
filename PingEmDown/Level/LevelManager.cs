using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PingEmDown.Messaging.Caliburn.Micro;

namespace PingEmDown.Level
{
    public class LevelManager
    {
        private readonly IEventAggregator _eventAggregator;

        public LevelManager(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void Load()
        {
            _eventAggregator.Subscribe(this);
        }

        public void Unload()
        {
            _eventAggregator.Unsubscribe(this);
        }
    }
}
