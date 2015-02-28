using Microsoft.Xna.Framework;
using PingEmDown.Rectangle;

namespace PingEmDown.Particle
{
    public interface IParticle
    {
        IRectangle Boundings { get; }
        float Rotation { get; set; }
        Color Color { get; set; }
    }
}