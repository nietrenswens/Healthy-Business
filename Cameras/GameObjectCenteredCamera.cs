using HealthyBusiness.Engine;
using Microsoft.Xna.Framework;

namespace HealthyBusiness.Cameras
{
    public class GameObjectCenteredCamera : Camera
    {
        public GameObject Target;
        public float Zoom;

        public GameObjectCenteredCamera(GameObject target, float zoom)
        {
            Target = target;
            Zoom = zoom;
        }

        public override Matrix GetWorldTransformationMatrix()
        {
            var screenWidth = Globals.SCREENWIDTH;
            var screenHeight = Globals.SCREENHEIGHT;

            var x = Target.WorldPosition.X;
            var y = Target.WorldPosition.Y;

            return Matrix.CreateTranslation(-x, -y, 0) * Matrix.CreateScale(Zoom) * Matrix.CreateTranslation(screenWidth / 2, screenHeight / 2, 0);
        }
    }
}
