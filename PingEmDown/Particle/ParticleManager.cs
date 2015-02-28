using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PingEmDown.Collision.Messages;
using PingEmDown.Components.Ball.Messages;
using PingEmDown.Draw;
using PingEmDown.Messaging.Caliburn.Micro;
using PingEmDown.Randomizer;
using PingEmDown.Rectangle;

namespace PingEmDown.Particle
{
    public class ParticleManager : IParticleManager, IHandle<BallMoved>, IHandle<BallCollidedWithBlock>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IDrawer _drawer;
        private readonly IRandomizer _randomizer;

        private IList<Particle> _particles;

        public ParticleManager(IEventAggregator eventAggregator, IDrawer drawer, IRandomizer randomizer)
        {
            _eventAggregator = eventAggregator;
            _drawer = drawer;
            _randomizer = randomizer;
            _particles = new List<Particle>();
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
            for (var particle = 0; particle < _particles.Count; particle++)
            {
                _particles[particle].Update(gameTime);

                if (_particles[particle].IsDead())
                {
                    _particles.RemoveAt(particle);
                    particle--;
                }
            }
        }

        public void Draw()
        {
            foreach (var particle in _particles)
            {
                _drawer.Draw(particle.Boundings, particle.Color, particle.Rotation);
            }
        }

        private Particle GenerateNewParticleWithRandomVelocity(Vector2 position, float height, float width,
            Vector2 gravity, Color[] colors, TimeSpan lifeTime)
        {
            var velocity = new Vector2(
                75f*(_randomizer.NextDouble()*2 - 1),
                75f*(_randomizer.NextDouble()*2 - 1));

            return GenerateNewParticle(position, height, width, velocity, gravity, colors, lifeTime);
        }

        private Particle GenerateNewParticle(Vector2 position, float height, float width, Vector2 velocity,
            Vector2 gravity, Color[] colors, TimeSpan lifeTime)
        {
            var angle = 0;
            var angularVelocity = 10f*(_randomizer.NextDouble()*2 - 1);
            var color = colors[_randomizer.Next(colors.Length)];

            return new Particle(position, height, width, velocity, angle, angularVelocity, color, gravity, lifeTime);
        }

        public void Handle(BallMoved message)
        {
            var numberOfParticles = _randomizer.Next(5);

            for (var i = 0; i < numberOfParticles; i++)
            {
                var size = _randomizer.NextDouble()*2.0f;

                _particles.Add(
                    GenerateNewParticle(
                        message.Ball.Position +
                        new Vector2(_randomizer.NextDouble()*message.Ball.Width*0.9f,
                            _randomizer.NextDouble()*message.Ball.Height*0.9f),
                        size,
                        size,
                        message.Ball.Velocity*-0.001f,
                        Vector2.Zero,
                        new[] {Color.Black},
                        TimeSpan.FromMilliseconds(200 + _randomizer.Next(600))));
            }
        }

        public void Handle(BallCollidedWithBlock message)
        {
            var particleRows = 2;
            var particleColumns = 10;

            var particleWidth = message.Block.Boundings.Width/particleColumns;
            var particleHeight = message.Block.Boundings.Height/particleRows;

            var gravity = new Vector2(0, 100);

            for (var row = 0; row < particleRows; row++)
            {
                for (var column = 0; column < particleColumns; column++)
                {
                    var position = message.Block.Boundings.Position +
                                   new Vector2(column * particleWidth, row * particleHeight);

                    _particles.Add(GenerateNewParticleWithRandomVelocity(position, particleHeight, particleWidth,
                        gravity, new[] {Color.Black},
                        TimeSpan.FromMilliseconds(5000)));
                }
            }
        }
    }
}