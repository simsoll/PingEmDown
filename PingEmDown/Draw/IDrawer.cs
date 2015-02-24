using Microsoft.Xna.Framework;
using PingEmDown.Rectangle;

namespace PingEmDown.Draw
{
    public interface IDrawer
    {
        void Draw(Vector2 position, Color color);
        void Draw(IRectangle destinationRectangle, Color color, float rotation);
    }
}