﻿using Microsoft.Xna.Framework;

namespace PingEmDown.Configuration
{
    public class LevelConfiguration : ILevelConfiguration
    {
        public int WallWidth { get; private set; }
        public Color WallColor { get; private set; }
        public int BlockHeight { get; private set; }
        public int BlockWidth { get; private set; }
        public Color BlockColor { get; private set; }
        public int BlockWallOffset { get; private set; }
        public int BlockBlockOffset { get; private set; }
        public int BlockRows { get; private set; }

        public LevelConfiguration(int wallWidth, Color wallColor, int blockHeight, int blockWidth, Color blockColor,
            int blockWallOffset, int blockBlockOffset, int blockRows)
        {
            BlockRows = blockRows;
            BlockBlockOffset = blockBlockOffset;
            BlockWallOffset = blockWallOffset;
            BlockColor = blockColor;
            BlockWidth = blockWidth;
            BlockHeight = blockHeight;
            WallColor = wallColor;
            WallWidth = wallWidth;
        }
    }
}