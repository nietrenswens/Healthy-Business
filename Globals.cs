﻿namespace HealthyBusiness
{
    public static class Globals
    {
        public const int SCREENWIDTH = 1280;
        public const int SCREENHEIGHT = 720;
        public const int TILESIZE = 64;

        public const int MAPWIDTH = 20;
        public const int MAPHEIGHT = 10;

        public const bool DEBUG = false;

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

        #region Enemy globals
        public const int ENEMY_SPAWN_THRESHOLD_LOW = 30;
        public const int ENEMY_SPAWN_THRESHOLD_MEDIUM = 60;
        public const int ENEMY_SPAWN_THRESHOLD_HIGH = 100;
        #endregion

        #region Path finding
        public const int MAX_PATHFINDING_COST = 200;
        #endregion
    }
}
