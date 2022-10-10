using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PacMan.Core;

namespace PacMan.GameObjects
{
    public class Player : GameObject
    {
        public Vector2 acceleration;

        public Player()
        {
            Texture = Game1.self.textures["player"];

            Position = new((Configuration.windowSize.X - Texture.Width) / 2, Configuration.windowSize.Y - 100);
            Velocity = new();
            acceleration = new();
        }

        public override void Update(GameTime UpdateTime)
        {
            base.Update(UpdateTime);

            if (Position.X < 0) Position = new(0, Position.Y);
            if (Position.Y < 0) Position = new(Position.X, 0);
            if (Position.X + Texture.Width > Configuration.windowSize.X) Position = new(Configuration.windowSize.X - Texture.Width, Position.Y);
            if (Position.Y + Texture.Height > Configuration.windowSize.Y) Position = new(Position.X, Configuration.windowSize.Y - Texture.Height);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
