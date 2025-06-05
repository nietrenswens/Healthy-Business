using HealthyBusiness.Engine.Levels;
using HealthyBusiness.Engine.Managers;
using System;

namespace HealthyBusiness.Data
{
    public class Quota
    {
        public int EmployerLevel { get; set; } = 1;
        public int Deadline { get; set; } = 0;
        public int MinimalBalance { get; set; } = 0;

        public Quota(int employerLevel)
        {
            Deadline = ShiftCount + 3;
        }

        public int DetermineQuota()
        {
            GameData gameData = GameManager.GetGameManager().GameData;

            if (gameData.ShiftCount < 1 || gameData.EmployerLevel < 0)
            {
                throw new System.Exception("Invalid game data: Shift count or employer level is not set correctly.");
            }

            int deadline = (int)Math.Ceiling(gameData.ShiftCount / 4.0);

            int baseQuota = 100;
            int scalingFactor = GameManager.GetGameManager().RNG.Next(50, 80);
            double exponent = 1.5;
            int levelMultiplier = 25;

            double quota = baseQuota + scalingFactor * Math.Pow(deadline, exponent) + levelMultiplier * gameData.EmployerLevel;

            return (int)Math.Round(quota);
        }

        public void IncreaseLevel()
        {
            if (QuotaIsMet)
            {
                Level++;
                gameData.EmployerLevel = Level;
                gameData.Quota = DetermineQuota();
                PrintQuotaStatus();
            }
        }

    }
}
