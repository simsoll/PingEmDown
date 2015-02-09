using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PingEmDown.Screen
{
    public interface IScreenFactory
    {
        IScreen CreateStartScreen();
        IScreen CreateGameScreen();
    }
}
