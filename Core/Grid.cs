using Microsoft.Xna.Framework;
using System.Diagnostics;
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
            Circle,
            Empty
        }
        FieldType[,] grid = new FieldType[(int)Configuration.cells.X, (int)Configuration.cells.Y];
        public Grid()
        {
            for (int i = 0; i < Configuration.cells.X; i++)
            {
                for (int j = 0; j < Configuration.cells.Y; j++)
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
            for (int i = 0; i < Configuration.cells.X; i++)
            {
                for (int j = 0; j < Configuration.cells.Y; j++)
                {
                    text += $" {grid[j,i]}";
                }
                text += "\n";
            }
            return text;
        }
        public void Fill()
        {
            for (int i = 1; i < Configuration.cells.X - 1; i++)
            {
                for (int j = 1; j < Configuration.cells.Y - 1; j++)
                {
                    if (grid[i, j] == FieldType.Empty && !(i > 7 && j > 7 && i < 21 && j < 19))
                    {
                        grid[i, j] = FieldType.Point;
                        Game1.self.activeScene.objects.Add(new Point(i, j));
                    }
                }
            }
        }
        public bool CanMoveInto(Vector2 arg)
        {
            if (arg == new Vector2(13, 11) || arg == new Vector2(14, 11)) return false;
            arg = new(arg.X % Configuration.cells.X, arg.Y % Configuration.cells.Y);
            if (arg.X < 0 || arg.Y < 0) return false;
            return grid[(int)arg.X, (int)arg.Y] != FieldType.Wall;
        }
    }
}
