using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PingEmDown.Configuration;
using PingEmDown.Input.Keyboard.Messages;
using PingEmDown.Messaging.Caliburn.Micro;
using PingEmDown.Screen.Messages;
using PingEmDown.Text;

namespace PingEmDown.Screen
{
    public class StartScreen : IScreen, IHandle<KeyPressed>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ITextDrawer _textDrawer;
        private readonly IScreenConfiguration _screenConfiguration;
        private readonly ITextConfiguration _textConfiguration;
        private readonly string _titleText;
        private readonly Vector2 _titleTextPosition;
        private readonly string _takeActionText;
        private bool _showTakeActionText;
        private readonly Vector2 _takActionTextPosition;
        private readonly TimeSpan _textBlickTimeSpan;
        private TimeSpan _timeSinceLastBlick;

        public StartScreen(IEventAggregator eventAggregator, ITextDrawer textDrawer,
            IScreenConfiguration screenConfiguration, ITextConfiguration textConfiguration)
        {
            _eventAggregator = eventAggregator;
            _textDrawer = textDrawer;
            _screenConfiguration = screenConfiguration;
            _textConfiguration = textConfiguration;

            _titleText = "Ping Em Down";
            _titleTextPosition = new Vector2(_screenConfiguration.ScreenWidth / 2.0f, _screenConfiguration.ScreenHeight / 4.0f);

            _takeActionText = "Press any key to start";
            _showTakeActionText = true;
            _takActionTextPosition = new Vector2(_screenConfiguration.ScreenWidth/2.0f,
                _screenConfiguration.ScreenHeight / 2.0f);

            _textBlickTimeSpan = TimeSpan.FromMilliseconds(750);
            _timeSinceLastBlick = TimeSpan.Zero;
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
            _timeSinceLastBlick += gameTime.ElapsedGameTime;

            if (_textBlickTimeSpan >= _timeSinceLastBlick) return;

            _showTakeActionText = !_showTakeActionText;
            _timeSinceLastBlick = TimeSpan.Zero;
        }

        public void Draw()
        {
            _textDrawer.DrawTextCentered(_titleText, _titleTextPosition, _textConfiguration.TextSize,
                _textConfiguration.TextColor);

            if (!_showTakeActionText) return;

            _textDrawer.DrawTextCentered(_takeActionText, _takActionTextPosition, _textConfiguration.TextSize,
                _textConfiguration.TextColor);
        }

        public void Handle(KeyPressed message)
        {
            _eventAggregator.Publish(new StartGame());
        }
    }
}