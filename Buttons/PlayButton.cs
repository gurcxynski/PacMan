using PacMan;
using PacMan.Core;

namespace PacMan.Buttons
{
    internal class PlayButton : Button
    {
        public PlayButton(int arg) : base(arg)
        {
            texture = Game1.self.textures["playbutton"];
        }

        protected override void Action()
        {
            Game1.self.state.Play();
        }
    }
}
