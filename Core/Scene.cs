using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PacMan.Buttons;
using PacMan.GameObjects;
using System.Collections.Generic;
using System.Timers;

namespace PacMan.Core
{
    public class Scene
    {
        public List<GameObject> objects = new();
        public List<GameObject> toAdd;
        public Player player;
        public Enemy enemy;

        public PauseButton SmallPauseButton;

        Timer Timer;

        // used when drawing communicates

        public bool drawScreen = false;
        Texture2D ScreenToDraw;

        public Scene()
        {
            Game1.keyboard.OnKeyPressed += KeyPressed;
            Game1.keyboard.OnKeyReleased += KeyReleased;
        }
        public void Initialize()
        {

            player = new();
            enemy = new();
            objects.Add(player);
            objects.Add(enemy);

            SmallPauseButton = new(new(0,0));
            SmallPauseButton.Activate();

            //ShowScreen(1000, Game1.self.textures["player"]);
        }
        public void Update(GameTime UpdateTime)
        {
            if (drawScreen) return;

            // updating every object and creating new lasers

            toAdd = new();
            objects.ForEach(delegate (GameObject item) { item.Update(UpdateTime); });
            objects.AddRange(toAdd);

            //TODO  fix occasional falling out

            // Removing lasers that hit something or have fallen out and dead enemies

            objects.RemoveAll(item => false);

            Game1.self.state.UpdateStatus();
        }

        void KeyPressed(Keys button)
        {
            if (Game1.self.state.state != State.GameState.Running) return;


            switch (button)
            {
                case Keys.Escape:
                    Game1.self.state.Pause();
                    break;
                case Keys.Left:
                    player.acceleration = new Vector2(-Configuration.basePlayerVel, player.acceleration.Y);
                    break;
                case Keys.Up:
                    player.acceleration = new Vector2(player.acceleration.X, -Configuration.basePlayerVel);
                    break;
                case Keys.Down:
                    player.acceleration = new Vector2(player.acceleration.X, Configuration.basePlayerVel);
                    break;
                case Keys.Right:
                    player.acceleration = new Vector2(Configuration.basePlayerVel, player.acceleration.Y);
                    break;
                default:
                    break;
            }
        }
        void KeyReleased(Keys button)
        {
            if (Game1.self.state.state != State.GameState.Running) return;


            switch (button)
            {
                case Keys.Left:
                    player.acceleration = new Vector2(0, player.acceleration.Y);
                    break;
                case Keys.Up:
                    player.acceleration = new Vector2(player.acceleration.X, 0);
                    break;
                case Keys.Down:
                    player.acceleration = new Vector2(player.acceleration.X, 0);
                    break;
                case Keys.Right:
                    player.acceleration = new Vector2(0, player.acceleration.Y);
                    break;
                default:
                    break;
            }
        }

        // drawing everything

        public void Draw(SpriteBatch spriteBatch)
        {
            objects.ForEach(delegate (GameObject item) { item.Draw(spriteBatch); });

            SmallPauseButton.Draw(spriteBatch);

            if (drawScreen) spriteBatch.Draw(ScreenToDraw, new Vector2((Configuration.windowSize.X - ScreenToDraw.Width) / 2, (Configuration.windowSize.Y - ScreenToDraw.Height) / 2), Color.White);
        }

        // show texture for given amount of milliseconds

        public void ShowScreen(int time, Texture2D texture)
        {
            drawScreen = true;
            ScreenToDraw = texture;
            Timer = new(time) { Enabled = true };
            Timer.Elapsed += HideScreen;
        }
        void HideScreen(object source, ElapsedEventArgs e)
        {
            Timer.Elapsed -= HideScreen;
            drawScreen = false;
        }
    }
}
