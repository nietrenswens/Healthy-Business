using HealthyBusiness.Cameras;
using HealthyBusiness.Engine;
using HealthyBusiness.Engine.GUI;
using HealthyBusiness.Engine.Utils;
using HealthyBusiness.Objects.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace HealthyBusiness.Scenes
{
    public class LoadingScene : Scene
    {
        public override void Load(ContentManager content)
        {
            base.Load(content);
            SetCamera(new DefaultCamera());
            AddGameObject(new ColoredBackground(Color.LightCoral));
            AddGameObject(new Text("fonts\\pixelated_elegance\\title", "Loading...", Color.White, new GUIStyling(marginTop: 200, horizontalFloat: HorizontalAlign.Center)));
        }
    }
}
