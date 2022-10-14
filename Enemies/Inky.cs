using Microsoft.Xna.Framework;
using PacMan.Core;
using PacMan.GameObjects;

namespace PacMan.Enemies
{
    public class Inky : Enemy
    {
        public Inky()
        {
            Texture = Game1.self.textures["br"];
            orgTexture = Texture;
            GridPosition = new(13, 13);
            Position = (GridPosition + new Vector2(+2.5f, 0)) * Configuration.cellSize;
            target = new(100, -1);
        }
    }
}
