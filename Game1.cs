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
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static Game1 self;

        // Mouse and keyboard from EasyInput

        public static EasyKeyboard keyboard;
        public static EasyMouse mouse;

        public State state;


        public Scene activeScene;

        public StartScreen starting;
        public PauseMenu menu;


        public Dictionary<string, Texture2D> textures = new();
        public SpriteFont font;

        public int high = 0;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            self = this;

            Window.Title = "Pac-Man";
            _graphics.PreferredBackBufferWidth = (int)Configuration.windowSize.X;
            _graphics.PreferredBackBufferHeight = (int)Configuration.windowSize.Y + 50;

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

            textures["back"] = Content.Load<Texture2D>("back");
            textures["button"] = Content.Load<Texture2D>("newgame");
            textures["player"] = Content.Load<Texture2D>("player");
            textures["pausebutton"] = Content.Load<Texture2D>("placeholder");
            textures["menubck"] = Content.Load<Texture2D>("menu");
            textures["playbutton"] = Content.Load<Texture2D>("newgame");
            textures["menubutton"] = Content.Load<Texture2D>("placeholder");
            textures["resumebutton"] = Content.Load<Texture2D>("resume");
            textures["exitbutton"] = Content.Load<Texture2D>("exit");
            textures["wall"] = Content.Load<Texture2D>("wall");
            textures["point"] = Content.Load<Texture2D>("point");
            textures["openLeft"] = Content.Load<Texture2D>("openleft");
            textures["openRight"] = Content.Load<Texture2D>("openright");
            textures["openDown"] = Content.Load<Texture2D>("opendown");
            textures["openUp"] = Content.Load<Texture2D>("openup");
            textures["circle"] = Content.Load<Texture2D>("circle");
            textures["scared"] = Content.Load<Texture2D>("scared");
            textures["life"] = Content.Load<Texture2D>("live");


            textures["ru"] = Content.Load<Texture2D>("ru");
            textures["rd"] = Content.Load<Texture2D>("rd");
            textures["rl"] = Content.Load<Texture2D>("rl");
            textures["rr"] = Content.Load<Texture2D>("rr");

            textures["pu"] = Content.Load<Texture2D>("pu");
            textures["pd"] = Content.Load<Texture2D>("pd");
            textures["pl"] = Content.Load<Texture2D>("pl");
            textures["pr"] = Content.Load<Texture2D>("pr");

            textures["yu"] = Content.Load<Texture2D>("yu");
            textures["yd"] = Content.Load<Texture2D>("yd");
            textures["yl"] = Content.Load<Texture2D>("yl");
            textures["yr"] = Content.Load<Texture2D>("yr");

            textures["bu"] = Content.Load<Texture2D>("bu");
            textures["bd"] = Content.Load<Texture2D>("bd");
            textures["bl"] = Content.Load<Texture2D>("bl");
            textures["br"] = Content.Load<Texture2D>("br");

            font = Content.Load<SpriteFont>("font");

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

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            //_spriteBatch.Draw(textures["back"], new Vector2(0,0), Color.White);

            if (state.state == State.GameState.Paused) menu.Draw(_spriteBatch);
            else if (state.state == State.GameState.StartMenu) starting.Draw(_spriteBatch);
            else activeScene.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}