using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PingEmDown.Input.Keyboard.Messages;
using PingEmDown.Input.Messages;
using PingEmDown.Messaging.Caliburn.Micro;

namespace PingEmDown.Input
{
    public class PlayerInput : IHandle<KeyPressed>, IHandle<KeyHeld>
    {
        private readonly IEventAggregator _eventAggregator;

        public PlayerInput(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void Handle(KeyPressed message)
        {
            HandleKey(message.Key);
        }

        public void Handle(KeyHeld message)
        {
            HandleKey(message.Key);
        }

        public void HandleKey(Keys key)
        {
            //TODO: get mapping from configuration
            switch (key)
            {
                case Keys.Left:
                    _eventAggregator.Publish(new Move { Direction = new Vector2(-1, 0) });
                    break;
                case Keys.Right:
                    _eventAggregator.Publish(new Move { Direction = new Vector2(1, 0) });
                    break;
                case Keys.Up:
                case Keys.Space:
                    _eventAggregator.Publish(new ReleaseBall());
                    break;
            }
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
