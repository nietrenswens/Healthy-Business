using HealthyBusiness.Collision;
using HealthyBusiness.Engine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace HealthyBusiness.Engine
{
    public abstract class GameObject
    {
        public GameObject Parent { get; set; }
        public IReadOnlyCollection<GameObject> Components { get; private set; }
        public CollisionGroup CollisionGroup { get; private set; }
        public Collider Collider { get; private set; }

        public Vector2 LocalPosition;
        public float LocalRotation;
        public float LocalScale;

        public Vector2 WorldPosition
        {
            get
            {
                return ToWorldSpace(LocalPosition);
            }
            set
            {
                LocalPosition = ToLocalPosition(value);
            }
        }

        public float WorldScale
        {
            get
            {
                return Parent == null ? LocalScale : LocalScale * Parent.WorldScale;
            }
            set
            {
                LocalScale = Parent == null ? value : value / Parent.WorldScale;
            }
        }

        public float WorldRotation
        {
            get
            {
                return Parent == null ? LocalRotation : LocalRotation + Parent.WorldRotation;
            }
            set
            {
                LocalRotation = Parent == null ? value : value - Parent.WorldRotation;
            }
        }

        public GameObject()
        {
            Components = new List<GameObject>();
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (var component in Components)
            {
                component.Update(gameTime);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (var component in Components)
            {
                component.Draw(spriteBatch);
            }
        }

        public virtual void Load(ContentManager content)
        {
            foreach (var component in Components)
            {
                component.Load(content);
            }
        }

        public virtual void OnCollision(GameObject other)
        {
            foreach (var component in Components)
            {
                component.OnCollision(other);
            }
        }

        public Vector2 ToWorldSpace(Vector2 localSpace)
        {
            if (Parent == null)
            {
                return localSpace;
            }
            Vector2 position = LocalPosition * Parent.WorldScale;
            position.Rotate(Parent.WorldRotation);
            return position + Parent.WorldPosition;
        }

        public Vector2 ToLocalPosition(Vector2 worldSpace)
        {
            if (Parent == null)
            {
                return worldSpace;
            }
            Vector2 position = worldSpace - Parent.WorldPosition;
            position.Rotate(-Parent.WorldRotation);
            return position / Parent.WorldScale;
        }

        public void Add(GameObject component)
        {
            component.Parent = this;
            component.Load(GameManager.GetGameManager().ContentManager);
            (Components as List<GameObject>).Add(component);
        }

        public GameObject GetGameObject<T>() where T : GameObject
        {
            foreach (var component in Components)
            {
                if (component is T)
                {
                    return component as T;
                }
            }
            return null;
        }

        public IEnumerable<GameObject> GetGameObjects<T>() where T : GameObject
        {
            foreach (var component in Components)
            {
                if (component is T)
                {
                    yield return component;
                }
            }
        }

        protected void SetCollider(Collider collider, CollisionGroup group = CollisionGroup.None)
        {
            this.Collider = collider;
            this.CollisionGroup = group;
        }
    }
}
