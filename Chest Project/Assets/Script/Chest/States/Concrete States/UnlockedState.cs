using ChestProject.Utility;

namespace ChestProject.Chest
{
    public class UnlockedState<T> : IState where T : ChestController
    {
        private GenericStateMachine<T> stateMachine;

        public UnlockedState(GenericStateMachine<T> stateMachine) => this.stateMachine = stateMachine;

        public override void OnStateEnter()
        {
            Owner.GetChestView().OnChestUnlocked();
            Owner.ChestService.ContinueNextChestInQueue();
        }
    }
}

