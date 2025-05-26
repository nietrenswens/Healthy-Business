using HealthyBusiness.Collision;
using HealthyBusiness.Controllers;
using HealthyBusiness.Controllers.PathFinding;
using HealthyBusiness.Engine;
using HealthyBusiness.Engine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Linq;

namespace HealthyBusiness.Objects.Creatures.Enemies.Tomato
{
    public class TomatoStateMachine : GameObject
    {
        public TomatoEnemyState State { get; private set; }

        public TomatoStateMachine()
        {
        }

        public override void Load(ContentManager content)
        {
            base.Load(content);
            SetIdle();
        }

        public override void Update(GameTime gameTime)
        {
            var player = GameManager.GetGameManager().CurrentLevel.GameObjects.OfType<Player.Player>().First();
            var linecollider = new LinePieceCollider(Parent!.WorldPosition, player.GetGameObject<Collider>()!.Center);
            switch (State)
            {
                case TomatoEnemyState.Idle:
                    if (Parent is Creature creature)
                    {
                        creature.Animation?.Pause();
                    }
                    if (linecollider.Length <= TomatoEnemy.AggroRange * Globals.TILESIZE)
                    {
                        SetAttack(player);
                    }
                    break;
                case TomatoEnemyState.Attack:
                    if (Parent is Creature creature2)
                    {
                        creature2.Animation?.Resume();
                    }
                    if (linecollider.Length <= TomatoEnemy.ExplosionRange * Globals.TILESIZE)
                    {
                        GameManager.GetGameManager().CurrentLevel.RemoveGameObject(Parent!);
                        return;
                    }
                    if (linecollider.Length > TomatoEnemy.AggroRange * Globals.TILESIZE)
                    {
                        SetIdle();
                    }
                    break;
            }
        }

        public void SetIdle()
        {
            State = TomatoEnemyState.Idle;
            var pathFindingController = Parent!.GetGameObject<PathfindingMovementController>();
            if (pathFindingController != null)
            {
                Parent.Remove(pathFindingController!);
            }
            Parent.Add(new IdleMovementController(TomatoEnemy.SPEED, 2f));
        }

        public void SetAttack(GameObject target)
        {
            if (Parent!.GetGameObject<PathfindingMovementController>() != null)
            {
                return;
            }
            State = TomatoEnemyState.Attack;
            var pathFindingController = new PathfindingMovementController(TomatoEnemy.SPEED);
            pathFindingController.Target = target;
            var idleController = Parent.GetGameObject<IdleMovementController>();
            Parent.Remove(idleController!);
            Parent.Add(pathFindingController);
        }
    }
}
