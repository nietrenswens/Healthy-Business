using HealthyBusiness.Engine;
using HealthyBusiness.Engine.Utils;
using HealthyBusiness.Objects;

namespace HealthyBusiness.Builders
{
    public static class LevelBuilder
    {
        public static GameObject[] CreateRectangularLevel(int width, int height, int offsetX = 0, int offsetY = 0)
        {
            var level = new GameObject[width * height];
            var offset = new TileLocation(offsetX, offsetY);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var tileLocation = new TileLocation(x, y);
                    if (y == height - 1 || y == 0 || x == width - 1 || x == 0)
                    {
                        level[x + y * width] = new Wall(tileLocation + offset);
                    }
                    else
                    {
                        level[x + y * width] = new Floor(tileLocation + offset);
                    }
                }
            }
            return level;
        }
    }
}
