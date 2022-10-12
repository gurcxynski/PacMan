using Microsoft.Xna.Framework;
using System.Text.Json.Serialization;

namespace PacMan.Core
{
    public class MyRectangle
    {
        [JsonInclude] public int X;
        [JsonInclude] public int Y;
        [JsonInclude] public int Width;
        [JsonInclude] public int Height;
        public Rectangle ToRect()
        {
            return new(Configuration.cellSize * X + 5, Configuration.cellSize * Y + 5, Configuration.cellSize * Width - 10, Configuration.cellSize * Height - 10);
        }
    }
}