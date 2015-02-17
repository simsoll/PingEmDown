using Microsoft.Xna.Framework;

namespace PingEmDown.Components.Ball
{
    public interface IBall
    {
        int Height { get; }
        int Width { get; }
        Vector2 Position { get; }
        Rectangle Boundings { get; }
        Color Color { get; }
        float Rotation { get; }

        void Update(GameTime gameTime);
    }
}