
using HealthyBusiness.Collision;
using HealthyBusiness.Controllers;
using HealthyBusiness.Engine;
using HealthyBusiness.Engine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HealthyBusiness.Objects.Creatures.Player
{
    public class Player : Creature
    {
        private Texture2D _texture;
        private float speed = 0.5f;
        public Player(Vector2 spawnPosition) : base(spawnPosition)
        {
            Health = 100;
            MaxHealth = 100;
            Add(new PlayerInputController());
        }

        public override void Load(ContentManager content)
        {
            base.Load(content);
            _texture = new Texture2D(GameManager.GetGameManager().GraphicsDevice, 1, 1);
            _texture.SetData(new Color[] { Color.White });
            SetCollider(new RectangleCollider(new Rectangle(WorldPosition.ToPoint(), new Point(100, 100))), CollisionGroup.Player);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (Collider is RectangleCollider rectangleCollider && Collider != null)
            {
                rectangleCollider.Shape.Location = WorldPosition.ToPoint();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, new Rectangle(WorldPosition.ToPoint(), new Point(100, 100)), Color.White);
        }

        public void Move(Vector2 direction, GameTime gameTime)
        {
            var velocity = direction * speed * gameTime.ElapsedGameTime.Milliseconds;
            var rectCollider = (RectangleCollider)Collider;

            var steps = 10;
            var steppedVelocity = velocity / steps;

            for (int i = 0; i < steps; i++)
            {
                var destination = WorldPosition + steppedVelocity;
                bool collided = false;
                var tempCollider = new RectangleCollider(new Rectangle(destination.ToPoint(), rectCollider.Shape.Size));

                foreach (var gameObject in GameManager.GetGameManager().GetGameObjects(CollisionGroup.Wall))
                {
                    if (tempCollider.CheckIntersection(gameObject.Collider))
                    {
                        collided = true;
                        break;
                    }
                }

                if (!collided)
                    WorldPosition += steppedVelocity;
            }
        }

        public override void OnCollision(GameObject other)
        {
            if (other.CollisionGroup == CollisionGroup.Wall)
            {
                if (Collider is RectangleCollider playerRect && other.Collider is RectangleCollider wallRect)
                {
                    Rectangle overlap = Rectangle.Intersect(playerRect.Shape, wallRect.Shape);

                    if (overlap.Width > 0 && overlap.Height > 0)
                    {
                        if (overlap.Width < overlap.Height)
                        {
                            if (playerRect.Shape.Center.X < wallRect.Shape.Center.X)
                            {
                                WorldPosition = new Vector2(WorldPosition.X - overlap.Width, WorldPosition.Y);
                            } else
                            {
                                WorldPosition = new Vector2(WorldPosition.X + overlap.Width, WorldPosition.Y);
                            }
                        } else
                        {
                            if (playerRect.Shape.Center.Y < wallRect.Shape.Center.Y)
                            {
                                WorldPosition = new Vector2(WorldPosition.X, WorldPosition.Y - overlap.Height);
                            } else
                            {
                                WorldPosition = new Vector2(WorldPosition.X, WorldPosition.Y + overlap.Height);
                            }
                        }

                        // Update collider position after adjustment
                        if (Collider is RectangleCollider rect)
                        {
                            rect.Shape.Location = WorldPosition.ToPoint();
                        }
                    }
                } else if (other.Collider != null)
                {
                    Vector2 direction = WorldPosition - other.WorldPosition;
                    if (direction != Vector2.Zero)
                    {
                        direction.Normalize();
                        WorldPosition += direction * 1.0f; // Push away from collision
                    }
                }
            }

            base.OnCollision(other);
        }


    }
}
