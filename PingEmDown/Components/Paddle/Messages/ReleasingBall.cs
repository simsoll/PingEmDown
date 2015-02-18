using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PingEmDown.Components.Paddle.Messages
{
    public class ReleasingBall
    {
        public IPaddle Paddle { get; set; }
    }
}
