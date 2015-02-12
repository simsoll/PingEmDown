using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PingEmDown.Draw
{
    public class Drawer : IDrawer
    {
        private readonly SpriteBatch _spriteBatch;
        private readonly Texture2D _texture;
        private readonly Rectangle _sourceRetangle;

        public Drawer(SpriteBatch spriteBatch, Texture2D texture)
        {
            _spriteBatch = spriteBatch;
            _texture = texture;

            _sourceRetangle = new Rectangle(0, 0, _texture.Width, _texture.Height);
        }

        public void Draw(Vector2 position, Color color)
        {
            _spriteBatch.Draw(_texture, position, color);
        }

        public void Draw(Rectangle destinationRectangle, Color color, float rotation)
        {
            _spriteBatch.Draw(_texture, destinationRectangle, _sourceRetangle, color, rotation, Vector2.Zero, SpriteEffects.None, 0.0f);
        }
    }
}