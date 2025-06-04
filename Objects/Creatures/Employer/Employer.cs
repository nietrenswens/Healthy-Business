using HealthyBusiness.Collision;
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
        public int Level = 1;
        private string? _textureName;
        private bool QuotaIsMet = false;
        private bool playerIsInRange = false;
        private Text? _text;

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

            Add(collider);
            base.Load(content);
        }

        public int DetermineQuota()
        {
            return 100 + (Level - 1) * 50;
        }

        public void IncreaseLevel(int? increaseLevelBy = null)
        {
            if (increaseLevelBy.HasValue)
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
            if (player != null && GetGameObject<Collider>() is Collider collider &&
                player.GetGameObject<Collider>() is Collider playerCollider)
            {
                bool isStillColliding = collider.CheckIntersection(playerCollider);

                if (isStillColliding)
                {
                    if (InputManager.GetInputManager().IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.E))
                    {
                        player.SellItems();
                    }

                    var textAttributesList = ((GameScene)GameManager.GetGameManager().CurrentScene).GUIObjects.Attributes
                        .OfType<Text>()
                        .ToList();

                    if (textAttributesList.Count > 0 && _text != null)
                    {
                        return;
                    }

                    playerIsInRange = true;
                    ChangeGui();
                }
                else if (!isStillColliding)
                {
                    playerIsInRange = false;
                    ChangeGui();
                }
            }
        }
    }
}
