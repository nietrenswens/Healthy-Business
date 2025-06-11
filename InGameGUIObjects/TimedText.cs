using HealthyBusiness.Engine.Managers;
using HealthyBusiness.Engine.Utils;
using HealthyBusiness.Objects.GUI;
using HealthyBusiness.Scenes;
using Microsoft.Xna.Framework;

namespace HealthyBusiness.InGameGUIObjects
{
    public class TimedText : Text
    {
        private float _timeToLive;
        private float _elapsedTime;

        public TimedText(string fontPath, string text, Color? color, float duration, Color? backgroundColor = null, GUIStyling? guiStyling = null) : base(fontPath, text, color, backgroundColor, guiStyling)
        {
            _timeToLive = duration;
            _elapsedTime = 0f;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _elapsedTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (_elapsedTime >= _timeToLive)
            {
                ((GameScene)GameManager.GetGameManager().CurrentScene).GUIObjects.Remove(this);
            }
        }
    }
}
