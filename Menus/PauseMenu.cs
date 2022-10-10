using PacMan.Buttons;
using PacMan.Core;

namespace PacMan.Menus
{
    public class PauseMenu : Menu
    {
        public new void Initialize()
        {
            buttons.Add(new ResumeButton(0));
            buttons.Add(new NewGameButton(1));
            buttons.Add(new ExitGameButton(2));

            base.Initialize();
        }
    }
}
