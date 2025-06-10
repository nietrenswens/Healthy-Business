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
        private bool _quotaIsMet = false;

        private GameData _gameData;

        public int EmployerLevel { get; set; }
        public int Deadline { get; set; }
        public int MinimalBalance { get; set; }

        public int Amount { get; set; }

        

        public Quota(int employerLevel, GameData gameData)
        {
            _gameData = gameData;
            EmployerLevel = employerLevel;
            Deadline = gameData.ShiftCount + 3;
            MinimalBalance = 0;
            Amount = 0;

            if (Amount == 0)
            {
                Amount = DetermineQuota();
            }
        }

        public int DetermineQuota()
        {
            if (EmployerLevel < 1)
            {
                throw new Exception("Invalid game data: Employer level is not set correctly.");
            }
            if (_gameData.ShiftCount < 0)
            {
                throw new Exception("Invalid game data: Shift count is not set correctly.");
            }

            int deadline = (int)Math.Ceiling(_gameData.ShiftCount / 4.0);

            int baseQuota = 100;
            int scalingFactor = 50;
            double exponent = 1.5;
            int levelMultiplier = 25;

            double quota = baseQuota + scalingFactor * Math.Pow(deadline, exponent) + levelMultiplier * EmployerLevel;

            return (int)Math.Round(quota);
        }

        public void IncreaseEmployerLevel()
        {
            EmployerLevel++;
            _gameData.Balance = _gameData.Balance - _gameData.Quota.Amount; // give the player the remaining balance after meeting the quota so he can get further in the levels
            Amount = DetermineQuota();
            PrintQuotaStatus();

            _gameData.Quota.Deadline = _gameData.ShiftCount + 3; // reset the deadline for the next quota
        }

        private void PrintQuotaStatus()
        {
            string message = $"Level: {EmployerLevel}, Quota: {Amount}, Quota Met: {_quotaIsMet}";

            Add(
                new Text("fonts\\pixelated_elegance\\small", message, Color.Green, new GUIStyling(verticalFloat: VerticalAlign.Center, horizontalFloat: HorizontalAlign.Center))
            );
        }

    }
}
