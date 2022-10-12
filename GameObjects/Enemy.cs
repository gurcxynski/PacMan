using Microsoft.Xna.Framework;
using PacMan.Core;

namespace PacMan.GameObjects
{
    public class Enemy : GameObject
    {
        Direction? dir;
        public Enemy(Vector2 arg)
        {
            Texture = Game1.self.textures["enemy"];
            GridPosition = arg;
            Position = Configuration.cellSize * GridPosition;
        }
        public override void Update(GameTime UpdateTime)
        {
            
        }
    }
}