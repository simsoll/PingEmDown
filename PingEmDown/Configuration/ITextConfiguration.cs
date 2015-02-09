using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PingEmDown.Configuration
{
    public interface ITextConfiguration
    {
        Color TextColor { get; }
        int TextSize { get; }
    }
}
