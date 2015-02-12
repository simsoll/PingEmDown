﻿using Microsoft.Xna.Framework;

namespace PingEmDown.Level
{
    public interface ILevelManager
    {
        void Load();
        void Unload();
        void Update(GameTime gameTime);
        void Draw();
    }
}