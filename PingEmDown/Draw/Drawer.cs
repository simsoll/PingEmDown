using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PingEmDown.Rectangle;

namespace PingEmDown.Draw
{
    public class Drawer : IDrawer
    {
        private readonly SpriteBatch _spriteBatch;
        private readonly Texture2D _texture;
        private readonly IRectangle _sourceRetangle;

        public Drawer(SpriteBatch spriteBatch, Texture2D texture)
        {
            _spriteBatch = spriteBatch;
            _texture = texture;

            _sourceRetangle = new Rectangle.Rectangle(0, 0, _texture.Width, _texture.Height);
        }

        public void Draw(Vector2 position, Color color)
        {
            _spriteBatch.Draw(_texture, position, color);
        }

        public void Draw(IRectangle destinationRectangle, Color color, float rotation)
        {
            _spriteBatch.Draw(_texture, destinationRectangle.Position,
                new Microsoft.Xna.Framework.Rectangle((int) _sourceRetangle.X, (int) _sourceRetangle.Y,
                    (int)_sourceRetangle.Width, (int)_sourceRetangle.Height), color, rotation, Vector2.Zero, destinationRectangle.Scale,
                SpriteEffects.None, 0.0f);
        }
    }
}