using HealthyBusiness.Collision;
using HealthyBusiness.Controllers.PathFinding;
using HealthyBusiness.Engine;
using HealthyBusiness.Engine.Managers;
using HealthyBusiness.InGameGUIObjects;
using HealthyBusiness.Objects.Creatures.PlayerCreature;
using HealthyBusiness.Scenes;
using Microsoft.Xna.Framework;
using System.Linq;

namespace HealthyBusiness.Objects.Creatures.Enemies.Potato
{
    public class PotatoEnemyStateMachine : GameObject
    {
        private float _damageTimer = 0f;
        private const float DAMAGE_COOLDOWN = 1000f; // Cooldown time in miliseconds
        public PotatoEnemyState State { get; private set; }

        public PotatoEnemyStateMachine()
        {
            State = PotatoEnemyState.Idle;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            PotatoEnemy? parent = (PotatoEnemy)Parent!;
            var player = GameManager.GetGameManager().CurrentScene.GameObjects.OfType<Player>().FirstOrDefault();
            if (player == null || parent is not PotatoEnemy)
                return;

            var linePieceCollider = new LinePieceCollider(Parent!.WorldPosition, player.WorldPosition);

            if (_damageTimer > 0f)
            {
                _damageTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            switch (State)
            {
                case PotatoEnemyState.Idle:
                    bool isPlayerInRange = linePieceCollider.Length <= PotatoEnemy.AGGRO_RANGE * Globals.TILESIZE;
                    Hotbar? hotbar = ((GameScene)GameManager.GetGameManager().CurrentScene).GUIObjects.Attributes
                        .OfType<Hotbar>().FirstOrDefault();
                    bool playerHasFries = hotbar != null && hotbar.HotbarSlots.Any(slot => slot.Item?.Name.ToLower().Contains("fries") ?? false);

                    if (isPlayerInRange && playerHasFries)
                    {
                        SetAttack(player);
                    }
                    break;

                case PotatoEnemyState.Attack:
                    if (linePieceCollider.Length > PotatoEnemy.AGGRO_RANGE * Globals.TILESIZE)
                    {
                        SetIdle();
                        return;
                    }

                    if (parent.GetGameObject<Collider>()!.CheckIntersection(player.GetGameObject<Collider>()!))
                    {
                        if (_damageTimer <= 0f)
                        {
                            player.TakeDamage(PotatoEnemy.DAMAGE);
                            _damageTimer = DAMAGE_COOLDOWN;
                        }
                    }

                    break;
            }
        }

        private void SetIdle()
        {
            ((Creature)Parent).SetTexture(PotatoEnemy.POTATO_NORMAL_TEXTURE_PATH);
            State = PotatoEnemyState.Idle;
            var pathFindingController = Parent!.GetGameObject<PathfindingMovementController>();
            if (pathFindingController != null)
            {
                Parent.Remove(pathFindingController!);
            }
        }

        private void SetAttack(GameObject target)
        {
            if (Parent!.GetGameObject<PathfindingMovementController>() != null)
            {
                return;
            }
            ((Creature)Parent).SetTexture(PotatoEnemy.POTATO_ATTACK_TEXTURE_PATH);
            State = PotatoEnemyState.Attack;
            var pathFindingController = new PathfindingMovementController(PotatoEnemy.SPEED);
            pathFindingController.Target = target;
            Parent.Add(pathFindingController);
        }
    }

    public enum PotatoEnemyState
    {
        Idle, Attack
    }
}
