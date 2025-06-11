using HealthyBusiness.Animations;
using HealthyBusiness.Collision;
using HealthyBusiness.Controllers;
using HealthyBusiness.Engine;
using HealthyBusiness.Engine.Managers;
using HealthyBusiness.Engine.Utils;
using HealthyBusiness.InGameGUIObjects;
using HealthyBusiness.Objects.Creatures.PlayerCreature.Modules;
using HealthyBusiness.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using Microsoft.Xna.Framework.Audio;


namespace HealthyBusiness.Objects.Creatures.PlayerCreature
{
    public class Player : Creature
    {
        private SoundEffect _footstep;
        private float _footstepVolume = 0.25f;

        public Player(Vector2 spawnPosition) : base(spawnPosition, 100, 100)
        {
            LocalScale = 4;
            Add(new CollidableMovementController(CollisionGroup.Solid));
            Add(new PlayerInputController());
            Add(new ItemPickupModule());
            CollisionGroup = CollisionGroup.Player;
        }

        public Player(TileLocation tileLocation) : this(tileLocation.ToVector2())
        { }

        public override void Load(ContentManager content)
        {
            Texture = content.Load<Texture2D>("entities\\player");
            var width = (int)(Texture.Width * LocalScale);
            var height = (int)(Texture.Height * LocalScale);

            var collider = new RectangleCollider(new Rectangle(WorldPosition.ToPoint(), new Point(width / 4, height / 4)));
            collider.LocalPosition = new Vector2((width / 2) - (width / 8), height - height / 4);
            Add(collider);
            base.Load(content);

            Animation = new PlayerAnimation("entities\\daniel");
            _footstep = content.Load<SoundEffect>("audio\\footstep");
        }
        public void SetFeetPosition(TileLocation location)
        {
            WorldPosition = location.ToVector2() - new Vector2(0, Globals.TILESIZE);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Animation.Update(gameTime);
            CheckHealth();
        }

        public override void OnCollision(GameObject other)
        {
            base.OnCollision(other);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Animation.Draw(spriteBatch, WorldPosition, LocalScale / 2);
            base.Draw(spriteBatch);
        }

        public void SellItems()
        {
            var gameManager = GameManager.GetGameManager();
            var currentScene = gameManager.CurrentScene as GameScene;

            var hotbar = currentScene?.GUIObjects.Attributes.OfType<Hotbar>().First();
            if (hotbar == null)
                return;

            var hotbarValue = hotbar.HotbarSlots.Sum(slot => slot.Item?.Price);
            gameManager.GameData.Balance += hotbarValue ?? 0;

            hotbar.Clear();
        }

        public void CheckHealth()
        {
            if (Health <= 0)
            {
                GameManager.GetGameManager().ChangeScene(new GameOverScene());
            }
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            CheckHealth();
        }

        public void PlayFootstepSound()
        {
            _footstep?.Play(_footstepVolume, 0f, 0f);
        }
    }
}
