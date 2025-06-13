using HealthyBusiness.Collision;
using HealthyBusiness.Data;
using HealthyBusiness.Engine.GUI;
using HealthyBusiness.Engine.Managers;
using HealthyBusiness.Engine.Utils;
using HealthyBusiness.Objects.Creatures.PlayerCreature;
using HealthyBusiness.Objects.GUI;
using HealthyBusiness.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace HealthyBusiness.Objects.Creatures.Employee
{
    public class Employer : Creature
    {
        private bool _quotaIsMet = false;
        private bool _playerIsInRange = false;
        private Text? _text;

        public Employer(Vector2 spawnPosition) : base(spawnPosition, 100, 100)
        {
            LocalScale = 4;
            CollisionGroup = CollisionGroup.Player;

            // the employer has not been spawed it yet if the quota is 0
            if (GameManager.GetGameManager().GameData.Quota.Amount == 0)
            {
                GameManager.GetGameManager().GameData
                    .Quota.Amount = GameManager.GetGameManager().GameData.Quota.DetermineQuota();
            }
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

            Add(collider);
            base.Load(content);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            var player = GameManager.GetGameManager()
                .CurrentScene.GameObjects
                .OfType<Player>()
                .FirstOrDefault();

            if (player != null)
            {
                HandlePlayerInteraction(player);
            }
        }

        private void ChangeGui()
        {
            var currentScene = (GameScene)GameManager.GetGameManager().CurrentScene;
            if (_playerIsInRange)
            {
                string message = "Press E to sell items to the Dam";
                _text = new Text(
                    "fonts\\pixelated_elegance\\title",
                    message,
                    Color.White,
                    guiStyling: new GUIStyling(
                        verticalFloat: VerticalAlign.Top,
                        horizontalFloat: HorizontalAlign.Center
                    )
                );
                currentScene.GUIObjects.Add(_text);
            }
            else if (_text != null)
            {
                currentScene.GUIObjects.Remove(_text);
                _text = null;
            }
        }

        private void HandlePlayerInteraction(Player player)
        {
            var collider = GetGameObject<Collider>();
            var playerCollider = player.GetGameObject<Collider>();

            if (collider == null || playerCollider == null) return;

            bool isColliding = collider.CheckIntersection(playerCollider);

            if (isColliding)
            {
                if (InputManager.GetInputManager().IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.E))
                {
                    SellItems(player);
                }

                if (_text == null)
                {
                    _playerIsInRange = true;
                    ChangeGui();
                }
            }
            else
            {
                if (_text != null)
                {
                    _playerIsInRange = false;
                    ChangeGui();
                }
            }
        }

        private void SellItems(Player player)
        {
            player.SellItems();
            GameData gameData = GameManager.GetGameManager().GameData;

            // check if the quota is met (balance and current quota)
            _quotaIsMet = gameData.Balance >= GameManager.GetGameManager().GameData.Quota.Amount;

            // if the quota is met, increase the level
            if (_quotaIsMet)
            {
                GameManager.GetGameManager().GameData.Quota.IncreaseEmployerLevel();
                return;
            }

            // if the quota is not met but the deadline is the same day as the current shift day -> game over
            if (gameData.ShiftCount == GameManager.GetGameManager().GameData.Quota.Deadline)
            {
                GameManager.GetGameManager().ChangeScene(new GameOverScene());
            }
        }
    }
}
