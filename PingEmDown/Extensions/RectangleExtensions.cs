using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace PingEmDown.Extensions
{
    public static class RectangleExtensions
    {
        public static IEnumerable<Vector2> Edges(this Rectangle rectangle)
        {
            return new List<Vector2>
                {
                    new Vector2(rectangle.Left, rectangle.Top),
                    new Vector2(rectangle.Left, rectangle.Bottom),
                    new Vector2(rectangle.Right, rectangle.Top),
                    new Vector2(rectangle.Right, rectangle.Top)
                };
        }
    }
}
