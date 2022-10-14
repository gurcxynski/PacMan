using PacMan.Core;

namespace PacMan.Buttons
{
    internal class QuitToStartButton : Button
    {
        public QuitToStartButton(int arg) : base(arg)
        {
            texture = Game1.self.textures["menubutton"];
        }

        protected override void Action()
        {
            Game1.self.state.ToStartMenu();
        }
    }
}
