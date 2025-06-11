using HealthyBusiness.Engine;
using HealthyBusiness.Engine.Managers;
using HealthyBusiness.Engine.Utils;
using HealthyBusiness.Objects.Creatures.PlayerCreature;
using HealthyBusiness.Scenes;
using System;

namespace HealthyBusiness.Objects.Doors
{
    public class ExitDoor : Door
    {
        private Action _exitAction;

        public ExitDoor(TileLocation tileLocation, DoorDirection doorDirection, Action exitAction) : base(tileLocation, doorDirection)
        {
            _exitAction = exitAction;
        }

        public override void OnCollision(GameObject other)
        {
            if (other is not Player || GameManager.GetGameManager().CurrentScene is not GameScene)
                return;
            _exitAction.Invoke();
        }
    }
}
