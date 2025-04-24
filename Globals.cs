namespace HealthyBusiness
{
    public static class Globals
    {
        public const int SCREENWIDTH = 1280;
        public const int SCREENHEIGHT = 720;
        public const int TILESIZE = 64;

        public const int MAPWIDTH = 20;
        public const int MAPHEIGHT = 10;

        #region Player globals
        /// <summary>
        /// The range at which the player can pick up items in pixels.
        /// </summary>
        public const int ITEMPICKUPRANGE = TILESIZE * 1;
        #endregion

        #region Hotbar globals
        public const int HOTBAR_SLOTS = 5;
        public const int HOTBAR_SLOT_SIZE = 100;
        public const int HOTBAR_SLOT_MARGIN = 10;
        #endregion
    }
}
