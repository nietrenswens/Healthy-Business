namespace HealthyBusiness
{
    public static class Globals
    {
        public const int SCREENWIDTH = 1920;
        public const int SCREENHEIGHT = 1080;
        public const int TILESIZE = 64;

        public const int MAPWIDTH = 20;
        public const int MAPHEIGHT = 10;

        #region Player globals
        /// <summary>
        /// The range at which the player can pick up items in pixels.
        /// </summary>
        public const int ITEMPICKUPRANGE = TILESIZE * 1;
        #endregion
    }
}
