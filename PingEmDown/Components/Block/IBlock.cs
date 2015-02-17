using Microsoft.Xna.Framework;

namespace PingEmDown.Components.Block
{
    public interface IBlock
    {
        Rectangle Boundings { get; }
        Color Color { get; }
        float Rotation { get; }
    }
}