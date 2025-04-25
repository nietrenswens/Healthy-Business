using HealthyBusiness.Engine.Managers;
using HealthyBusiness.Engine.Utils;
using HealthyBusiness.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthyBusiness.Controllers.PathFinding
{
    public static class Pathfinding
    {
        public static Stack<TileLocation> PathFinding(TileLocation originLocation, TileLocation targetLocation)
        {
            PriorityQueue<Step, float> prioritizedSteps = new();
            HashSet<TileLocation> visitedLocations = new();
            var gameObjects = GameManager.GetGameManager().CurrentLevel.GameObjects.Where(go => go is Floor).ToList();

            prioritizedSteps.Enqueue(new Step(0, originLocation), 0);

            while (prioritizedSteps.Count > 0)
            {
                Step currentStep = prioritizedSteps.Dequeue();
                if (currentStep.Location == targetLocation)
                {
                    break;
                }

                for (int i = 0; i < 4; i++)
                {
                    TileLocation nextLocation = new TileLocation(currentStep.Location.X, currentStep.Location.Y);

                    switch (i)
                    {
                        case 0:
                            nextLocation -= new TileLocation(1, 0);
                            break; // left
                        case 1:
                            nextLocation += new TileLocation(1, 0);
                            break; // right
                        case 2:
                            nextLocation -= new TileLocation(0, 1);
                            break; // up
                        case 3:
                            nextLocation += new TileLocation(0, 1); ;
                            break; // down
                    }
                    var nextGameObject = gameObjects.FirstOrDefault(go => go.TileLocation == nextLocation);
                    if (nextGameObject == null)
                        continue;

                    if (nextGameObject.TileLocation == targetLocation)
                    {
                        return GetPath(currentStep, targetLocation);
                    }

                    if (visitedLocations.Contains(nextLocation))
                        continue;

                    // Adding to the queue
                    float cost = currentStep.Cost + Heuristics(nextLocation, targetLocation);
                    if (cost > Globals.MAX_PATHFINDING_COST)
                        return new();
                    Step step = new(cost, nextLocation, currentStep);
                    prioritizedSteps.Enqueue(step, cost);
                }
                visitedLocations.Add(currentStep.Location);
            }
            return new();
        }

        private static Stack<TileLocation> GetPath(Step step, TileLocation targetLocation)
        {
            Stack<TileLocation> path = new();
            path.Push(targetLocation);
            var currentStep = step;
            while (currentStep != null)
            {
                path.Push(currentStep.Location);
                currentStep = currentStep.Previous;
            }
            return path;
        }

        private static float Heuristics(TileLocation a, TileLocation b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        }
    }
}
