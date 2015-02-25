using PingEmDown.Components.Ball;
using PingEmDown.Components.Wall;

namespace PingEmDown.Collision.Messages
{
    public class BallCollidedWithWall
    {
        public IBall Ball { get; set; }
        public IWall Wall { get; set; }
    }
}