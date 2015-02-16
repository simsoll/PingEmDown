using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using PingEmDown.Component;

namespace PingEmDown.Level
{
    public interface ILevel
    {
        IEnumerable<IComponent> Walls { get; }
        IEnumerable<IComponent> Blocks { get; }
        IComponent Paddle { get; }
        IComponent Ball { get; }
        void Load();
        void Unload();
        void Update(GameTime gameTime);
    }
}