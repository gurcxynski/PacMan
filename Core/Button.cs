using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.EasyInput;

namespace PacMan.Core
{
    public abstract class Button
    {
        protected Vector2 position;
        protected Texture2D texture;

        protected bool active = false;
        protected Button(int level)
        {
            texture = Game1.self.textures["button"];
            position = new Vector2((Configuration.windowSize.X - texture.Width) / 2, (level * 100) + Configuration.windowSize.Y / 2 - 150);
        }
        protected Button(Vector2 pos)
        {
            texture = Game1.self.textures["button"];
            position = pos;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.self.textures["wall"], new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height), EnteredButton() ? new Color(0x11CC0000) : Color.Blue);
            spriteBatch.Draw(texture, position, Color.White);
        }
        protected bool EnteredButton()
        {
            if (Game1.mouse.Position.X < position.X + texture.Width &&
                    Game1.mouse.Position.X > position.X &&
                    Game1.mouse.Position.Y < position.Y + texture.Height &&
                    Game1.mouse.Position.Y > position.Y) return true;
            return false;
        }
        protected void OnClick(MouseButtons button)
        {
            if (EnteredButton() && active && button == MouseButtons.Left)
            {
                Action();
            }
        }
        public virtual void Activate()
        {
            active = true;
            Game1.mouse.OnMouseButtonPressed += OnClick;
        }
        public void Deactivate()
        {
            active = false;
            Game1.mouse.OnMouseButtonPressed -= OnClick;
        }
        protected abstract void Action();
    }
}
