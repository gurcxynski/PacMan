using Microsoft.Xna.Framework;
using System.IO;

namespace PacMan.Core
{
    public static class Configuration
    {
        public static Vector2 windowSize = new(500, 750); //size of game window, do not modify

        // base game stats

        public static int basePlayerVel = 30;
        public static int enemySpeed = 100;

        // player dampening makes movement more natural

       public static float dampening = 0.9f;

    }
}
