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

                converted.Add(new(item.ToRect()));
                converted.Add(new(item.ReverseRect()));
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
                for (int i = 0; i < item.bounds.Width / Configuration.cellSize; i++)
                {
                    for (int j = 0; j < item.bounds.Height / Configuration.cellSize; j++)
                    {
                        grid.Add(i + item.bounds.X / Configuration.cellSize, j + item.bounds.Y / Configuration.cellSize, Grid.FieldType.Wall);
                    }
                }
            }
            return converted;
        }
    }
}