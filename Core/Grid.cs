using Microsoft.Xna.Framework;
using Point = PacMan.GameObjects.Point;

namespace PacMan.Core
{
    public class Grid
    {
        public enum FieldType
        {
            Player,
            Wall,
            Point,
            Enemy,
            Empty
        }
        FieldType[,] grid = new FieldType[Configuration.cells, Configuration.cells];
        public Grid()
        {
            for (int i = 0; i < Configuration.cells; i++)
            {
                for (int j = 0; j < Configuration.cells; j++)
                {
                    grid[i,j] = FieldType.Empty;
                }
            }
        }
        public bool Add(int x, int y, FieldType type)
        {
            if (grid[x,y] == FieldType.Empty) grid[x,y] = type;
            else return false;
            return true;
        }
        public void Clear(int x, int y)
        {
            grid[x,y] = FieldType.Empty;
        }
        public override string ToString()
        {
            string text = "";
            for (int i = 0; i < Configuration.cells; i++)
            {
                for (int j = 0; j < Configuration.cells; j++)
                {
                    text += $" {grid[j,i]}";
                }
                text += "\n";
            }
            return text;
        }
        public void Fill()
        {
            for (int i = 1; i < Configuration.cells - 1; i++)
            {
                for (int j = 1; j < Configuration.cells - 1; j++)
                {
                    if (grid[i, j] == FieldType.Empty)
                    {
                        grid[i, j] = FieldType.Point;
                        Game1.self.activeScene.objects.Add(new Point(i, j));
                    }
                }
            }
        }
        public bool CanMoveInto(Vector2 arg)
        {
            arg = new(arg.X % Configuration.cells, arg.Y % Configuration.cells);
            return grid[(int)arg.X, (int)arg.Y] != FieldType.Wall;
        }
    }
}
