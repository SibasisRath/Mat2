using ChestProject.Event;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ChestProject.UI
{
    public class ConfirmationUIView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI comfirmMessage;
        [SerializeField] private string undoText;
        private string skipTimmerConfirmText;
        [SerializeField] private Button skipTimerButton;
        [SerializeField] private Button startTimerButton;
        [SerializeField] private Button cancelButton;
        private EventService eventService;
        // Start is called before the first frame update
        void Start()
        {
            startTimerButton.onClick.AddListener(OnTimerButtonClicked);
            skipTimerButton.onClick.AddListener(OnSkipTimerButtonClicked);
            cancelButton.onClick.AddListener(OnCancelButtonClicked);
        }
        public void Init(EventService eventService)
        {
            this.eventService = eventService;
        }

        private void OnSkipTimerButtonClicked()
        {
            eventService.OnSkipUnlockingButtonClicked.InvokeEvent();
            this.gameObject.SetActive(false);
        }
        private void OnTimerButtonClicked() 
        {
            eventService.OnStartUnlockingButtonClicked.InvokeEvent();
            this.gameObject.SetActive(false);
        }

        private void OnCancelButtonClicked()
        {
            eventService.OnConfirmationMessageCanceled.InvokeEvent();
            this.gameObject.SetActive(false);
        }

        public void DisplayMessage(ConfirmationMessageType confirmationMessageType)
        {
            switch (confirmationMessageType)
            {
                case ConfirmationMessageType.SKIP_TIMER_CONFIRMATION_LOCKED:
                    comfirmMessage.text = skipTimmerConfirmText;
                    skipTimerButton.gameObject.SetActive(true);
                    startTimerButton.gameObject.SetActive(true);
                    cancelButton.gameObject.SetActive(true);
                    break;
                case ConfirmationMessageType.SKIP_TIMER_CONFIRMATION_UNLOCKING:
                    comfirmMessage.text = skipTimmerConfirmText;
                    skipTimerButton.gameObject.SetActive(true);
                    startTimerButton.gameObject.SetActive(false);
                    cancelButton.gameObject.SetActive(true);
                    break;
                case ConfirmationMessageType.UNDO_CONFIRMATION:
                    comfirmMessage.text = undoText;
                    break;
                default:
                    Debug.Log("wrong message.");
                    break;
            }
        }

        public void ConstructMessage(int requiredGems)
        {
           skipTimmerConfirmText = "Do you want to skip the unlocking time spending gems: " + requiredGems;
        }
    }
}

