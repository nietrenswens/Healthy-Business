using HealthyBusiness.Collision;
using HealthyBusiness.Engine;
using HealthyBusiness.Engine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HealthyBusiness.Objects.Items
{
    public class Item : GameObject
    {
        public string Name { get; private set; }
        public Texture2D? Texture { get; private set; }

        private string _textureName;

        //public Texture2D? Texture => _texture;



        public Item(TileLocation tileLocation, string textureName, string name)
        {
            _textureName = textureName;
            WorldPosition = tileLocation.ToVector2();
            CollisionGroup = CollisionGroup.Item;
            Name = name;
        }

        public override void Load(ContentManager content)
        {
            base.Load(content);
            Texture = content.Load<Texture2D>(_textureName);
            SetCollider(new RectangleCollider(new Rectangle(WorldPosition.ToPoint(), new Point(Texture.Width, Texture.Height))));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (Collider is RectangleCollider rectangleCollider)
                rectangleCollider.Shape.Location = WorldPosition.ToPoint();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Draw(Texture, new Rectangle(TileLocation.ToPoint(), new Point(Globals.TILESIZE, Globals.TILESIZE)), Color.White);
        }
    }
}
