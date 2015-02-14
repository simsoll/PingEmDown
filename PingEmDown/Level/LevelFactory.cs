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
            var paddle = new Paddle.Paddle(new Component.Component(
                _levelConfiguration.PaddleHeight,
                _levelConfiguration.PaddleWidth,
                new Vector2(_screenConfiguration.ScreenWidth/2.0f - _levelConfiguration.PaddleWidth/2.0f,
                    _screenConfiguration.ScreenHeight - _levelConfiguration.PaddleHeight*2),
                _levelConfiguration.PaddleColor,
                0.0f));

            var ball = new Ball.Ball(new Component.Component(
                _levelConfiguration.BallSize,
                _levelConfiguration.BallSize,
                new Vector2(_screenConfiguration.ScreenWidth/2.0f - _levelConfiguration.BallSize/2.0f,
                    paddle.Position.Y - _levelConfiguration.BallSize),
                _levelConfiguration.BallColor,
                0.0f));

            return new Level(
                CreateWalls(),
                CreateBlocks(),
                paddle,
                ball);
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
            var blockHeight = _levelConfiguration.BlockHeight;
            var blockRows = _levelConfiguration.BlockRows;
            var blockColor = _levelConfiguration.BlockColor;

            for (var x = wallWidth + blockWallOffset;
                x < screenWidth - wallWidth - blockWallOffset;
                x += blockWidth + blockBlockOffset)
            {
                for (var row = 0; row < blockRows; row++)
                {
                    var y = wallWidth + blockWallOffset + row*(blockHeight + blockBlockOffset);

                    block.Add(new Component.Component(blockHeight, blockWidth, new Vector2(x, y), blockColor, 0.0f));
                }
            }

            return block;
        }
    }
}