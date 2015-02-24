using Microsoft.Xna.Framework;
using PingEmDown.Rectangle;

namespace PingEmDown.Components.Paddle
{
    public interface IPaddle
    {
        Vector2 Position { get; set; }
        Vector2 Velocity { get; set; }
        IRectangle Boundings { get; }
        Color Color { get; }
        float Rotation { get; }

        void Load();
        void Unload();
        void Update(GameTime gameTime);
    }
}