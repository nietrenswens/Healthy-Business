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
using System;

namespace HealthyBusiness.Objects.Creatures.Employee
{
    public class Employer : Creature
    {
        public int Level = 1;
        private string? _textureName;
        private bool QuotaIsMet = false;

        private RectangleCollider? _collider;

        private bool playerIsInRange = false;

        private Text? _text;

        GameScene currentScene;
        GameManager gm = GameManager.GetGameManager();

        public Employer(Vector2 spawnPosition) : base(spawnPosition, 100, 100)
        {
            LocalScale = 4;
            CollisionGroup = CollisionGroup.Player;
            currentScene = (GameScene)gm.CurrentlyLoadingScene;
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

            var player = GameManager.GetGameManager()
                .CurrentScene.GameObjects
                .OfType<Creatures.Player.Player>()
                .FirstOrDefault();

            if (player != null && GetGameObject<Collider>() is Collider collider &&
                player.GetGameObject<Collider>() is Collider playerCollider)
            {
                bool isStillColliding = collider.CheckIntersection(playerCollider);

                if (isStillColliding)
                {
                   var textAttributesList = currentScene.GUIObjects.Attributes
                        .OfType<Text>()
                        .ToList();

                    if (textAttributesList.Count > 0 && _text != null)
                    {
                        System.Diagnostics.Debug.WriteLine("text already exists");
                        return;
                    }

                    playerIsInRange = true;
                    ChangeGui();
                }
                else if(!isStillColliding)
                {
                    playerIsInRange = false;
                    RemoveInteractionText();
                }
            }
        }

        public override void OnCollision(GameObject other)
        {
            base.OnCollision(other);
        }


        private void ChangeGui()
        {
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
        }

        private void RemoveInteractionText()
        {

            currentScene.GUIObjects.Remove(_text);
            _text = null;
        }
    }
}
