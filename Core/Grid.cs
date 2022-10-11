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
        FieldType[,] grid = new FieldType[26, 26];
        public Grid()
        {
            for (int i = 0; i < 26; i++)
            {
                for (int j = 0; j < 26; j++)
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
            for (int i = 0; i < 26; i++)
            {
                for (int j = 0; j < 26; j++)
                {
                    text += $" {grid[j,i]}";
                }
                text += "\n";
            }
            return text;
        }
        public void Fill()
        {
            for (int i = 0; i < 26; i++)
            {
                for (int j = 0; j < 26; j++)
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
            return grid[(int)arg.X, (int)arg.Y] != FieldType.Wall;
        }
    }
}
