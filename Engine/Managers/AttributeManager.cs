using HealthyBusiness.Engine.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace HealthyBusiness.Engine.Managers
{
    public class AttributeManager<T> where T : IGameAttribute
    {
        public List<T> Attributes { get; private set; } = new List<T>();

        private List<T> _attributesToBeAdded = new List<T>();
        private List<T> _attributesToBeRemoved = new List<T>();

        public void Update(GameTime gameTime)
        {
            foreach (var attribute in _attributesToBeAdded)
            {
                attribute.Load(GameManager.GetGameManager().ContentManager);
                Attributes.Add(attribute);
            }
            _attributesToBeAdded.Clear();
            foreach (var attribute in Attributes)
            {
                attribute.Update(gameTime);
            }
            foreach (var attribute in _attributesToBeRemoved)
            {
                Attributes.Remove(attribute);
            }
            _attributesToBeRemoved.Clear();
        }

        public void Load(ContentManager content)
        {
            foreach (var attribute in Attributes)
            {
                attribute.Load(content);
            }
        }

        public void Unload()
        {
            foreach (var attribute in Attributes)
            {
                attribute.Unload();
            }
            Attributes.Clear();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var attribute in Attributes)
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

    }
}
