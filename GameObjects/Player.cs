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
            // create boundary bouncing effect

            if (Position.X < 0 || Position.X + Texture.Width > Configuration.windowSize.X) Velocity *= new Vector2(-1, 1);
            if (Position.Y < 0 || Position.Y + Texture.Height > Configuration.windowSize.Y) Velocity *= new Vector2(1, -1);

            // adjust movement by dampening to create more realistic movement

            base.Update(UpdateTime);
            Velocity += acceleration;
            Velocity *= Configuration.dampening;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
