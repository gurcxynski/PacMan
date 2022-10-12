using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PacMan.Core;

namespace PacMan.GameObjects
{
    internal class WallRect : GameObject
    {
        public Rectangle bounds;
        public bool vertical;
        public MyRectangle original;
        public WallRect(Rectangle arg, MyRectangle source)
        {
            bounds = arg;
            vertical = bounds.Width == 1;
            original = source;
            Texture = Game1.self.textures["wall"];
        }
        override public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, bounds, vertical ? Color.White : Color.Gray);
        }
    }
}
