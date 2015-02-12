using System.Collections.Generic;
using System.Security.Cryptography;
using Microsoft.Xna.Framework;
using PingEmDown.Component;
using PingEmDown.Configuration;

namespace PingEmDown.Level
{
    public class LevelFactory : ILevelFactory
    {
        private readonly IScreenConfiguration _screenConfiguration;
        private readonly ILevelConfiguration _levelConfiguration;

        public LevelFactory(IScreenConfiguration screenConfiguration, ILevelConfiguration levelConfiguration)
        {
            _screenConfiguration = screenConfiguration;
            _levelConfiguration = levelConfiguration;
        }

        public ILevel CreateLevel()
        {
            return new Level(
                CreateWalls(),
                CreateBlocks(),
                new Paddle.Paddle(new Component.Component(0, 0, Vector2.One*10, Color.Black, 0.0f)),
                new Ball.Ball(new Component.Component(0, 0, Vector2.One, Color.Black, 0.0f)));
        }

        private IEnumerable<IComponent> CreateWalls()
        {
            return new List<IComponent>
            {
                new Component.Component(
                    _levelConfiguration.WallWidth,
                    _screenConfiguration.ScreenWidth,
                    Vector2.Zero,
                    _levelConfiguration.WallColor,
                    0.0f),
                new Component.Component(
                    _screenConfiguration.ScreenHeight - _levelConfiguration.WallWidth,
                    _levelConfiguration.WallWidth,
                    new Vector2(0, _levelConfiguration.WallWidth),
                    _levelConfiguration.WallColor,
                    0.0f),
                new Component.Component(
                    _screenConfiguration.ScreenHeight - _levelConfiguration.WallWidth,
                    _levelConfiguration.WallWidth,
                    new Vector2(_screenConfiguration.ScreenWidth - _levelConfiguration.WallWidth,
                        _levelConfiguration.WallWidth),
                    _levelConfiguration.WallColor,
                    0.0f)
            };
        }

        private IEnumerable<IComponent> CreateBlocks()
        {
            var block = new List<IComponent>();
            var screenWidth = _screenConfiguration.ScreenWidth;
            var wallWidth = _levelConfiguration.WallWidth;
            var blockWallOffset = _levelConfiguration.BlockWallOffset;
            var blockBlockOffset = _levelConfiguration.BlockBlockOffset;
            var blockWidth = _levelConfiguration.BlockWidth;

            for (var x = wallWidth + blockWallOffset; x < screenWidth - wallWidth - blockWallOffset; x += blockWidth + blockBlockOffset)
            {
                for (var y)
            }

            return new List<IComponent>();
        }
    }
}