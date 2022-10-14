using Microsoft.Xna.Framework;
using PacMan.GameObjects;

namespace PacMan.Core
{
    public class State
    {
        public enum GameState
        {
            Running,
            GameWon,
            GameLost,
            Paused,
            StartMenu
        }
        public GameState state;
        public int lives = 3;
        public void UpdateStatus()
        {
            if (state == GameState.GameWon && !Game1.self.activeScene.drawScreen)
            {
                ToStartMenu();
            }
            if (state == GameState.GameLost && !Game1.self.activeScene.drawScreen)
            {
                ToStartMenu();
            }
        }
        public bool GameOver()
        {
            if (state != GameState.Running) return false;
            Game1.self.activeScene.TextPopUp(2000, "YOU LOSE");
            state = GameState.GameLost;
            return false;
        }
        public bool GameWon()
        {
            if (state != GameState.Running) return false;
            Game1.self.activeScene.TextPopUp(2000, "YOU WIN");
            state = GameState.GameWon;
            return false;
        }
        public bool Pause()
        {
            state = GameState.Paused;
            Game1.self.activeScene.SmallPauseButton.Deactivate();
            Game1.self.menu.Activate();
            return false;
        }
        public bool ToStartMenu()
        {
            state = GameState.StartMenu;
            Game1.self.activeScene.SmallPauseButton.Deactivate();
            Game1.self.starting.Activate();
            lives = 3;
            return false;
        }
        public bool Play()
        {
            if (Game1.self.activeScene.score > Game1.self.high) Game1.self.high = Game1.self.activeScene.score;
            Game1.self.starting.Deactivate();
            Game1.self.activeScene = new();
            Game1.self.activeScene.Initialize();
            state = GameState.Running;
            return false;
        }
        public bool Resume()
        {
            Game1.self.menu.Deactivate();
            Game1.self.activeScene.SmallPauseButton.Activate();
            state = GameState.Running;
            return false;
        }

        internal void RemoveLive()
        {
            lives--;
            if (lives == 0) GameOver();
        }
        public void EatenCircle(GameTime updateTime)
        {
            Game1.self.activeScene.blinky.Frighten();
            Game1.self.activeScene.inky.Frighten();
            Game1.self.activeScene.pinky.Frighten();
            Game1.self.activeScene.clyde.Frighten();
            if (Game1.self.activeScene.jail.Count > 0) Game1.self.activeScene.jail[^1].Leave(updateTime);
        }

        internal void EatenScared(Enemy food, GameTime updateTime)
        {
            Game1.self.activeScene.jail.Add(food);
            if (Game1.self.activeScene.jail.Count > 0) Game1.self.activeScene.jail[^1].Leave(updateTime);
            food.phase = Phase.Scatter;
            food.state = Leaving.stay;
            food.GridPosition = new(13, 13);
            food.Position = (food.GridPosition + new Vector2(-1.5f + Game1.self.activeScene.jail.Count * 2f, 0)) * Configuration.cellSize;
            food.Texture = food.orgTexture;
        }
    }
}
