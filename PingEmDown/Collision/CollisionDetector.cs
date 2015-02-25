using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using MathNet.Numerics.LinearAlgebra.Double;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PingEmDown.Collision.Messages;
using PingEmDown.Components.Ball;
using PingEmDown.Components.Ball.Messages;
using PingEmDown.Components.Block;
using PingEmDown.Components.Paddle;
using PingEmDown.Components.Wall;
using PingEmDown.Extensions;
using PingEmDown.Level;
using PingEmDown.Level.Messages;
using PingEmDown.Messaging.Caliburn.Micro;
using PingEmDown.Rectangle;
using Tao.Sdl;
using Matrix = Microsoft.Xna.Framework.Matrix;

namespace PingEmDown.Collision
{
    public class CollisionDetector : ICollisionDetector, IHandle<PaddleMoved>, IHandle<BallMoved>, IHandle<LevelLoaded>, IHandle<LevelUnloaded>
    {
        private readonly IEventAggregator _eventAggregator;
        private ILevel _level;

        public CollisionDetector(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void Handle(PaddleMoved message)
        {
            var paddle = _level.Paddle;
            var walls = _level.Walls;

            if (walls.Any(wall => paddle.Boundings.Intersects(wall.Boundings)))
            {
                var wall = walls.First(w => paddle.Boundings.Intersects(w.Boundings));

                var paddleBoundingsLastFrame = new Rectangle.Rectangle(paddle.Boundings.X - paddle.Velocity.X,
                    paddle.Boundings.Y - paddle.Velocity.Y, paddle.Boundings.Width, paddle.Boundings.Height);

                var collisionPenetration = CollisionPenetration(wall.Boundings, paddle.Boundings,
                    paddleBoundingsLastFrame);

                paddle.Position -= collisionPenetration.Depth;
                paddle.Velocity = Vector2.Zero;
            }
        }

        public void Handle(BallMoved message)
        {
            CheckForBallCollisionWithBlocks(_level.Ball, _level.Blocks);
            CheckForBallCollisionWithWalls(_level.Ball, _level.Walls);
            CheckForBallCollisionWithPaddle(_level.Ball, _level.Paddle);
        }

        private void CheckForBallCollisionWithPaddle(IBall ball, IPaddle paddle)
        {
            if (!ball.Boundings.Intersects(paddle.Boundings))
            {
                return;
            }

            var collisionPenetration = CollisionPenetration(paddle.Boundings, ball.Boundings,
                ball.BoundingsLastFrame);

            ball.Position -= collisionPenetration.Depth;
            switch (collisionPenetration.From)
            {
                case Direction.Left:
                case Direction.Right:
                    ball.Velocity *= new Vector2(-1, 1);
                    break;
                case Direction.Top:
                case Direction.Bottom:
                    ball.Velocity *= new Vector2(1, -1);
                    break;
            }

            _eventAggregator.Publish(new BallCollidedWithPaddle()
            {
                Ball = ball,
                Paddle = paddle
            });
        }

        private void CheckForBallCollisionWithWalls(IBall ball, IEnumerable<IWall> walls)
        {
            var wall = walls.FirstOrDefault(w => ball.Boundings.Intersects(w.Boundings));

            if (wall == null)
            {
                return;
            }

            var collisionPenetration = CollisionPenetration(wall.Boundings, ball.Boundings,
                ball.BoundingsLastFrame);

            ball.Position -= collisionPenetration.Depth;
            switch (collisionPenetration.From)
            {
                case Direction.Left:
                case Direction.Right:
                    ball.Velocity *= new Vector2(-1, 1);
                    break;
                case Direction.Top:
                case Direction.Bottom:
                    ball.Velocity *= new Vector2(1, -1);
                    break;
            }

            _eventAggregator.Publish(new BallCollidedWithWall
            {
                Ball = ball,
                Wall = wall
            });
        }

        private void CheckForBallCollisionWithBlocks(IBall ball, IEnumerable<IBlock> blocks)
        {
            var block = blocks.FirstOrDefault(b => ball.Boundings.Intersects(b.Boundings));

            if (block == null)
            {
                return;
            }

            var collisionPenetration = CollisionPenetration(block.Boundings, ball.Boundings,
                ball.BoundingsLastFrame);

            ball.Position -= collisionPenetration.Depth;
            switch (collisionPenetration.From)
            {
                case Direction.Left:
                case Direction.Right:
                    ball.Velocity *= new Vector2(-1, 1);
                    break;
                case Direction.Top:
                case Direction.Bottom:
                    ball.Velocity *= new Vector2(1, -1);
                    break;
            }

            _eventAggregator.Publish(new BallCollidedWithBlock()
            {
                Ball = ball,
                Block = block
            });
        }

        private CollisionPenetration CollisionPenetration(
            IRectangle struckObject, 
            IRectangle movingObject,
            IRectangle movingObjectLastFrame)
        {
            var penetrations = new List<CollisionPenetration>();

            if (movingObject.Left <= struckObject.Right && struckObject.Right <= movingObjectLastFrame.Left)
            {
                penetrations.Add(new CollisionPenetration
                {
                    Depth = new Vector2(movingObject.Left - struckObject.Right, 0),
                    From = Direction.Left
                });
            }
            if (movingObjectLastFrame.Right <= struckObject.Left && struckObject.Left <= movingObject.Right)
            {
                penetrations.Add(new CollisionPenetration
                {
                    Depth = new Vector2(movingObject.Right - struckObject.Left, 0),
                    From = Direction.Right
                });
            }
            if (movingObjectLastFrame.Bottom <= struckObject.Top && struckObject.Top <= movingObject.Bottom)
            {
                penetrations.Add(new CollisionPenetration
                {
                    Depth = new Vector2(0, movingObject.Bottom - struckObject.Top),
                    From = Direction.Top
                });
            }
            if (movingObject.Top <= struckObject.Bottom && struckObject.Bottom <= movingObjectLastFrame.Top)
            {
                penetrations.Add(new CollisionPenetration
                {
                    Depth = new Vector2(0, movingObject.Top - struckObject.Bottom),
                    From = Direction.Bottom
                });
            }

            if (!penetrations.Any())
            {
                return new CollisionPenetration
                {
                    Depth = Vector2.Zero,
                    From = Direction.Unknown
                };
            }

            return penetrations.OrderBy(p => Math.Abs(p.Depth.X) + Math.Abs(p.Depth.Y)).FirstOrDefault();
        }

        public void Handle(LevelLoaded message)
        {
            _level = message.Level;
        }

        public void Handle(LevelUnloaded message)
        {
            if (_level.Name == message.Level.Name)
            {
                _level = null;
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

    public class CollisionPenetration
    {
        public Vector2 Depth { get; set; }
        public Direction From { get; set; }
    }

    public enum Direction
    {
        Unknown = 0,
        Left,
        Right,
        Top,
        Bottom
    }
}
