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

        private string _textureName;
        private Texture2D? _texture;

        public Item(TileLocation tileLocation, string textureName, string name)
        {
            _textureName = textureName;
            WorldPosition = tileLocation.ToVector2();
            Name = name;
        }

        public override void Load(ContentManager content)
        {
            base.Load(content);
            _texture = content.Load<Texture2D>(_textureName);
            var collider = new RectangleCollider(new Rectangle(WorldPosition.ToPoint(), new Point(_texture.Width, _texture.Height)));
            collider.CollisionGroup = CollisionGroup.Item;
            Add(collider);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (GetGameObject<Collider>() is RectangleCollider rectangleCollider)
                rectangleCollider.Shape.Location = WorldPosition.ToPoint();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Draw(_texture, new Rectangle(TileLocation.ToPoint(), new Point(Globals.TILESIZE, Globals.TILESIZE)), Color.White);
        }
    }
}
