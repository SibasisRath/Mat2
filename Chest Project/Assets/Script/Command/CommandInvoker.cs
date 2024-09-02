using System.Collections.Generic;

namespace ChestProject.Command
{
    public class CommandInvoker
    {
        private Stack<ICommand> commandRegistry = new();

        public void ProcessCommand(ICommand commandToProcess)
        {
            ExecuteCommand(commandToProcess);
            RegisterCommand(commandToProcess);
        }

        public void UndoCommand()
        {
            if (commandRegistry.Count != 0)
            {
                commandRegistry.Pop().Undo();
            }
        }

        private void ExecuteCommand(ICommand command) => command.Execute();

        private void RegisterCommand(ICommand command) => commandRegistry.Push(command);
    }
}

