using Microsoft.Xna.Framework;
using PacMan.Core;
using PacMan.GameObjects;

namespace PacMan.Enemies
{
    public class Pinky : Enemy
    {
        public Pinky()
        {
            Texture = Game1.self.textures["pr"];
            orgTexture = Texture;
            GridPosition = new(13, 13);
            Position = (GridPosition + new Vector2(-1.5f, 0)) * Configuration.cellSize;
            target = new(-1, 100);
        }
    }
}
