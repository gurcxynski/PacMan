using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PacMan.Core
{
    public class GameObject
    {
        public Vector2 Velocity;
        public Vector2 Position;
        public Vector2 GridPosition;
        public Texture2D Texture;
        public virtual void Draw(SpriteBatch spriteBatch) => spriteBatch.Draw(Texture, Position, Color.White);

        public virtual void Update(GameTime UpdateTime)
        {
            float passed = (float)UpdateTime.ElapsedGameTime.TotalSeconds;
            var newPos = Position + Velocity * passed;
            if (Game1.self.activeScene.grid.CanMoveInto(newPos / Configuration.cellSize))
            {
                Position = newPos;
            }
            else Velocity = Vector2.Zero;
            // TODO FIX MOVING INTO A LOWER WALL
        }
    }
}
