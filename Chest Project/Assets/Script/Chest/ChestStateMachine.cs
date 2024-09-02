using ChestProject.Utility;

namespace ChestProject.Chest
{
    public class ChestStateMachine : GenericStateMachine<ChestController>
    {
        // Constructor
        public ChestStateMachine(ChestController Owner) : base(Owner)
        {
            this.Owner = Owner;
            CreateStates();
            SetOwner();
        }

        //  Protected Methods 
        protected override void CreateStates()
        {
            statesDict.Add(ChestStates.LOCKED, new LockedState<ChestController>(this));
            statesDict.Add(ChestStates.QUEUED, new QueueState<ChestController>(this));
            statesDict.Add(ChestStates.UNLOCKING, new UnlockingState<ChestController>(this));
            statesDict.Add(ChestStates.UNLOCKED, new UnlockedState<ChestController>(this));
        }
    }
}

