using HealthyBusiness.Collision;
using HealthyBusiness.Engine;
using HealthyBusiness.Engine.Managers;
using HealthyBusiness.Engine.Utils;
using HealthyBusiness.Objects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HealthyBusiness.Controllers.PathFinding
{
    public class PathfindingMovementController : MovementController
    {
        private TileLocation? _lastTargetTileLocation;
        private Task _pathFindingDiscoveryTask;
        private CancellationTokenSource _cancellationTokenSource;
        private GameObject[] _walkableGameObjects;
        private Mutex _gameObjectsMutex;

        public Stack<TileLocation> CurrentPath { get; private set; }
        public GameObject? Target { get; set; }


        public PathfindingMovementController(float speed) : base(speed)
        {
            CurrentPath = new Stack<TileLocation>();
            _gameObjectsMutex = new Mutex(false);
            _walkableGameObjects = Array.Empty<GameObject>();
            _cancellationTokenSource = new CancellationTokenSource();
            _pathFindingDiscoveryTask = Task.Run(async () => await PathFindingDiscovery(_cancellationTokenSource.Token));
        }

        // @source: https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtokensource.cancel?view=net-9.0
        public override void Unload()
        {
            _cancellationTokenSource.Cancel();
            if (_pathFindingDiscoveryTask != null && !_pathFindingDiscoveryTask.IsCompleted)
            {
                try
                {
                    _pathFindingDiscoveryTask.Wait();
                }
                catch (AggregateException)
                {
                }
            }
            _cancellationTokenSource.Dispose();

            base.Unload();
        }


        private void SetNextStep()
        {
            if (_targetLocation != null || CurrentPath == null || CurrentPath.Count == 0)
            {
                return;
            }
            TileLocation nextTile = CurrentPath.Pop();
            _targetLocation = nextTile;
        }

        private async Task PathFindingDiscovery(CancellationToken cancellationToken)
        {
            bool first = true;
            var discoveryTimer = new PeriodicTimer(TimeSpan.FromSeconds(2));
            try
            {
                while (!cancellationToken.IsCancellationRequested &&
                       (first || await discoveryTimer.WaitForNextTickAsync(cancellationToken)))
                {
                    if (first)
                        first = false;
                    CalculatePath();
                }
            }
            catch (OperationCanceledException)
            {
            }
            finally
            {
                discoveryTimer.Dispose();
            }
        }

        private void CalculatePath()
        {
            if (Target == null)
                return;

            if (_lastTargetTileLocation != Target.TileLocation || CurrentPath.Count == 0)
            {
                _lastTargetTileLocation = Target.TileLocation;
                var targetTileLocation = new TileLocation(Target.GetGameObject<Collider>()!.Center);
                var gameObjects = GetFloorTiles();

                var path = Pathfinding.PathFinding(Parent!.TileLocation, targetTileLocation, gameObjects).Skip(1).ToList();
                path.Reverse();
                CurrentPath = new(path);
            }
        }

        private GameObject[] GetFloorTiles()
        {
            _gameObjectsMutex.WaitOne();
            try
            {
                if (_walkableGameObjects == null || !_walkableGameObjects.Any())
                    _walkableGameObjects = GameManager.GetGameManager().CurrentScene.GameObjects
                        .Where(go => go is Floor).ToArray();

                return _walkableGameObjects.ToArray();
            }
            finally
            {
                _gameObjectsMutex.ReleaseMutex();
            }
        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            SetNextStep();
        }
    }
}
