using ChestProject.Chest;
using ChestProject.Event;
using UnityEngine;
using UnityEngine.UI;

namespace ChestProject.UI
{
    public class UIService : MonoBehaviour
    {
        private EventService eventService;

        [SerializeField] private PopUpMessageUIView popUpMessageUIView;
        [SerializeField] private ConfirmationUIView confirmationMessageUIView;

        [Header ("CHEST SLOT")]
        [SerializeField] private ChestView emptySlotPrefab;
        [SerializeField] private int totalNumberOfSlots;
        [SerializeField] private Transform slotLocation;
        [SerializeField] private Button generateButton;
        [SerializeField] private Button undoButton;

        public ChestView EmptySlotview { get => emptySlotPrefab; set => emptySlotPrefab = value; }
        public int TotalNumberOfSlots { get => totalNumberOfSlots; set => totalNumberOfSlots = value; }
        public Transform SlotLocation { get => slotLocation; set => slotLocation = value; }
        public PopUpMessageUIView PopUpMessageUIView { get => popUpMessageUIView; }
        public ConfirmationUIView ConfirmationMessageUIView { get => confirmationMessageUIView; }

        public void DisplayPopUpMessage(PopUpMessageTypes popUpMessage)
        {
            PopUpMessageUIView.gameObject.SetActive(true);
            PopUpMessageUIView.DisplayMessage(popUpMessage);
        }

        public void DisplayConfirmMessage(ConfirmationMessageType confirmationMessageType)
        {
            confirmationMessageUIView.gameObject.SetActive(true);
            confirmationMessageUIView.DisplayMessage(confirmationMessageType);
        }

        public void init(EventService eventService)
        {
            this.eventService = eventService;
        }

        // Start is called before the first frame update
        void Start()
        {
            confirmationMessageUIView.Init(eventService);
            generateButton.onClick.AddListener(OnGenerateButtonClicked); 
            undoButton.onClick.AddListener(OnUndoButtonClicked);
        }

        private void OnGenerateButtonClicked()
        {
            eventService.OnGenerateButtonCLicked.InvokeEvent();
        }
        private void OnUndoButtonClicked()
        {
            eventService.OnUndoButtonClicked.InvokeEvent();
        }
    }
}

