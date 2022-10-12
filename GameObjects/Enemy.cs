using Microsoft.Xna.Framework;
using PacMan.Core;
using System;
using System.Diagnostics;

namespace PacMan.GameObjects
{
    public class Enemy : GameObject
    {
        Direction? dir;
        public Vector2 Velocity;
        Vector2 toStop = new(-1, -1);
        public Enemy(Vector2 arg)
        {
            Texture = Game1.self.textures["enemy"];
            GridPosition = arg;
            Position = Configuration.cellSize * GridPosition;
            SetNewVel();
        }
        public override void Update(GameTime UpdateTime)
        {
            float passed = (float)UpdateTime.ElapsedGameTime.TotalSeconds;

            var newPos = Position + Velocity * passed;
            var newGrid = newPos / Configuration.cellSize;

            if (dir == Direction.Right) newGrid += new Vector2(1, 0);
            if (dir == Direction.Down) newGrid += new Vector2(0, 1);

            if (Game1.self.activeScene.grid.IsWall(newGrid))
            {
                Position = newPos;
            }
            else SetNewVel();

            if (Position.X < 0) Position = new(Configuration.windowSize.X - Texture.Width, Position.Y);
            if (Position.Y < 0) Position = new(Position.X, Configuration.windowSize.Y - Texture.Height);
            if (Position.X + Texture.Width > Configuration.windowSize.X) Position = new(0, Position.Y);
            if (Position.Y + Texture.Height > Configuration.windowSize.Y) Position = new(Position.X, 0);
        }
        void SetNewVel()
        {
            Random rnd = new();
            var last = dir;
            while(last == dir) dir = (Direction?)rnd.Next(0, 4);
            switch (dir)
            {
                case Direction.Left:
                    Velocity = new(-150, 0);
                    break;
                case Direction.Right:
                    Velocity = new(150, 0);
                    break;
                case Direction.Down:
                    Velocity = new(0, 150);
                    break;
                case Direction.Up:
                    Velocity = new(0, -150);
                    break;
                default:
                    break;
            }
        }
    }
}