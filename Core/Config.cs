using Microsoft.Xna.Framework;

namespace PacMan.Core
{
    public static class Configuration
    {
        public static int cellSize = 20;

        public static Vector2 windowSize = 26 * new Vector2(cellSize);

        public static int basePlayerVel = 30;
        public static int enemySpeed = 100;

    }
}
