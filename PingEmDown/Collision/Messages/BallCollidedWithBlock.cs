using PingEmDown.Components.Ball;
using PingEmDown.Components.Block;

namespace PingEmDown.Collision.Messages
{
    public class BallCollidedWithBlock
    {
        public IBall Ball { get; set; }
        public IBlock Block { get; set; }
    }
}