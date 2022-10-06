using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.EasyInput;
using PacMan.Core;
using PacMan.Menus;
using System.Collections.Generic;

namespace PacMan
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static Game1 self;

        // Mouse and keyboard from EasyInput

        public static EasyKeyboard keyboard;
        public static EasyMouse mouse;

        // Game State

        public State state;

        // Game Scene

        public Scene activeScene;

        // All menus

        public StartScreen starting;
        public PauseMenu menu;

        // Texture list

        public Dictionary<string, Texture2D> textures = new();


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            self = this;

            Window.Title = "Pac-Man";
            _graphics.PreferredBackBufferWidth = (int)Configuration.windowSize.X;
            _graphics.PreferredBackBufferHeight = (int)Configuration.windowSize.Y;

            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            keyboard = new();
            mouse = new();

            activeScene = new();

            menu = new();
            starting = new();

            state = new() { state = State.GameState.StartMenu };

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Loading all needed files

            textures["back"] = Content.Load<Texture2D>("menu");
            textures["button"] = Content.Load<Texture2D>("placeholder");
            textures["player"] = Content.Load<Texture2D>("placeholder");
            textures["pausebutton"] = Content.Load<Texture2D>("placeholder");
            textures["menubck"] = Content.Load<Texture2D>("menu");
            textures["playbutton"] = Content.Load<Texture2D>("placeholder");
            textures["menubutton"] = Content.Load<Texture2D>("placeholder");
            textures["resumebutton"] = Content.Load<Texture2D>("placeholder");
            textures["exitbutton"] = Content.Load<Texture2D>("placeholder");

            // Initializing menus and game scene, loading level 1

            menu.Initialize();
            starting.Initialize();

            starting.Activate();

            activeScene.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            mouse.Update();
            keyboard.Update();

            if (state.state == State.GameState.Running || state.state == State.GameState.GameLost || state.state == State.GameState.GameWon)
                activeScene.Update(gameTime);

            System.Diagnostics.Debug.WriteLine(state.state);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            if (state.state == State.GameState.Paused) menu.Draw(_spriteBatch);
            else if (state.state == State.GameState.StartMenu) starting.Draw(_spriteBatch);
            else activeScene.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}