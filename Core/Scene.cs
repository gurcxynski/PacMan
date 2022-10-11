using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PacMan.Buttons;
using PacMan.GameObjects;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Timers;
using Point = PacMan.GameObjects.Point;

namespace PacMan.Core
{
    public class Scene
    {
        public List<GameObject> objects = new();
        public List<GameObject> toAdd;
        public Player player;
        public Enemy enemy;
        public Grid grid;

        public PauseButton SmallPauseButton;

        Rectangles rectangles;

        Timer Timer;

        // used when drawing communicates

        public bool drawScreen = false;
        Texture2D ScreenToDraw;

        public Scene()
        {
            Game1.keyboard.OnKeyPressed += KeyPressed;
        }
        public void Initialize()
        {
            grid = new();

            player = new();
            //enemy = new();
            objects.Add(player);
            
            //objects.Add(enemy);
            
            var jsonString = File.ReadAllText("rectangles.json");
            rectangles = JsonSerializer.Deserialize<Rectangles>(jsonString);

            objects.AddRange(rectangles.Convert());

            grid.Fill();


            SmallPauseButton = new(new(0,0));
            SmallPauseButton.Activate();

            //Debug.WriteLine(grid.ToString());

            //ShowScreen(1000, Game1.self.textures["player"]);
        }
        public void Update(GameTime UpdateTime)
        {
            if (drawScreen) return;


            toAdd = new();
            objects.ForEach(delegate (GameObject item) { item.Update(UpdateTime); });
            objects.AddRange(toAdd);

            objects.RemoveAll(item => item.GetType() == typeof(Point) && item.GridPosition == player.GridPosition);


            Game1.self.state.UpdateStatus();
        }

        void KeyPressed(Keys button)
        {
            if (Game1.self.state.state != State.GameState.Running) return;


            switch (button)
            {
                case Keys.Left:
                    player.Queue(Direction.Left);
                    break;
                case Keys.Up:
                    player.Queue(Direction.Up);
                    break;
                case Keys.Down:
                    player.Queue(Direction.Down);
                    break;
                case Keys.Right:
                    player.Queue(Direction.Right);
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
