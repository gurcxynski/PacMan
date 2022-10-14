using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PacMan.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;

namespace PacMan.GameObjects
{
    public enum Leaving
    {
        stay,
        phase1,
        phase2,
        left
    }
    public enum Phase
    {
        Scatter,
        Chase,
        Frightened
    }
    public abstract class Enemy : GameObject
    {
        protected Vector2 Velocity;
        protected Timer timer;
        protected Vector2 target;
        protected Vector2 prevTile;
        protected Vector2 nextTile;
        protected double? lastTurn;
        public Phase phase = Phase.Scatter;
        public Leaving state = Leaving.stay;
        public Texture2D orgTexture;

        protected Enemy()
        {
            timer = new(7000)
            {
                Enabled = true
            };
            timer.Elapsed += ChangePhase;
        }
        public override void Update(GameTime UpdateTime)
        {
            if (state == Leaving.stay) return;
            lastTurn ??= UpdateTime.TotalGameTime.TotalSeconds;

            if (state == Leaving.phase1 || state == Leaving.phase2)
            {
                if (state == Leaving.phase1)
                {
                    if (MoveTo(new Vector2(13, 12), UpdateTime)) state = Leaving.phase2;

                }
                else if (MoveTo(new Vector2(13, 10), UpdateTime)) state = Leaving.left;
                return;
            }


            MoveTo(nextTile, UpdateTime);

        }
        protected static float Lerp(float firstFloat, float secondFloat, float by)
        {
            return firstFloat * (1 - by) + secondFloat * by;
        }
        protected static Vector2 VectorLerp(Vector2 firstVector, Vector2 secondVector, float by)
        {
            float retX = Lerp(firstVector.X, secondVector.X, by);
            float retY = Lerp(firstVector.Y, secondVector.Y, by);
            return new Vector2(retX, retY);
        }
        public void Leave(GameTime updateTime)
        {
            state = Leaving.phase1;
            lastTurn = updateTime.TotalGameTime.TotalSeconds;
            Game1.self.activeScene.jail.Remove(this);
        }
        protected bool MoveTo(Vector2 to, GameTime updateTime)
        {
            double? progress = updateTime.TotalGameTime.TotalSeconds - lastTurn;
            Vector2 relative = to - GridPosition;

            if (progress * Configuration.baseEnemyVel > Configuration.cellSize)
            {
                prevTile = GridPosition;
                GridPosition = to;
                Position = Configuration.cellSize * GridPosition;
                QueueTurn(GridPosition);
                lastTurn = updateTime.TotalGameTime.TotalSeconds;
                ChangeTexture();
                return true;
            }
            else
            {
                Position = (float)progress * Configuration.baseEnemyVel * relative + GridPosition * Configuration.cellSize;
            }
            if (Position == Vector2.Zero) Debug.WriteLine(progress + " " + GridPosition);
            return false;
        }

        protected void ChangeTexture()
        {
            if (phase == Phase.Frightened) return;
            Vector2 relative = nextTile - GridPosition;
            switch (relative)
            {
                case Vector2(1, 0):
                    Texture = Game1.self.textures[orgTexture.Name[0] + "r"];
                    break;
                case Vector2(-1, 0):
                    Texture = Game1.self.textures[orgTexture.Name[0] + "l"];
                    break;
                case Vector2(0, -1):
                    Texture = Game1.self.textures[orgTexture.Name[0] + "u"];
                    break;
                case Vector2(0, 1):
                    Texture = Game1.self.textures[orgTexture.Name[0] + "d"];
                    break;
                default:
                    break;
            }
        }

        protected void QueueTurn(Vector2 from)
        {
            Random rng = new();
            Vector2 left = from + new Vector2(-1, 0);
            Vector2 right = from + new Vector2(1, 0);
            Vector2 up = from + new Vector2(0, -1);
            Vector2 down = from + new Vector2(0, 1);
            List<Vector2> turns = new()
            {
                left,
                right,
                up,
                down
            };

            List<Vector2> viable = new();
            foreach (Vector2 item in turns)
            {
                if (Game1.self.activeScene.grid.CanMoveInto(item) && item != prevTile && item.X > 0 && item.X < Configuration.cells.X - 1 && item.Y > 0 && item.Y < Configuration.cells.Y - 1)
                {
                    viable.Add(item);
                }
            }
            (float, Vector2) min = (float.MaxValue, new());
            foreach (Vector2 item in viable)
            {
                if (phase == Phase.Frightened)
                {
                    nextTile = viable[rng.Next(0, viable.Count)];
                    return;
                }
                float len = (item - (phase == Phase.Chase ? Game1.self.activeScene.player.GridPosition : target)).Length();
                if (len < min.Item1) min = (len, item);
            }
            if (nextTile == min.Item2 - from) return;
            nextTile = min.Item2;
        }
        protected void ChangePhase(object source, ElapsedEventArgs e)
        {
            if (phase == Phase.Chase)
            {
                phase = Phase.Scatter;
                timer = new(7000);
            }
            else if (phase == Phase.Scatter)
            {
                phase = Phase.Chase;
                timer = new(21000);
            }
            else if (phase == Phase.Frightened)
            {
                phase = Phase.Scatter;
                Texture = orgTexture;
                timer = new(7000);
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position - new Vector2(5, 5) + new Vector2(0, 50), Color.White);
        }
        public void Frighten()
        {
            if (state != Leaving.left || phase == Phase.Frightened) return;
            phase = Phase.Frightened;
            orgTexture = Texture;
            Texture = Game1.self.textures["scared"];
            timer = new(5000);
        }
        protected static Vector2 ConvertToVec(Direction turn)
        {
            return turn switch
            {
                Direction.Right => new Vector2(-1, 0),
                Direction.Left => new Vector2(1, 0),
                Direction.Down => new Vector2(0, -1),
                Direction.Up => new Vector2(0, 1),
                _ => new Vector2(0, 0),
            };
        }
    }
}