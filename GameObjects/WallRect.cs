using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PacMan.Core;

namespace PacMan.GameObjects
{
    internal class WallRect : GameObject
    {
        Rectangle bounds;
        public WallRect(Rectangle arg)
        {
            bounds = arg;
            Texture = Game1.self.textures["wall"];
        }
        override public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, bounds, Color.White);
        }
    }
}
