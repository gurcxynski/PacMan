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
            rectangles.ForEach(delegate (MyRectangle item) { converted.Add(new(item.ToRect())); });
            return converted;
        }
    }
}