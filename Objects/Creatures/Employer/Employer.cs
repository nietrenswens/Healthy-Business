using HealthyBusiness.Animations;
using HealthyBusiness.Collision;
using HealthyBusiness.Controllers;
using HealthyBusiness.Engine.Utils;
using HealthyBusiness.Engine;
using HealthyBusiness.Objects.Creatures.Player.Modules;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyBusiness.Objects.Creatures.Employee
{
    class Employer : Creature
    {
        private string? _textureName;

        public Employer(Vector2 spawnPosition) : base(spawnPosition, 100, 100)
        {
            LocalScale = 4;
            CollisionGroup = CollisionGroup.Solid;
        }

        public Employer(TileLocation tileLocation) : this(tileLocation.ToVector2())
        { }

        public override void Load(ContentManager content)
        {
            Texture = content.Load<Texture2D>("entities\\theDam\\theDam");
            var width = (int)(Texture.Width * LocalScale);
            var height = (int)(Texture.Height * LocalScale);

            var collider = new RectangleCollider(new Rectangle(WorldPosition.ToPoint(), new Point(width / 4, height / 4)));
            collider.LocalPosition = new Vector2((width / 2) - (width / 8), height - height / 4);
            Add(collider);
            base.Load(content);
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
        }
    }
}
