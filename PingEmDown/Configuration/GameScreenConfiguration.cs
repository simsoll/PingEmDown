namespace PingEmDown.Configuration
{
    public class GameScreenConfiguration : IScreenConfiguration
    {
        public int ScreenHeight { get; private set; }
        public int ScreenWidth { get; private set; }

        public GameScreenConfiguration(int screenHeight, int screenWidth)
        {
            ScreenHeight = screenHeight;
            ScreenWidth = screenWidth;
        }
    }
}