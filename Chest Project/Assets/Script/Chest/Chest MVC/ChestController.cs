using ChestProject.Command;
using ChestProject.Currency;
using ChestProject.Event;
using ChestProject.UI;
using System;

namespace ChestProject.Chest
{
    public class ChestController 
    {
        private ChestView chestView;
        private ChestModel chestModel;
        private bool isSlotEmpty;
        private ChestStateMachine stateMachine;
        private EventService eventService;
        private UIService uiService;
        private ChestService chestService;
        private CurrencyService currencyService;
        private bool isSelected;
        private CommandInvoker commandInvoker;

        public bool IsSlotEmpty { get => isSlotEmpty; set => isSlotEmpty = value; }
        public ChestStateMachine StateMachine { get => stateMachine; }
        public bool IsSelected { get => isSelected; set => isSelected = value; }
        public ChestService ChestService { get => chestService; }
        public EventService EventService { get => eventService;}

        public ChestController(ChestView chestView) 
        {
            this.chestView = chestView;
            chestModel = new();
            chestView.SetController(this);
            IsSlotEmpty = true;
            CreateStateMachine();
            isSelected = false;
        }
        public void Init(EventService eventService, UIService uIService, ChestService chestService, CurrencyService currencyService, CommandInvoker commandInvoker) 
        {
            this.eventService = eventService;
            this.uiService = uIService;
            this.chestService = chestService;
            this.currencyService = currencyService;
            this.commandInvoker = commandInvoker;
            chestView.Init(uiService);
            SubscribeToEvents();
        }
        private void SubscribeToEvents()
        {
            eventService.OnSkipUnlockingButtonClicked.AddListener(OpenChestByGems);
            eventService.OnStartUnlockingButtonClicked.AddListener(CheckForQueuedState);
            eventService.OnStartUnlockingButtonClicked.AddListener(CheckForUnlockingState);
            eventService.OnConfirmationMessageCanceled.AddListener(chestView.ResumeTimer);
        }
        private void UnsubscribeToEvents()
        {
            eventService.OnSkipUnlockingButtonClicked.RemoveListener(OpenChestByGems);
            eventService.OnStartUnlockingButtonClicked.RemoveListener(CheckForQueuedState);
            eventService.OnStartUnlockingButtonClicked.RemoveListener(CheckForUnlockingState);
            eventService.OnConfirmationMessageCanceled.RemoveListener(chestView.ResumeTimer);
        }

        public void SetLockedState() 
        {
            stateMachine.ChangeState(ChestStates.LOCKED);
        }
        private void CheckForQueuedState() 
        {
            if (isSelected && chestService.IsAnyChestUnlocking())
            {
                SetQueuedState();
            }
        }
        public void SetQueuedState()
        {
            stateMachine.ChangeState(ChestStates.QUEUED);
        }
        private void CheckForUnlockingState() 
        {
            if (isSelected && !chestService.IsAnyChestUnlocking())
            {
                SetUnlockingState();
            }
        }
        public void SetUnlockingState()
        {
            stateMachine.ChangeState(ChestStates.UNLOCKING);
        }
        public void SetUnlockedState() 
        {
            stateMachine.ChangeState(ChestStates.UNLOCKED);
        }
        public void UpdateAllDeselctedController() 
        {
            chestService.DefiningTheSelectedChest(this);
        }

        private void CreateStateMachine()
        {
            stateMachine = new ChestStateMachine(this);
        }
        public ChestView GetChestView() => chestView;
        public ChestModel GetChestModel()
        {
            return this.chestModel;
        }
        public void SetChestModel(ChestSO chestSO)
        {
            chestModel.AssigningValues(chestSO);
            SetLockedState();
            IsSlotEmpty = false;
            uiService.PopUpMessageUIView.ConstruckChestInfoMessage(chestModel);
            uiService.DisplayPopUpMessage(PopUpMessageTypes.CHESTINFO);
        }

        public void SendingToPlayerCurrency()
        {
            currencyService.CollectFromChest
                (chestModel.GoldCoinsWillBeCollected,
                chestModel.GemsWillBeCollected);
        }

        public void ResetingSlot()
        {
            uiService.DisplayPopUpMessage(PopUpMessageTypes.CHESTINFO);
            chestView.ResetChestInfo();
            isSlotEmpty = true;
            isSelected = false;
        }
        public void UpdateRequiredGemsValue() 
        {
            chestModel.GemsNeededToUnlock -= 1;
            chestView.UpdateRequiredGemsValues();
        }

        private ChestCommand GetCommand(CommandTypes commandType)
        {
            switch (commandType)
            {
                case CommandTypes.Transaction:
                    TransactionCommand transactionCommand = new (this, chestModel.GemsNeededToUnlock);
                    transactionCommand.Init(currencyService, uiService);
                    return transactionCommand;

                default:
                    throw new Exception($"Command not found of : {commandType}");
            }
        }

        public void OpenChestByGems()
        {
            if (isSelected)
            {
                if (currencyService.IsValidTransaction(chestModel.GemsNeededToUnlock))
                {
                    commandInvoker.ProcessCommand(GetCommand(CommandTypes.Transaction));
                    SetUnlockedState();
                }
                else
                {
                    uiService.DisplayPopUpMessage(PopUpMessageTypes.NOT_ENOUGH_GEMS);
                }
            }
        }

        ~ChestController() {
            UnsubscribeToEvents();
        }
    }
}

