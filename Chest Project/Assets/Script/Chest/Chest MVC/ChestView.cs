using ChestProject.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ChestProject.Chest
{
    public class ChestView : MonoBehaviour
    {
        [SerializeField] private GameObject emptySlot;
        [SerializeField] private GameObject chestSlot;

        [SerializeField] private TextMeshProUGUI chestTypeText;
        [SerializeField] private Image chestImage;
        [SerializeField] private TextMeshProUGUI stateText;
        [SerializeField] private TextMeshProUGUI requiredGemText;
        [SerializeField] private TimerScript timer;

        private UIService uiService;
        private ChestController controller;

        public void SetController(ChestController chestController) => this.controller = chestController;
        public void Init(UIService uIService) => this.uiService = uIService;
        private void Start() => SetEmptySlot();
        private void OnChestButtonClicked()
        {
            controller.IsSelected = true;
            controller.UpdateAllDeselctedController();

            switch (controller.StateMachine.ChestStateEnum)
            {
                case ChestStates.LOCKED:
                    ShowConfirmationMessage(ConfirmationMessageType.SKIP_TIMER_CONFIRMATION_LOCKED);
                    break;
                case ChestStates.UNLOCKING:
                    PauseTimerAndShowConfirmation();
                    break;
                case ChestStates.UNLOCKED:
                    HandleChestUnlocked();
                    break;
                default:
                    break;
            }            
        }

        private void ShowConfirmationMessage(ConfirmationMessageType messageType)
        {
            int gemsNeeded = controller.GetChestModel().GemsNeededToUnlock;
            uiService.ConfirmationMessageUIView.ConstructMessage(gemsNeeded);
            uiService.DisplayConfirmMessage(messageType);
        }
        private void PauseTimerAndShowConfirmation()
        {
            timer.PauseTimer();
            ShowConfirmationMessage(ConfirmationMessageType.SKIP_TIMER_CONFIRMATION_UNLOCKING);
        }
        private void HandleChestUnlocked()
        {
            controller.SendingToPlayerCurrency();
            controller.ResetingSlot();
        }
        public void ResumeTimer()
        {
            timer.ResumeTimer();
        }
        public void UpdateRequiredGemsValues()
        {
            requiredGemText.text = "gems:\n" +controller.GetChestModel().GemsNeededToUnlock.ToString();
        }
        public void ResetChestInfo()
        {
            chestTypeText.text = string.Empty;
            chestImage.sprite = null;
            stateText.text = string.Empty;
            requiredGemText.text = string.Empty;
            SetEmptySlot();
        }

        public void SetEmptySlot()
        {
            emptySlot.SetActive(true);
            chestSlot.SetActive(false);
        }

        public void SetChestSlot()
        {
            emptySlot.SetActive(false);
            chestSlot.SetActive(true);
        }

        public void DisplayChestState()
        {
            stateText.text = controller.StateMachine.ChestStateEnum.ToString();
        }

        public void DisplayChestInfo()
        {
            SetChestSlot();
            ChestModel chestModel = controller.GetChestModel();
            chestTypeText.text = chestModel.ChestTypes.ToString();
            chestImage.sprite = chestModel.ClosedChestSprite;
            DisplayChestState();
        }

        public void OnChestLocked() 
        {
            DisplayChestInfo();
            chestSlot.GetComponent<Button>().onClick.AddListener(OnChestButtonClicked);
        }

        public void OnChestQueued() 
        {
            chestImage.sprite = controller.GetChestModel().ClosedChestSprite;
            DisplayChestState();
        }

        public void OnChestUnlocking()
        {
            timer.gameObject.SetActive(true);
            timer.ChestController = controller;
            timer.StartTimer(controller.GetChestModel().TimerSecs);
            UpdateRequiredGemsValues();
            chestImage.sprite = controller.GetChestModel().ClosedChestSprite;
            DisplayChestState();
        }

        public void OnChestUnlocked()
        {
            requiredGemText.text = string.Empty;
            timer.gameObject.SetActive(false);
            chestImage.sprite = controller.GetChestModel().OpenChestSprite;
            DisplayChestState();
        }
    }
}

