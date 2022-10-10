using PacMan.Buttons;
using PacMan.Core;

namespace PacMan.Menus
{
    public class StartScreen : Menu
    {
        public new void Initialize()
        {
            buttons.Add(new NewGameButton(1));
            buttons.Add(new ExitGameButton(2));

            base.Initialize();
        }
    }
}
