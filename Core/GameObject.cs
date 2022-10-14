using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PacMan.GameObjects;

namespace PacMan.Core
{
    abstract public class GameObject
    {
        public Vector2 Position;
        public Vector2 GridPosition;
        public Texture2D Texture;
        public virtual void Draw(SpriteBatch spriteBatch) => spriteBatch.Draw(Texture, Position + new Vector2(0, 50), Color.White);

        public virtual void Update(GameTime UpdateTime)
        {

        }


    }
}
