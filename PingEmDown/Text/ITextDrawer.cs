using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PingEmDown.Text
{
    public interface ITextDrawer
    {
        void DrawText(string text, Vector2 position, int size, Color color);

        void DrawTextCentered(string text, Vector2 position, int size, Color color);
    }
}