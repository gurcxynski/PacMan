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

        public Player player;
        public Enemy enemy;
        public Grid grid;
        public int score = 0;

        public PauseButton SmallPauseButton;

        Rectangles rectangles;

        Timer Timer;

        // used when drawing communicates

        public bool drawScreen = false;
        string TextToDraw;

        public Scene()
        {
            Game1.keyboard.OnKeyPressed += KeyPressed;
        }
        public void Initialize()
        {
            grid = new();

            player = new();
            enemy = new(new(12, 12));

            
            var jsonString = File.ReadAllText("rectangles.json");
            rectangles = JsonSerializer.Deserialize<Rectangles>(jsonString);

            objects.AddRange(rectangles.Convert());

            grid.Fill();
            objects.Add(player);
            objects.Add(enemy);

            SmallPauseButton = new(new(0,0));
            SmallPauseButton.Activate();

            TextPopUp(1000, "START");
        }
        public void Update(GameTime UpdateTime)
        {

            List<GameObject> toAdd = new();
            List<GameObject> toRemove = new();

            if (drawScreen) return; 

            objects.ForEach(delegate (GameObject item) { item.Update(UpdateTime); });
            objects.AddRange(toAdd);

            foreach (var item in objects)
            {
                if (item.GetType() == typeof(Point) && item.GridPosition == player.GridPosition)
                {
                    score += 20;
                    toRemove.Add(item);
                }
            }

            objects.RemoveAll(item => toRemove.Contains(item));

            if (objects.FindAll(item => item.GetType() == typeof(Point)).Count == 0)
            {
                Game1.self.state.GameWon();
            }

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

            spriteBatch.DrawString(Game1.self.font, $"SCORE: {score}\nHIGH SCORE: {Game1.self.high}", new(0, Configuration.windowSize.Y), Color.White);

            if (drawScreen) spriteBatch.DrawString(Game1.self.font, TextToDraw, (Configuration.windowSize - Game1.self.font.MeasureString(TextToDraw)) / 2, Color.White); 
        }

        // show texture for given amount of milliseconds

        public void TextPopUp(int time, string text)
        {
            drawScreen = true;
            TextToDraw = text;
            Timer = new(time) { Enabled = true };
            Timer.Elapsed += HideText;
        }
        void HideText(object source, ElapsedEventArgs e)
        {
            Timer.Dispose();
            drawScreen = false;
        }
    }
}
