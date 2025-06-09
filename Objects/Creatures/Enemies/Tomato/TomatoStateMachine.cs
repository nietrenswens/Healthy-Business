using HealthyBusiness.Animations;
using HealthyBusiness.Builders;
using HealthyBusiness.Collision;
using HealthyBusiness.Controllers;
using HealthyBusiness.Controllers.PathFinding;
using HealthyBusiness.Engine;
using HealthyBusiness.Engine.Managers;
using HealthyBusiness.Engine.Utils;
using HealthyBusiness.Objects.Creatures.PlayerCreature;
using HealthyBusiness.Scenes;
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
            var player = GameManager.GetGameManager().CurrentScene.GameObjects.OfType<Player>().First();
            var playerCollider = player.GetGameObject<Collider>();
            if (playerCollider == null)
            {
                return; // Player collider not found, exit early
            }
            var linecollider = new LinePieceCollider(Parent!.WorldPosition, playerCollider.Center);
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
                        SetExploding();
                    }
                    if (linecollider.Length > TomatoEnemy.AggroRange * Globals.TILESIZE)
                    {
                        SetIdle();
                    }
                    break;
                case TomatoEnemyState.Exploding:
                    if (Parent is Creature creature3 && creature3.Animation?.IsFinished == true)
                    {
                        player.TakeDamage(TomatoEnemy.Damage);

                        player.CheckHealth();

                        var tileLocation = new TileLocation(creature3.WorldPosition);

                        var ketchup = ItemBuilder.CreateKetchup(tileLocation);
                        GameManager.GetGameManager().CurrentScene.AddGameObject(ketchup);

                        GameManager.GetGameManager().CurrentScene.RemoveGameObject(Parent!);
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

        public void SetExploding()
        {
            State = TomatoEnemyState.Exploding;

            if (Parent is Creature creature)
            {
                creature.Animation = new ExplosionAnimation("entities\\enemies\\tomato\\TomatoBoom");
                creature.Animation.Resume();
            }

            var idle = Parent.GetGameObject<IdleMovementController>();
            if (idle != null)
            {
                Parent.Remove(idle);
            }

            var path = Parent.GetGameObject<PathfindingMovementController>();
            if (path != null)
            {
                Parent.Remove(path);
            }
        }


    }
}
