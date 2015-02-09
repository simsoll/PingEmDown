using Microsoft.Xna.Framework;

namespace PingEmDown.Screen
{
    public interface IScreen
    {
        void Load();
        void Unload();
        void Update(GameTime gameTime);
        void Draw();
    }
}