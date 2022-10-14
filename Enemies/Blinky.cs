using Microsoft.Xna.Framework;
using PacMan.Core;
using PacMan.GameObjects;

namespace PacMan.Enemies
{
    public class Blinky : Enemy
    {
        public Blinky()
        {
            Texture = Game1.self.textures["rr"];
            orgTexture = Texture;
            GridPosition = new(13, 10);
            Position = (GridPosition + new Vector2(+0.5f, 0)) * Configuration.cellSize;
            state = Leaving.left;
            target = new(100, 100);
            QueueTurn(GridPosition);
        }
    }
}
