using Microsoft.Xna.Framework;

namespace PingEmDown.Draw
{
    public interface IDrawer
    {
        void Draw(Vector2 position, Color color);
        void Draw(Rectangle destinationRectangle, Color color, float rotation);
    }
}