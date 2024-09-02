namespace ChestProject.Chest
{
    public abstract class IState
    {
        public ChestController Owner;
        public virtual void OnStateEnter() { }

        public virtual void Update() { }

        public virtual void OnStateExit() { }
    }
}

