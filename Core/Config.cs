using Microsoft.Xna.Framework;

namespace PacMan.Core
{
    public static class Configuration
    {
        public static int cellSize = 20;
        public static Vector2 cells = new(28, 31);
        public static Vector2 windowSize = cells * new Vector2(cellSize);

        public static int basePlayerVel = 175;
        public static int baseEnemyVel = 150;

    }
}
