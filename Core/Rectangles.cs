using PacMan.GameObjects;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PacMan.Core
{
    internal class Rectangles
    {
        [JsonInclude] public List<MyRectangle> rectangles;
        public Rectangles()
        {
            rectangles = new();
        }
        public List<WallRect> Convert()
        {
            List<WallRect> converted = new();
            var grid = Game1.self.activeScene.grid;

            rectangles.ForEach(delegate (MyRectangle item) { 

                converted.Add(new(item.ToRect(), item));

                for (int i = 0; i < item.Width; i++)
                {
                     for (int j = 0; j < item.Height; j++)
                     {
                        grid.Add(i + item.X, j + item.Y, Grid.FieldType.Wall);
                     }
                }
            });
            foreach (var item in converted)
            {
                if (item.vertical)
                {
                    if (grid.IsWall(new(item.original.X, item.original.Y - 1)))
                    {
                        item.bounds.Y -= 5;
                        item.bounds.Height += 5;
                    }
                    if (grid.IsWall(new(item.original.X, item.original.Y + 1)))
                    {
                        item.bounds.Height += 5;
                    }
                }
                else
                {
                    if (grid.IsWall(new(item.original.X - 1, item.original.Y)))
                    {
                        item.bounds.X -= 5;
                        item.bounds.Width += 5;
                    }
                    if (grid.IsWall(new(item.original.X + 1, item.original.Y)))
                    {
                        item.bounds.Width += 5;
                    }
                }
            }
            return converted;
        }
    }
}