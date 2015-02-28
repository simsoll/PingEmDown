using Microsoft.Xna.Framework;

namespace PingEmDown.Particle
{
    public interface IParticleManager
    {
        void Load();
        void Unload();
        void Update(GameTime gameTime);
        void Draw();
    }
}