using Microsoft.Xna.Framework;
using PingEmDown.Rectangle;

namespace PingEmDown.Components.Block
{
    public interface IBlock
    {
        IRectangle Boundings { get; }
        Color Color { get; }
        float Rotation { get; }
    }
}