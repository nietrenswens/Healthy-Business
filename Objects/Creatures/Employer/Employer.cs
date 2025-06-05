using HealthyBusiness.Collision;
using HealthyBusiness.Data;
using HealthyBusiness.Engine;
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
        public int Level = GameManager.GetGameManager().GameData.EmployerLevel;
        private bool QuotaIsMet = false;
        private bool playerIsInRange = false;
        private Text? _text;
        private GameData gameData = GameManager.GetGameManager().GameData;

        public Employer(Vector2 spawnPosition) : base(spawnPosition, 100, 100)
        {
            LocalScale = 4;
            CollisionGroup = CollisionGroup.Player;

            // the employer has not been spawed it yet if the quota is 0
            if (GameManager.GetGameManager().GameData.Quota == 0)
            {
                gameData.Quota = DetermineQuota();
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

        public void PrintQuotaStatus()
        {
            string message = $"Level: {Level}, Quota: {gameData.Quota}, Quota Met: {QuotaIsMet}";

            Add(
                new Text("fonts\\pixelated_elegance\\small", message, Color.Green, new GUIStyling(verticalFloat: VerticalAlign.Center, horizontalFloat: HorizontalAlign.Center))
            );
        }

        public void SetFeetPosition(TileLocation location)
        {
            WorldPosition = location.ToVector2() - new Vector2(0, Globals.TILESIZE);
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

        public override void OnCollision(GameObject other)
        {
            base.OnCollision(other);
        }

        private void ChangeGui()
        {
            var currentScene = (GameScene)GameManager.GetGameManager().CurrentScene;
            if (playerIsInRange)
            {
                string message = "Press E to sell items to the Dam";
                _text = new Text(
                    "fonts\\pixelated_elegance\\title",
                    message,
                    Color.White,
                    new GUIStyling(
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

        // TODO: make the Creatures.Player.Player -> Player but it is not working rn
        private void HandlePlayerInteraction(Player player)
        {
            if (player == null) return;

            var collider = GetGameObject<Collider>() as Collider;
            var playerCollider = player.GetGameObject<Collider>() as Collider;
            if (collider == null || playerCollider == null) return;

            bool isColliding = collider.CheckIntersection(playerCollider);

            if (isColliding)
            {
                if (InputManager.GetInputManager().IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.E))
                {
                    player.SellItems();
                    GameData gameData = GameManager.GetGameManager().GameData;

                    // check if the quota is met (balance and current quota)
                    QuotaIsMet = gameData.Balance >= GameManager.GetGameManager().GameData.Quota;

                    // if the quota is met, increase the level
                    if (QuotaIsMet) IncreaseLevel();

                    // if the quota is not met but the deadline is the same day as the current shift day -> game over
                    if (!QuotaIsMet && gameData.ShiftCount == GameManager.GetGameManager().GameData.Deadline)
                    {
                        // TODO: show game over screen
                        GameManager.GetGameManager().Exit();
                    }
                    // if the quota is not met but the deadline is not the same day as the current shift day
                    else if (!QuotaIsMet && gameData.ShiftCount < GameManager.GetGameManager().GameData.Deadline)
                    {
                        return;
                    }
                }

                if (_text == null)
                {
                    playerIsInRange = true;
                    ChangeGui();
                }
            }
            else
            {
                if (_text != null)
                {
                    playerIsInRange = false;
                    ChangeGui();
                }
            }
        }

        private void SyncLevelWithGameData()
        {
            Level = GameManager.GetGameManager().GameData.EmployerLevel;
        }
    }
}
