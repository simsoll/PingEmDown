using Microsoft.Xna.Framework;
using OpenTK.Graphics.OpenGL;
using PingEmDown.Component;

namespace PingEmDown.Draw
{
    public interface IDrawer
    {
        void Draw(Vector2 position, Color color);
        void Draw(Rectangle destinationRectangle, Color color, float rotation);
        void Draw(IComponent component);
    }
}