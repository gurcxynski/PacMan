using PacMan.Core;

namespace PacMan.Buttons
{
    internal class NewGameButton : Button
    {
        public NewGameButton(int arg) : base(arg)
        {
            texture = Game1.self.textures["playbutton"];
        }

        protected override void Action()
        {
            Game1.self.state.Play();
        }
    }
}
