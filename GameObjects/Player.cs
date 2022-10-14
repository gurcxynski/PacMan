using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PacMan.Core;

namespace PacMan.GameObjects
{
    public enum Direction
    {
        Left,
        Right,
        Down,
        Up
    }
    public class Player : GameObject
    {
        Direction? dir;
        Direction? queuedTurn;
        public Vector2 Velocity;
        double changedTexture = 0;
        public Player()
        {
            Texture = Game1.self.textures["player"];

            GridPosition = new(13, 22);
            Position = Configuration.cellSize * GridPosition + new Vector2(Configuration.cellSize / 2, 0);

            Velocity = new(Configuration.basePlayerVel, 0);
            dir = Direction.Right;

            Game1.self.activeScene.grid.Add((int)GridPosition.X, (int)GridPosition.Y, Grid.FieldType.Player);
        }

        public override void Update(GameTime UpdateTime)
        {
            float passed = (float)UpdateTime.ElapsedGameTime.TotalSeconds;
            var newPos = Position + Velocity * passed;
            var newGrid = new Vector2((int)(newPos / Configuration.cellSize).X, (int)(newPos / Configuration.cellSize).Y);

            if (Game1.self.activeScene.grid.CanMoveInto(newGrid))
            {
                if ((dir == Direction.Right && !Game1.self.activeScene.grid.CanMoveInto(newGrid + new Vector2(1, 0)))
                    || (dir == Direction.Down && !Game1.self.activeScene.grid.CanMoveInto(newGrid + new Vector2(0, 1))))
                {
                    Velocity = Vector2.Zero;
                    Position = Configuration.cellSize * (GridPosition + (dir == Direction.Right ? new Vector2(1,0) : new Vector2(0,1)));
                    dir = null;
                }
                else Position = newPos;
            }
            else
            {
                Velocity = Vector2.Zero;
                dir = null;
            }

            if (Position.X < 0) Position = new(Configuration.windowSize.X - Texture.Width, Position.Y);
            if (Position.Y < 0) Position = new(Position.X, Configuration.windowSize.Y - Texture.Height);
            if (Position.X + Texture.Width > Configuration.windowSize.X) Position = new(0, Position.Y);
            if (Position.Y + Texture.Height > Configuration.windowSize.Y) Position = new(Position.X, 0);

            GridPosition = new((int)(Position.X / Configuration.cellSize), (int)(Position.Y / Configuration.cellSize));

            TryTurn();
           
            if(UpdateTime.TotalGameTime.TotalSeconds - changedTexture > 0.1)
            {
                if (Texture == Game1.self.textures["player"] && dir is not null)
                {
                    Texture = Game1.self.textures["open"+dir.ToString()];
                }
                else Texture = Game1.self.textures["player"];
                changedTexture = UpdateTime.TotalGameTime.TotalSeconds;
            }


        }
        public void Queue(Direction arg)
        {
            if (arg == dir) return;
            queuedTurn = arg;
        }
        bool TryTurn()
        {
            var cell = Configuration.cellSize;
            var grid = Game1.self.activeScene.grid;
            Vector2 relativePos = new(Position.X % cell, Position.Y % cell);
            switch (queuedTurn)
            {
                case Direction.Left:
                    if (relativePos.Y < 5 && grid.CanMoveInto(GridPosition + new Vector2(-1,0)))
                    {
                        dir = Direction.Left;
                        Velocity = new(-Configuration.basePlayerVel, 0);
                        Position = new(Position.X, GridPosition.Y * cell);
                        queuedTurn = null;
                    }
                    break;
                case Direction.Right:
                    if (relativePos.Y < 5 && grid.CanMoveInto(GridPosition + new Vector2(1, 0)))
                    {
                        dir = Direction.Right;
                        Velocity = new(Configuration.basePlayerVel, 0);
                        Position = new(Position.X, GridPosition.Y * cell);
                        queuedTurn = null;
                    }
                    break;
                case Direction.Down:
                    if (relativePos.X < 5 && grid.CanMoveInto(GridPosition + new Vector2(0, 1)))
                    {
                        dir = Direction.Down;
                        Velocity = new(0, Configuration.basePlayerVel);
                        Position = new(GridPosition.X * cell, Position.Y);
                        queuedTurn = null;
                    }
                    break;
                case Direction.Up:
                    if (relativePos.X < 5 && grid.CanMoveInto(GridPosition + new Vector2(0, -1)))
                    {
                        dir = Direction.Up;
                        Velocity = new(0, -Configuration.basePlayerVel);
                        Position = new(GridPosition.X * cell, Position.Y);
                        queuedTurn = null;
                    }
                    break;
                default:
                    break;
            }
            return false;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position - new Vector2(5, 5) + new Vector2(0, 50), Color.White);
        }
    }
}
