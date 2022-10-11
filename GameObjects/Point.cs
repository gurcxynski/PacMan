using PacMan.Core;

namespace PacMan.GameObjects
{
    internal class Point : GameObject
    {
        public Point(int x, int y)
        {
            Texture = Game1.self.textures["point"];
            GridPosition = new(x, y);
            Position = Configuration.cellSize * GridPosition;
        }
    }
}
