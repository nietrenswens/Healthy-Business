using HealthyBusiness.Collision;
using HealthyBusiness.Engine.Utils;
using HealthyBusiness.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using HealthyBusiness.Objects.GUI;
using HealthyBusiness.Engine.GUI;
using HealthyBusiness.Objects.Creatures.Player;
using HealthyBusiness.Engine.Managers;
using HealthyBusiness.Objects.Items;
using HealthyBusiness.Scenes;

namespace HealthyBusiness.Objects.Creatures.Employee
{
    public class Employer : Creature
    {
        public int Level = 1;
        private string? _textureName;
        private bool QuotaIsMet = false;

        private RectangleCollider? _collider;

        public Employer(Vector2 spawnPosition) : base(spawnPosition, 100, 100)
        {
            LocalScale = 4;
            CollisionGroup = CollisionGroup.Player;
        }

        public Employer(TileLocation tileLocation) : this(tileLocation.ToVector2())
        { }

        public override void Load(ContentManager content)
        {
            Texture = content.Load<Texture2D>("entities\\theDam\\theDam");

            var width = (int)(Texture.Width * LocalScale);
            var height = (int)(Texture.Height * LocalScale);

            var collider = new RectangleCollider(new Rectangle(WorldPosition.ToPoint(), new Point(width, height)));
            collider.LocalPosition = Vector2.Zero;

            _collider = collider;
            
            Add(collider);
            base.Load(content);
        }

        public int DetermineQuota()
        {
            return 100 + (Level - 1) * 50;                                   
        }

        public void IncreaseLevel(int? increaseLevelBy = null)
        {
            if(increaseLevelBy.HasValue)
            {
                Level += increaseLevelBy.Value;
                DetermineQuota();
                return;
            }

            Level++;
            DetermineQuota();
        }
        public void PrintQuotaStatus()
        {
            string message = $"Level: {Level}, Quota: {DetermineQuota()}, Quota Met: {QuotaIsMet}";

            Add(
                new Text("fonts\\pixelated_elegance\\small", message, Color.White, new GUIStyling(verticalFloat: VerticalAlign.Center, horizontalFloat: HorizontalAlign.Left))
            );
        }

        public void SetFeetPosition(TileLocation location)
        {
            WorldPosition = location.ToVector2() - new Vector2(0, Globals.TILESIZE);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void OnCollision(GameObject other)
        {
            base.OnCollision(other);

            // check if we can change this to a player type
            if(other is Creatures.Player.Player) // TODO: fix the namespace bug
            {
                string message = "Press E to interact with The Dam";
                Add(
                    new Text("fonts\\pixelated_elegance\\title",
                        message,
                        Color.White,
                        new GUIStyling(
                            verticalFloat: VerticalAlign.Bottom,
                            horizontalFloat: HorizontalAlign.Center
                        )
                    )
                );
            }
        }
    }
}
