using Microsoft.Xna.Framework;

namespace PingEmDown.Components.Paddle
{
    public interface IPaddle
    {
        Vector2 Position { get; set; }
        Vector2 Velocity { get; set; }
        Rectangle Boundings { get; }
        Color Color { get; }
        float Rotation { get; }

        void Load();
        void Unload();
        void Update(GameTime gameTime);
    }
}