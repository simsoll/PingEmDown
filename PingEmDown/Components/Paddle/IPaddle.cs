using Microsoft.Xna.Framework;

namespace PingEmDown.Components.Paddle
{
    public interface IPaddle
    {
        Rectangle Boundings { get; }
        Color Color { get; }
        float Rotation { get; }

        void Move(Vector2 direction);
    }
}