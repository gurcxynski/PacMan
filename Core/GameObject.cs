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

        // object movement is adjusted by dampering in scene.Update()

        public virtual void Update(GameTime UpdateTime)
        {
            float passed = (float)UpdateTime.ElapsedGameTime.TotalSeconds;
            Position += Velocity * passed;
        }
    }
}
