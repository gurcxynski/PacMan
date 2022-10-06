using PacMan.Core;

namespace PacMan.Buttons
{
    internal class ExitGameButton : Button
    {
        public ExitGameButton(int arg) : base(arg)
        {
            texture = Game1.self.textures["exitbutton"];
        }

        protected override void Action()
        {
            Game1.self.Exit();
        }
    }
}
