﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PacMan.Core;
using System.Diagnostics;

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
        Direction dir;
        Direction? queuedTurn;
        public Player()
        {
            Texture = Game1.self.textures["player"];

            GridPosition = new(12, 12);
            Position = Configuration.cellSize * GridPosition;

            Velocity = new(-150, 0);
            dir = Direction.Left;

            Game1.self.activeScene.grid.Add((int)GridPosition.X, (int)GridPosition.Y, Grid.FieldType.Player);
        }

        public override void Update(GameTime UpdateTime)
        {
            base.Update(UpdateTime);

            if (Position.X < 0) Position = new(0, Position.Y);
            if (Position.Y < 0) Position = new(Position.X, 0);
            if (Position.X + Texture.Width > Configuration.windowSize.X) Position = new(Configuration.windowSize.X - Texture.Width, Position.Y);
            if (Position.Y + Texture.Height > Configuration.windowSize.Y) Position = new(Position.X, Configuration.windowSize.Y - Texture.Height);

            GridPosition = new((int)(Position.X / Configuration.cellSize), (int)(Position.Y / Configuration.cellSize));

            TryTurn();
        }
        public void Queue(Direction arg)
        {
            if (arg == dir) return;
            queuedTurn ??= arg;
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
                        Velocity = new(-150, 0);
                        Position = new(Position.X, GridPosition.Y * cell);
                        queuedTurn = null;
                    }
                    break;
                case Direction.Right:
                    if (relativePos.Y < 5 && grid.CanMoveInto(GridPosition + new Vector2(1, 0)))
                    {
                        dir = Direction.Right;
                        Velocity = new(150, 0);
                        Position = new(Position.X, GridPosition.Y * cell);
                        queuedTurn = null;
                    }
                    break;
                case Direction.Down:
                    if (relativePos.X < 5 && grid.CanMoveInto(GridPosition + new Vector2(0, 1)))
                    {
                        dir = Direction.Down;
                        Velocity = new(0, 150);
                        Position = new(GridPosition.X * cell, Position.Y);
                        queuedTurn = null;
                    }
                    break;
                case Direction.Up:
                    if (relativePos.X < 5 && grid.CanMoveInto(GridPosition + new Vector2(0, -1)))
                    {
                        dir = Direction.Up;
                        Velocity = new(0, -150);
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
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
