using HealthyBusiness.Collision;
using HealthyBusiness.Engine;
using HealthyBusiness.Engine.Managers;
using HealthyBusiness.InGameGUIObjects;
using HealthyBusiness.Objects.Items;
using HealthyBusiness.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Linq;

namespace HealthyBusiness.Objects.Creatures.PlayerCreature.Modules
{
    public class ItemPickupModule : GameObject
    {
        private SoundEffect? _pickupSound;
        private float _pickupVolume = 1f;
        private Vector2 _center => ((CircleCollider)GetGameObject<Collider>()!).Center;

        public Item? SelectedItem { get; private set; }

        public override void Load(ContentManager content)
        {
            base.Load(content);
            if (Parent is not Player)
                throw new Exception("ItemPickupModule can only be added to a Player.");

            if (GetGameObject<CircleCollider>() == null)
                Add(new CircleCollider(new(0, 0), Globals.ITEMPICKUPRANGE));

            _pickupSound = content.Load<SoundEffect>("audio\\pickup");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            var parentCollider = Parent!.GetGameObject<Collider>();
            ((CircleCollider)GetGameObject<Collider>()!).Center = Parent.GetGameObject<Collider>()!.Center;
            CheckCollision();
            CheckInput();
        }

        private void CheckCollision()
        {
            var items = GameManager.GetGameManager()
                .CurrentScene.GameObjects
                .OfType<Item>();

            var closestItem = items
                .Where(item => item.GetGameObject<Collider>()?.Intersects((CircleCollider)GetGameObject<Collider>()) == true)
                .Select(item => new { Item = item, Distance = (_center - item.GetGameObject<Collider>()!.Center).Length() })
                .OrderBy(x => x.Distance)
                .FirstOrDefault();

            if (closestItem?.Item != SelectedItem)
            {
                SelectedItem = closestItem?.Item ?? null;
                ChangeGUI();
            }
        }

        public override void Unload()
        {
            base.Unload();
        }

        private void CheckInput()
        {
            if (InputManager.GetInputManager().IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.E) && SelectedItem != null && SelectedItem is ValuedItem)
            {
                var gameLevel = (GameScene)GameManager.GetGameManager().CurrentScene;

                if (!gameLevel.GUIObjects.Attributes.OfType<Hotbar>()
                    .First()
                    .AddItem((ValuedItem)SelectedItem))
                {
                    return; // no empty slot
                }

                _pickupSound?.Play(_pickupVolume, 0f, 0f);

                GameManager.GetGameManager().CurrentScene.RemoveGameObject(SelectedItem);
                SelectedItem = null;

            }
        }
        private void ChangeGUI()
        {
            if (SelectedItem == null)
            {
                Parent!.Remove(Parent.GetGameObject<SelectedItemText>()!);
            }
            else
            {
                var selectedItemText = new SelectedItemText();
                Parent!.Add(selectedItemText);
            }
        }
    }
}
