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

        public Vector2 Center => WorldPosition + new Vector2(Width, Height) / 2;

        private string _textureName;
        private Texture2D? _texture;

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
            _texture = content.Load<Texture2D>(_textureName);
            SetCollider(new RectangleCollider(new Rectangle(WorldPosition.ToPoint(), new Point(_texture.Width, _texture.Height))));
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
            spriteBatch.Draw(_texture, new Rectangle(TileLocation.ToPoint(), new Point(Globals.TILESIZE, Globals.TILESIZE)), Color.White);
        }
    }
}
