using HealthyBusiness.Collision;
using HealthyBusiness.Engine.Managers;
using HealthyBusiness.Engine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace HealthyBusiness.Engine
{
    public abstract class GameObject
    {
        public GameObject? Parent { get; set; }
        public Collider? Collider { get; private set; }
        public List<GameObject> Components { get; private set; }
        public CollisionGroup CollisionGroup { get; protected set; }

        public virtual float Width => Collider?.GetBoundingBox().Width ?? 0;
        public virtual float Height => Collider?.GetBoundingBox().Height ?? 0;
        public Vector2 Center
        {
            get
            {
                if (Width == 0 || Height == 0)
                {
                    return Vector2.Zero;
                }
                return new Vector2(WorldPosition.X + Width / 2, WorldPosition.Y + Height / 2);
            }
        }

        public Vector2 LocalPosition;
        public float LocalRotation;
        public float LocalScale;

        private List<GameObject> _componentsToBeAdded = new List<GameObject>();
        private List<GameObject> _componentsToBeRemoved = new List<GameObject>();

        public TileLocation TileLocation
        {
            get
            {
                return new TileLocation(WorldPosition.ToPoint());
            }
        }

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
            foreach (var component in _componentsToBeAdded)
            {
                Components.Add(component);
            }
            _componentsToBeAdded.Clear();

            foreach (var component in Components)
            {
                component.Update(gameTime);
            }

            foreach (var component in _componentsToBeRemoved)
            {
                Components.Remove(component);
            }
            _componentsToBeRemoved.Clear();
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
            // I do not think that we should pass collision events to children.
            // Children might have own collision
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
            _componentsToBeAdded.Add(component);
        }

        public void Remove(GameObject component)
        {
            _componentsToBeRemoved.Add(component);
        }

        public T? GetGameObject<T>() where T : GameObject
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

        public IEnumerable<T> GetGameObjects<T>() where T : GameObject
        {
            foreach (var component in Components)
            {
                if (component is T typedComponent)
                {
                    yield return typedComponent;
                }
            }
        }

        protected void SetCollider(Collider collider)
        {
            this.Collider = collider;
        }
    }
}
