using HealthyBusiness.Engine;
using Microsoft.Xna.Framework;

namespace HealthyBusiness.Cameras
{
    public class GameObjectCenteredCamera : Camera
    {
        public GameObject Target;
        public float Zoom;
        private Vector2 _position;
        private float _interpolationSpeed = 0.1f;

        public GameObjectCenteredCamera(GameObject target, float zoom)
        {
            Target = target;
            Zoom = zoom;
            _position = target.WorldPosition;
        }

        public override Matrix GetWorldTransformationMatrix()
        {
            var screenWidth = Globals.SCREENWIDTH;
            var screenHeight = Globals.SCREENHEIGHT;

            var target = Target.Center;

            // Interpolate the camera position
            _position = Vector2.Lerp(_position, target, _interpolationSpeed);

            return Matrix.CreateTranslation(-_position.X, -_position.Y, 0) * Matrix.CreateScale(Zoom) * Matrix.CreateTranslation(screenWidth / 2, screenHeight / 2, 0);
        }
    }
}
