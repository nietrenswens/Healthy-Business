using HealthyBusiness.Engine;
using HealthyBusiness.Engine.GUI;
using HealthyBusiness.Engine.Levels;
using HealthyBusiness.Engine.Managers;
using HealthyBusiness.Engine.Utils;
using HealthyBusiness.Objects.GUI;
using Microsoft.Xna.Framework;
using System;

namespace HealthyBusiness.Data
{
    public class Quota : GameObject
    {
        public int EmployerLevel { get; set; } = 1;
        public int Deadline { get; set; } = 0;
        public int MinimalBalance { get; set; } = 0;

        public int amount = 0;

        private bool QuotaIsMet = false;

        private GameData gameData;

        public Quota(int employerLevel, GameData gameData)
        {
            this.gameData = gameData;
            EmployerLevel = employerLevel;
            Deadline = gameData.ShiftCount + 3;

            if (amount == 0)
            {
                amount = DetermineQuota();
            }
        }

        public int DetermineQuota()
        {
            if (EmployerLevel < 1 || gameData.ShiftCount < 0)
            {
                throw new System.Exception("Invalid game data: Shift count or employer level is not set correctly.");
            }

            int deadline = (int)Math.Ceiling(gameData.ShiftCount / 4.0);

            int baseQuota = 100;
            //int scalingFactor = GameManager.GetGameManager().RNG.Next(50, 80);
            int scalingFactor = 50;
            double exponent = 1.5;
            int levelMultiplier = 25;

            double quota = baseQuota + scalingFactor * Math.Pow(deadline, exponent) + levelMultiplier * EmployerLevel;

            return (int)Math.Round(quota);
        }

        public void IncreaseLevel()
        {
            if (QuotaIsMet)
            {
                EmployerLevel++;
                amount = DetermineQuota();
                PrintQuotaStatus();
            }
        }

        private void PrintQuotaStatus()
        {
            string message = $"Level: {EmployerLevel}, Quota: {amount}, Quota Met: {QuotaIsMet}";

            Add(
                new Text("fonts\\pixelated_elegance\\small", message, Color.Green, new GUIStyling(verticalFloat: VerticalAlign.Center, horizontalFloat: HorizontalAlign.Center))
            );
        }

    }
}
