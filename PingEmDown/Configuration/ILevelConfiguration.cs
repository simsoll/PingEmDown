using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PingEmDown.Configuration
{
    public interface ILevelConfiguration
    {
        int WallWidth { get; }
        Color WallColor { get; }
        int BlockHeight { get; }
        int BlockWidth { get; }
        Color BlockColor { get; }
        int BlockWallOffset { get; }
        int BlockBlockOffset { get; }
        int BlockRows { get; }
    }
}
