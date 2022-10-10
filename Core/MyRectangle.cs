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
            return new(Configuration.cellSize * X, Configuration.cellSize * Y, Configuration.cellSize * Width, Configuration.cellSize * Height);
        }
    }
}