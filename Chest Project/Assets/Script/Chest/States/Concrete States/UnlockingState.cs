using ChestProject.Utility;

namespace ChestProject.Chest
{
    public class UnlockingState<T> : IState where T : ChestController
    {
        private GenericStateMachine<T> stateMachine;
        public UnlockingState(GenericStateMachine<T> stateMachine) => this.stateMachine = stateMachine;

        public override void OnStateEnter()
        {
            Owner.GetChestView().OnChestUnlocking();
        }
    }
}

