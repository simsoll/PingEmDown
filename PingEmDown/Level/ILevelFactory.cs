using System;
using System.Linq;
using System.Text;

namespace PingEmDown.Level
{
    public interface ILevelFactory
    {
        ILevel CreateLevel();
    }
}
