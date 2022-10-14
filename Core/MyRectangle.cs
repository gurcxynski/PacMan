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
        readonly int cell = Configuration.cellSize;
        public Rectangle ToRect()
        {
            return new(cell * X, cell * Y, cell * Width, cell * Height);
        }

        public Rectangle ReverseRect()
        {
            return new(
                (int)Configuration.windowSize.X - cell * X - cell * Width,
                cell * Y,
                cell * Width,
                cell * Height);
        }
    }
}