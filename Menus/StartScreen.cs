using PacMan.Buttons;
using PacMan.Core;

namespace PacMan.Menus
{
    public class StartScreen : Menu
    {
        public new void Initialize()
        {
            buttons.Add(new PlayButton(1));

            base.Initialize();
        }
    }
}
