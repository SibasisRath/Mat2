using ChestProject.Chest;

namespace ChestProject.Event
{
    public class EventService
    {
        public EventController OnGenerateButtonCLicked { get; private set; }
        public EventController OnStartUnlockingButtonClicked { get; private set; }
        public EventController OnConfirmationMessageCanceled { get; private set; }
        public EventController OnSkipUnlockingButtonClicked { get; private set; }
        public EventController OnUndoButtonClicked { get; private set; }
        public EventService()
        {
            OnGenerateButtonCLicked = new ();
            OnStartUnlockingButtonClicked = new ();
            OnConfirmationMessageCanceled = new ();
            OnSkipUnlockingButtonClicked = new ();
            OnUndoButtonClicked = new ();
        }
    }
}

