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
        public void UpdateStatus()
        {
            if (state == GameState.GameWon && !Game1.self.activeScene.drawScreen)
            {
                ToStartMenu();
            }
        }
        public bool GameOver()
        {
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
            if (Game1.self.activeScene.score > Game1.self.high) Game1.self.high = Game1.self.activeScene.score;
            state = GameState.StartMenu;
            Game1.self.activeScene.SmallPauseButton.Deactivate();
            Game1.self.starting.Activate();
            return false;
        }
        public bool Play()
        {
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

    }
}
