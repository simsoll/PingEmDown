using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PingEmDown.Randomizer
{
    public interface IRandomizer
    {
        int Next();
        int Next(int maxValue);
        int Next(int minValue, int maxValue);
        double NextDouble();
    }
}