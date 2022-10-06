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
            
        }
        public bool GameOver()
        {
            return false;
        }
        public bool GameWon()
        {
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
            return false;
        }
        public bool Play()
        {
            if (state == GameState.StartMenu) Game1.self.starting.Deactivate();
            if (state == GameState.Paused) Game1.self.menu.Deactivate();
            Game1.self.activeScene.SmallPauseButton.Activate();
            state = GameState.Running;
            return false;
        }

    }
}
