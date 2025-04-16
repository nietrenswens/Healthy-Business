using Microsoft.Xna.Framework;

namespace HealthyBusiness.Cameras
{
    public class DefaultCamera : Camera
    {
        public override Matrix GetWorldTransformationMatrix()
        {
            return Matrix.CreateScale(1f);
        }
    }
}
