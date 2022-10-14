using Microsoft.Xna.Framework;
using PacMan.Core;
using PacMan.GameObjects;

namespace PacMan.Enemies
{
    public class Clyde : Enemy
    {
        public Clyde()
        {
            Texture = Game1.self.textures["yr"];
            orgTexture = Texture;
            GridPosition = new(13, 13);
            Position = (GridPosition + new Vector2(+0.5f, 0)) * Configuration.cellSize;
            target = new(-1, -1);
        }
    }
}
