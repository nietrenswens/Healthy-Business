using HealthyBusiness.Engine.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace HealthyBusiness.Engine.Managers
{
    public class AttributeManager<T> where T : IGameAttribute
    {
        private List<T> _attributes = new List<T>();
        private List<T> _attributesToBeAdded = new List<T>();
        private List<T> _attributesToBeRemoved = new List<T>();
        private object _lock = new object();

        public IReadOnlyList<T> Attributes
        {
            get
            {
                lock (_lock)
                {
                    return _attributes.AsReadOnly();
                }
            }
        }

        public IReadOnlyList<T> AttributesToBeAdded => _attributesToBeAdded.AsReadOnly();
        public IReadOnlyList<T> AttributesToBeRemoved => _attributesToBeRemoved.AsReadOnly();

        public void Update(GameTime gameTime)
        {
            foreach (var attribute in _attributesToBeAdded)
            {
                attribute.Load(GameManager.GetGameManager().ContentManager);
                lock (_lock)
                {
                    _attributes.Add(attribute);
                }
            }
            _attributesToBeAdded.Clear();
            foreach (var attribute in _attributes)
            {
                attribute.Update(gameTime);
            }
            foreach (var attribute in _attributesToBeRemoved)
            {
                lock (_lock)
                {
                    _attributes.Remove(attribute);
                    attribute.Unload();
                }
            }
            _attributesToBeRemoved.Clear();
        }

        public void Load(ContentManager content)
        {
            foreach (var attribute in _attributes)
            {
                attribute.Load(content);
            }
        }

        public void Unload()
        {
            foreach (var attribute in _attributes)
            {
                attribute.Unload();
            }
            _attributes.Clear();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var attribute in _attributes)
            {
                attribute.Draw(spriteBatch);
            }
        }

        public void Add(T attribute)
        {
            _attributesToBeAdded.Add(attribute);
        }

        public void Add(T[] attributes)
        {
            _attributesToBeAdded.AddRange(attributes);
        }

        public void Remove(T attribute)
        {
            _attributesToBeRemoved.Add(attribute);
        }
    }
}
