using HealthyBusiness.Collision;
using HealthyBusiness.Engine;
using HealthyBusiness.Engine.Managers;
using HealthyBusiness.InGameGUIObjects;
using HealthyBusiness.Objects.Items;
using HealthyBusiness.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Linq;

namespace HealthyBusiness.Objects.Creatures.PlayerCreature.Modules
{
    public class ItemPickupModule : GameObject
    {
        public Item? SelectedItem { get; private set; }

        private Vector2 _center => ((CircleCollider)GetGameObject<Collider>()!).Center;

        public override void Load(ContentManager content)
        {
            base.Load(content);
            if (Parent is not Player)
                throw new Exception("ItemPickupModule can only be added to a Player.");

            if (GetGameObject<CircleCollider>() == null)
                Add(new CircleCollider(new(0, 0), Globals.ITEMPICKUPRANGE));
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
                //.Where(item => (bool)(item.GetGameObject<Collider>()?.Intersects((CircleCollider)GetGameObject<Collider>())))
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
