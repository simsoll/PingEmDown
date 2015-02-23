using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using MathNet.Numerics.LinearAlgebra.Double;
using Microsoft.Xna.Framework;
using PingEmDown.Components.Ball.Messages;
using PingEmDown.Components.Wall;
using PingEmDown.Extensions;
using PingEmDown.Level;
using PingEmDown.Level.Messages;
using PingEmDown.Messaging.Caliburn.Micro;
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

                var direction = Vector2.Normalize(paddle.Velocity);

                paddle.Position += CollisionPenetration(wall.Boundings, paddle.Boundings, direction);
                paddle.Velocity = Vector2.Zero;
            }
        }

        public void Handle(BallMoved message)
        {
            var ball = _level.Ball;
            var walls = _level.Walls;

            if (walls.Any(wall => ball.Boundings.Intersects(wall.Boundings)))
            {
                var wall = walls.First(w => ball.Boundings.Intersects(w.Boundings));

                var direction = Vector2.Normalize(ball.Velocity);

                ball.Position += CollisionPenetration(wall.Boundings, ball.Boundings, direction);
                ball.Velocity = Vector2.Zero;
            }
        }

        private Vector2 CollisionPenetration(Rectangle struckObject, Rectangle movingObject, Vector2 direction)
        {
            var perpendicularDirection = Vector2.Normalize(new Vector2(-direction.Y, direction.X));

            var projectionMatrix = ProjectionMatrix(perpendicularDirection);

            var movingProjectedEdges = ProjectRectangle(movingObject, projectionMatrix);
            var struckProjectedEdges = ProjectRectangle(struckObject, projectionMatrix);

            if (direction.X < 0)
            {
                var movingProjectionPoint = movingProjectedEdges.OrderBy(x => x.X).First();
                var struckProjectionPoint = struckProjectedEdges.OrderByDescending(x => x.X).First();

                return struckProjectionPoint - movingProjectionPoint;
            }
            
            if (direction.X > 0)
            {
                var movingProjectionPoint = movingProjectedEdges.OrderByDescending(x => x.X).First();
                var struckProjectionPoint = struckProjectedEdges.OrderBy(x => x.X).First();

                return struckProjectionPoint - movingProjectionPoint;
            }

            return Vector2.Zero;
        }

        private IEnumerable<Vector2> ProjectRectangle(Rectangle rectangle, DenseMatrix projectionMatrix)
        {
            var projectedEdges = new List<Vector2>();

            foreach (var edge in rectangle.Edges())
            {
                var edgeAsDenseVector = new DenseVector(new double[] { edge.X, edge.Y });
                var projectedEdge = projectionMatrix * edgeAsDenseVector;

                projectedEdges.Add(new Vector2((float)projectedEdge[0], (float)projectedEdge[1]));
            }

            return projectedEdges;
        }

        private DenseMatrix ProjectionMatrix(Vector2 perpendicular)
        {
            return new DenseMatrix(2, 2,
                new[] 
                {
                    1.0 - perpendicular.X*perpendicular.X, 
                    -perpendicular.X*perpendicular.Y, 
                    -perpendicular.X*perpendicular.Y, 
                    1 - perpendicular.Y*perpendicular.Y
                });
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
}
