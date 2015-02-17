using Microsoft.Xna.Framework;

namespace PingEmDown.Components.Wall
{
    public interface IWall
    {
        Rectangle Boundings { get; }
        Color Color { get; }
        float Rotation { get; }
    }
}