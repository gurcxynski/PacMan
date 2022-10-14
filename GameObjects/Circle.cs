using PacMan.Core;

namespace PacMan.GameObjects
{
    internal class Circle : GameObject
    {
        public Circle(int x, int y)
        {
            Texture = Game1.self.textures["circle"];
            GridPosition = new(x, y);
            Position = Configuration.cellSize * GridPosition;
        }
    }
}
