using HealthyBusiness.Engine;
using HealthyBusiness.Engine.GUI;
using HealthyBusiness.Engine.Managers;
using HealthyBusiness.Engine.Utils;
using HealthyBusiness.InGameGUIObjects;
using HealthyBusiness.Scenes;
using Microsoft.Xna.Framework;
using System;

namespace HealthyBusiness.Data
{
    public class Quota
    {
        private GameData _gameData;

        public int EmployerLevel { get; set; }
        public int Deadline { get; set; }
        public int Amount { get; set; }
        public int LastAchievedQuotaDeadline { get; set; }

        public Quota(int employerLevel, GameData gameData)
        {
            _gameData = gameData;
            EmployerLevel = employerLevel;
            Deadline = gameData.ShiftCount + 3;
            LastAchievedQuotaDeadline = Deadline;
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
            LastAchievedQuotaDeadline = Deadline;
            EmployerLevel++;
            _gameData.Balance = _gameData.Balance - _gameData.Quota.Amount;
            Amount = DetermineQuota();
            PrintQuotaStatus();

            _gameData.Quota.Deadline = _gameData.ShiftCount + 3;
        }

        private void PrintQuotaStatus()
        {
            string message = $"You have reached a new quota!\nYour new quota: {Amount}";
            Scene scene = GameManager.GetGameManager().CurrentScene;

            if (scene is not GameScene gameScene)
            {
                return;
            }

            gameScene.GUIObjects.Add(
                new TimedText("fonts\\pixelated_elegance\\small", message, Color.White, 4000, backgroundColor: Color.DarkSlateGray, guiStyling: new GUIStyling(verticalFloat: VerticalAlign.Center, horizontalFloat: HorizontalAlign.Center))
            );
        }

    }
}
