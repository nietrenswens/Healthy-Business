using HealthyBusiness.Collision;
using HealthyBusiness.Engine;
using HealthyBusiness.Engine.Utils;
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

        public Stack<TileLocation> CurrentPath { get; private set; }
        public GameObject? Target { get; set; }


        public PathfindingMovementController(float speed) : base(speed)
        {
            CurrentPath = new Stack<TileLocation>();
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
            var discoveryTimer = new PeriodicTimer(TimeSpan.FromSeconds(2));
            try
            {
                while (!cancellationToken.IsCancellationRequested &&
                       await discoveryTimer.WaitForNextTickAsync(cancellationToken))
                {
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
                var path = Pathfinding.PathFinding(Parent!.TileLocation, targetTileLocation).Skip(1).ToList();
                path.Reverse();
                CurrentPath = new(path);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            SetNextStep();
        }
    }
}
