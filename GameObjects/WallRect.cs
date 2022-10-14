using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PacMan.Core;

namespace PacMan.GameObjects
{
    internal class WallRect : GameObject
    {
        public Rectangle bounds;
        public WallRect(Rectangle arg)
        {
            bounds = arg;
            Texture = Game1.self.textures["wall"];
        }
        override public void Draw(SpriteBatch spriteBatch)
        {
            if(bounds.Width == Configuration.cellSize || bounds.Height == Configuration.cellSize) spriteBatch.Draw(Texture, new Rectangle(bounds.X + 7, bounds.Y + 7 + 50, bounds.Width - 14, bounds.Height - 14), Color.DarkRed);
            else spriteBatch.Draw(Texture, new Rectangle(bounds.X + 10, bounds.Y + 10 + 50, bounds.Width - 20, bounds.Height - 20), Color.DarkRed);
        }
    }
}
