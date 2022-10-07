using Microsoft.Xna.Framework;
using PacMan.Core;
using System;
using System.Collections.Generic;

namespace PacMan.GameObjects
{
    public class Enemy : GameObject
    {

        protected List<Vector2> path;
        protected int currentPath = 0;

        protected double lastTurn = 0;
        public Enemy(List<Vector2> patharg)
        {
            Texture = Game1.self.textures["enemy"];
            Position = new(20, 20); // to hide enemies while loading 
            path = patharg;
        }
        public Enemy()
        {
            Texture = Game1.self.textures["enemy"];
            Position = new(20, 20); // to hide enemies while loading 

            // generating random 4 element path for them to move on

            Random rnd = new();
            path = new() {
                new(rnd.Next(0, (int)Configuration.windowSize.X), rnd.Next(0, (int)Configuration.windowSize.Y - 50)),
                new(rnd.Next(0, (int)Configuration.windowSize.X), rnd.Next(0, (int)Configuration.windowSize.Y - 50)),
                new(rnd.Next(0, (int)Configuration.windowSize.X), rnd.Next(0, (int)Configuration.windowSize.Y - 50)),
                new(rnd.Next(0, (int)Configuration.windowSize.X), rnd.Next(0, (int)Configuration.windowSize.Y - 50))};
        }

        // algorithms for calculating position on the looping path

        protected float Lerp(float firstFloat, float secondFloat, float by)
        {
            return firstFloat * (1 - by) + secondFloat * by;
        }
        protected Vector2 VectorLerp(Vector2 firstVector, Vector2 secondVector, float by)
        {
            float retX = Lerp(firstVector.X, secondVector.X, by);
            float retY = Lerp(firstVector.Y, secondVector.Y, by);
            return new Vector2(retX, retY);
        }
        public override void Update(GameTime UpdateTime)
        {
        
            if (lastTurn == 0) lastTurn = UpdateTime.TotalGameTime.TotalSeconds;

            double currentlength = (path[(currentPath + 1) % path.Count] - path[currentPath]).Length();

            double progress = (UpdateTime.TotalGameTime.TotalSeconds - lastTurn);

            // adjusting movement speed to path length

            progress *= Configuration.enemySpeed;
            progress /= currentlength;

            if (progress > 1)
            {
                progress = 0;
                currentPath = (currentPath + 1) % path.Count;
                lastTurn = UpdateTime.TotalGameTime.TotalSeconds;
            }

            Position = VectorLerp(path[currentPath], path[(currentPath + 1) % path.Count], (float)progress);

        }
    }
}