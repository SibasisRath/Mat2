using ChestProject.Utility;

namespace ChestProject.Chest
{
    public class QueueState<T> : IState where T : ChestController
    {
        private GenericStateMachine<T> stateMachine;
        public QueueState(GenericStateMachine<T> stateMachine) => this.stateMachine = stateMachine;
        public override void OnStateEnter()
        {
            Owner.ChestService.AddToChestQueue(Owner);
            Owner.GetChestView().OnChestQueued();
        }
    }
}
