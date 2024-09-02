namespace ChestProject.Command
{
    public abstract class ChestCommand : ICommand
    {
        public int gemCount;
        public abstract void Execute();
        public abstract void Undo();
    }
}

