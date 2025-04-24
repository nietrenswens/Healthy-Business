using HealthyBusiness.Collision;
using HealthyBusiness.Engine;
using HealthyBusiness.Engine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace HealthyBusiness.Objects.Items
{
    public class Item : GameObject
    {
        public string Name { get; private set; }
        public Texture2D? Texture { get; private set; }

        private string _textureName;

        //public Texture2D? Texture => _texture;

        public int price;
        public ItemPrice ItemPrice;



        public Item(TileLocation tileLocation, string textureName, string name)
        {
            _textureName = textureName;
            WorldPosition = tileLocation.ToVector2();
            Name = name;
            ItemPrice = ItemPrice.Medium;
            price = InitilizePrice();
        }

        private int InitilizePrice()
        {
            Random random = new Random();

            switch (ItemPrice)
            {
                case ItemPrice.Low:
                    return random.Next(1, 15);
                case ItemPrice.Medium:
                    return random.Next(16, 30);
                case ItemPrice.High:
                    return random.Next(31, 60);
                default:
                    throw new ArgumentOutOfRangeException(nameof(ItemPrice), ItemPrice, null);
            }
        }

        public override void Load(ContentManager content)
        {
            base.Load(content);
            Texture = content.Load<Texture2D>(_textureName);
            var collider = new RectangleCollider(new Rectangle(WorldPosition.ToPoint(), new Point(Texture.Width, Texture.Height)));
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
            spriteBatch.Draw(Texture, new Rectangle(TileLocation.ToPoint(), new Point(Globals.TILESIZE, Globals.TILESIZE)), Color.White);
        }
    }
}
