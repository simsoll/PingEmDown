#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using PingEmDown.Configuration;
using PingEmDown.Draw;
using PingEmDown.Input.Keyboard;
using PingEmDown.Level;
using PingEmDown.Messaging.Caliburn.Micro;
using PingEmDown.Screen;
using PingEmDown.Text;

#endregion

namespace PingEmDown
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class PingEmDown : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private IEventAggregator _eventAggregator;
        private KeyboardManager _keyboardManager;
        private ScreenManager _screenManager;


        public PingEmDown()
            : base()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            var viewPort = _graphics.GraphicsDevice.Viewport;

            var screenConfiguration = new GameScreenConfiguration(viewPort.Height, viewPort.Width);
            var levelConfiguration = new LevelConfiguration(12, Color.Black, 12, 52, Color.Black, 12, 64, Color.Black, 8, Color.Black, 42, 12, 8);
            var textConfiguration = new TextConfiguration(Color.Black, 2);

            var textTexture = GetPlain2DTexture(textConfiguration.TextSize);
            var texture = GetPlain2DTexture(1);

            _eventAggregator = new EventAggregator();
            _keyboardManager = new KeyboardManager(_eventAggregator, TimeSpan.FromMilliseconds(500));
            var drawer = new Drawer(_spriteBatch, texture);
            var textDrawer = new Drawer(_spriteBatch, textTexture);
            var pixelTextDrawer = new PixelTextDrawer(textDrawer);

            var startScreen = new StartScreen(_eventAggregator, pixelTextDrawer, screenConfiguration, textConfiguration);
            var levelFactory = new LevelFactory(screenConfiguration, levelConfiguration);
            var levelManager = new LevelManager(_eventAggregator, levelFactory, drawer);
            var gameScreen = new GameScreen(_eventAggregator, levelManager);
            _screenManager = new ScreenManager(_eventAggregator, startScreen, gameScreen);

            _keyboardManager.Load();
            _screenManager.Load();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _keyboardManager.Update(gameTime);
            _screenManager.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightGray);

            _spriteBatch.Begin();

            _screenManager.Draw();

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private Texture2D GetPlain2DTexture(int textureSize)
        {
            var texture = new Texture2D(GraphicsDevice, textureSize, textureSize);
            var color = new Color[textureSize * textureSize];
            for (var i = 0; i < color.Length; i++)
            {
                color[i] = Color.White;
            }
            texture.SetData(color);

            return texture;
        }
    }
}
