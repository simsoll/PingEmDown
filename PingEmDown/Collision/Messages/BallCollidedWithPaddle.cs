using PingEmDown.Components.Ball;
using PingEmDown.Components.Paddle;

namespace PingEmDown.Collision
{
    public class BallCollidedWithPaddle
    {
        public IBall Ball { get; set; }
        public IPaddle Paddle { get; set; }
    }
}