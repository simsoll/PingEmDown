using Microsoft.Xna.Framework;
using PingEmDown.Components.Paddle;

namespace PingEmDown.Components.Ball
{
    public interface IBall
    {
        int Height { get; }
        int Width { get; }
        Vector2 Position { get; set; }
        Vector2 Velocity { get; set; }
        Rectangle Boundings { get; }
        Color Color { get; }
        float Rotation { get; }

        void Update(GameTime gameTime);

        void Load();
        void Unload();
    }
}