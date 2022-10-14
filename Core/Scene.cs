using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PacMan.Buttons;
using PacMan.Enemies;
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

        public Blinky blinky;
        public Pinky pinky;
        public Inky inky;
        public Clyde clyde;

        public List<Enemy> jail;

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


            string jsonString = File.ReadAllText("rectangles.json");
            rectangles = JsonSerializer.Deserialize<Rectangles>(jsonString);

            objects.AddRange(rectangles.Convert());

            grid.Fill();

            blinky = new();
            pinky = new();
            inky = new();
            clyde = new();

            jail = new()
            {
                pinky,
                inky,
                clyde
            };

            objects.Add(player);

            objects.Add(blinky);
            objects.Add(pinky);
            objects.Add(inky);
            objects.Add(clyde);

            objects.Add(new Circle(5, 4));
            objects.Add(new Circle(22, 4));
            objects.Add(new Circle(5, 26));
            objects.Add(new Circle(22, 26));

            SmallPauseButton = new(new(Configuration.windowSize.X - 30, 0));
            SmallPauseButton.Activate();

            TextPopUp(1000, "START");
        }
        public void Update(GameTime UpdateTime)
        {

            List<GameObject> toAdd = new();
            List<GameObject> toRemove = new();
            if (drawScreen) return;

            if (player is null)
            {
                player = new();
                objects.Add(player);
            }

            objects.ForEach(delegate (GameObject item) { item.Update(UpdateTime); });
            objects.AddRange(toAdd);

            foreach (GameObject item in objects)
            {
                if (item.GetType() == typeof(Point) && player is not null && item.GridPosition == player.GridPosition)
                {
                    score += 20;
                    toRemove.Add(item);
                }
                if (item.GetType() == typeof(Circle) && player is not null && item.GridPosition == player.GridPosition)
                {
                    Game1.self.state.EatenCircle(UpdateTime);
                    toRemove.Add(item);
                }
                if (item.GetType().IsSubclassOf(typeof(Enemy)) && player is not null && item.GridPosition == player.GridPosition)
                {
                    if (((Enemy)item).phase == Phase.Frightened)
                    {
                        Game1.self.state.EatenScared((Enemy)item, UpdateTime);
                        score += 200;
                    }
                    else
                    {
                        Game1.self.state.RemoveLive();
                        toRemove.Add(player);
                        player = null;
                    }
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
            if (player is null) return;
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

        public void Draw(SpriteBatch spriteBatch)
        {
            objects.ForEach(delegate (GameObject item) { item.Draw(spriteBatch); });

            SmallPauseButton.Draw(spriteBatch);

            spriteBatch.DrawString(Game1.self.font, $"SCORE: {score}\nHIGH SCORE: {Game1.self.high}", new(0, 0), Color.White);

            for (int i = 0; i < Game1.self.state.lives; i++)
            {
                spriteBatch.Draw(Game1.self.textures["life"], new Vector2(Configuration.windowSize.X - 100 + i * 20, 10), Color.White);
            }

            if (drawScreen) spriteBatch.DrawString(Game1.self.font, TextToDraw, (Configuration.windowSize - Game1.self.font.MeasureString(TextToDraw)) / 2 + new Vector2(0, 70), Color.White);
        }


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
