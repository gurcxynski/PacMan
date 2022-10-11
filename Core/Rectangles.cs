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
            rectangles.ForEach(delegate (MyRectangle item) { converted.Add(new(item.ToRect()));
            for (int i = 0; i < item.Width; i++)
                {
                    for (int j = 0; j < item.Height; j++)
                    {
                        Game1.self.activeScene.grid.Add(i + item.X, j + item.Y, Grid.FieldType.Wall);
                    }
                }
            });
            return converted;
        }
    }
}