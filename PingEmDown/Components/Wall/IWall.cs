using Microsoft.Xna.Framework;
using PingEmDown.Rectangle;

namespace PingEmDown.Components.Wall
{
    public interface IWall
    {
        IRectangle Boundings { get; }
        Color Color { get; }
        float Rotation { get; }
    }
}