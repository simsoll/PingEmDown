using System.Collections.Generic;
using Microsoft.Xna.Framework;
using PingEmDown.Components.Ball;
using PingEmDown.Components.Block;
using PingEmDown.Components.Paddle;
using PingEmDown.Components.Wall;
using PingEmDown.Configuration;
using PingEmDown.Messaging.Caliburn.Micro;

namespace PingEmDown.Level
{
    public class LevelFactory : ILevelFactory
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IScreenConfiguration _screenConfiguration;
        private readonly ILevelConfiguration _levelConfiguration;

        public LevelFactory(IEventAggregator eventAggregator, IScreenConfiguration screenConfiguration,
            ILevelConfiguration levelConfiguration)
        {
            _eventAggregator = eventAggregator;
            _screenConfiguration = screenConfiguration;
            _levelConfiguration = levelConfiguration;
        }

        public ILevel CreateLevel()
        {
            var paddle = new Paddle(_eventAggregator,
                _levelConfiguration.PaddleHeight,
                _levelConfiguration.PaddleWidth,
                new Vector2(_screenConfiguration.ScreenWidth/2.0f - _levelConfiguration.PaddleWidth/2.0f,
                    _screenConfiguration.ScreenHeight - _levelConfiguration.PaddleHeight*2),
                _levelConfiguration.PaddleColor);

            var ball = new Ball(_eventAggregator,
                new StickyBall(_eventAggregator,
                    _levelConfiguration.BallSize,
                    _levelConfiguration.BallSize,
                    new Vector2(_screenConfiguration.ScreenWidth/2.0f - _levelConfiguration.BallSize/2.0f,
                        paddle.Position.Y - _levelConfiguration.BallSize),
                    _levelConfiguration.BallColor,
                    0.0f),
                new FlyingBall(_eventAggregator, 
                    _levelConfiguration.BallSize,
                    _levelConfiguration.BallSize,
                    new Vector2(_screenConfiguration.ScreenWidth/2.0f - _levelConfiguration.BallSize/2.0f,
                        paddle.Position.Y - _levelConfiguration.BallSize),
                    _levelConfiguration.BallColor,
                    0.0f,
                    20.0f));

            return new Level(
                _eventAggregator,
                CreateWalls(),
                CreateBlocks(),
                paddle,
                ball);
        }

        private IEnumerable<IWall> CreateWalls()
        {
            return new List<IWall>
            {
                new Wall(
                    _levelConfiguration.WallWidth,
                    _screenConfiguration.ScreenWidth,
                    Vector2.Zero,
                    _levelConfiguration.WallColor,
                    0.0f),
                new Wall(
                    _screenConfiguration.ScreenHeight - _levelConfiguration.WallWidth,
                    _levelConfiguration.WallWidth,
                    new Vector2(0, _levelConfiguration.WallWidth),
                    _levelConfiguration.WallColor,
                    0.0f),
                new Wall(
                    _screenConfiguration.ScreenHeight - _levelConfiguration.WallWidth,
                    _levelConfiguration.WallWidth,
                    new Vector2(_screenConfiguration.ScreenWidth - _levelConfiguration.WallWidth,
                        _levelConfiguration.WallWidth),
                    _levelConfiguration.WallColor,
                    0.0f)
            };
        }

        private IEnumerable<IBlock> CreateBlocks()
        {
            var block = new List<IBlock>();
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

                    block.Add(new Block(blockHeight, blockWidth, new Vector2(x, y), blockColor, 0.0f));
                }
            }

            return block;
        }
    }
}