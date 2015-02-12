using Microsoft.Xna.Framework;

namespace PingEmDown.Component
{
    public interface IComponent
    {
        int Height { get; set; }
        int Width { get; set; }
        Vector2 Position { get; set; }
        Rectangle Boundings { get; }
        Color Color { get; set; }
        float Rotation { get; set; }

        void Update(GameTime gameTime);
    }
}