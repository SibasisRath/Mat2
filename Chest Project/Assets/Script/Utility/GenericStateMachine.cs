using ChestProject.Chest;
using System.Collections.Generic;

namespace ChestProject.Utility
{
    public class GenericStateMachine<T> where T : ChestController
    {
        // Protected Variables 
        protected T Owner;
        private IState currentState;
        private ChestStates chestStateEnum;
        protected Dictionary<ChestStates, IState> statesDict = new();

        public ChestStates ChestStateEnum { get => chestStateEnum; private set => chestStateEnum = value; }

        // Constructor
        public GenericStateMachine(T Owner) => this.Owner = Owner;

        // Protected Methods 
        protected void ChangeState(IState newState)
        {
            currentState?.OnStateExit();
            currentState = newState;
            currentState?.OnStateEnter();
        }

        protected virtual void CreateStates() { }

        protected void SetOwner()
        {
            foreach (IState state in statesDict.Values)
            {
                state.Owner = Owner;
            }
        }

        // Public Methods
        public void ChangeState(ChestStates newState)
        {
            ChestStateEnum = newState;
            ChangeState(statesDict[newState]);
        }
        public void Update() => currentState.Update();
    }
}
