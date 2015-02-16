using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PingEmDown.Component;

namespace PingEmDown.Paddle.Messages
{
    public class PaddleMoved
    {
        public IComponent Paddle { get; set; }
    }
}
