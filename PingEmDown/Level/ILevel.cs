using System.Collections.Generic;
using Microsoft.Xna.Framework;
using PingEmDown.Components.Ball;
using PingEmDown.Components.Block;
using PingEmDown.Components.Paddle;
using PingEmDown.Components.Wall;

namespace PingEmDown.Level
{
    public interface ILevel
    {
        IEnumerable<IWall> Walls { get; }
        IEnumerable<IBlock> Blocks { get; }
        IPaddle Paddle { get; }
        IBall Ball { get; }
        void Load();
        void Unload();
        void Update(GameTime gameTime);
    }
}