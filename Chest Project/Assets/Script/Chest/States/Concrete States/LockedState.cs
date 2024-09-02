using ChestProject.Utility;

namespace ChestProject.Chest
{
    public class LockedState<T> : IState where T : ChestController
    {
        private GenericStateMachine<T> stateMachine;
        public LockedState(GenericStateMachine<T> stateMachine) => this.stateMachine = stateMachine;
        public override void OnStateEnter()
        {
            Owner.GetChestView().OnChestLocked();
        }
    }
}

