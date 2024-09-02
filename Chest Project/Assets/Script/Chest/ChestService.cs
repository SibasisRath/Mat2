using ChestProject.Command;
using ChestProject.Currency;
using ChestProject.Event;
using ChestProject.UI;
using System.Collections.Generic;
using UnityEngine;

namespace ChestProject.Chest
{
    public class ChestService
    {
        private ChestView chestView;
        private ChestSO chestSo;
        private Transform chestSlotTransform;
        private int chestSlotsSize;

        private List<ChestController> slots;
        private List<ChestSO> chestSOs;
        private Queue<ChestController> controllerQueue;

        private EventService eventService;
        private UIService uiService;
        private CurrencyService currencyService;
        private CommandInvoker commandInvoker;

        public ChestService(UIService uIService, List<ChestSO> chestSOs) 
        {
            this.uiService = uIService;
            this.chestView = uIService.EmptySlotview;
            this.chestSlotTransform = uIService.SlotLocation;
            this.chestSlotsSize = uiService.TotalNumberOfSlots;
            this.chestSOs = chestSOs;
            slots = new();
            controllerQueue = new Queue<ChestController>();
        }

        public void Init(EventService eventService, CurrencyService currencyService, CommandInvoker commandInvoker)
        {
            this.eventService = eventService;
            this.currencyService = currencyService;
            this.commandInvoker = commandInvoker;
            SubscribeToEvents();
        }

        private void SubscribeToEvents() 
        {
            eventService.OnGenerateButtonCLicked.AddListener(GenerateChest);
            eventService.OnUndoButtonClicked.AddListener(UndoingLastTransaction);
        }
        private void UnSubscribeToEvents() 
        {
            eventService.OnGenerateButtonCLicked.RemoveListener(GenerateChest);
            eventService.OnUndoButtonClicked.RemoveListener(UndoingLastTransaction);
        }

        public void AddToChestQueue(ChestController chestController)
        {
            controllerQueue.Enqueue(chestController);
        }

        public void ContinueNextChestInQueue() 
        {
            if (controllerQueue.Count != 0)
            {
                controllerQueue.Dequeue().SetUnlockingState();
            }
        }

        public void GenerateEmptySlots()
        {
            for (int i = 0; i < chestSlotsSize; i++)
            {
                GameObject emptySlot = Object.Instantiate(chestView.gameObject);
                emptySlot.transform.SetParent(chestSlotTransform);
                ChestController chestController = new (emptySlot.GetComponent<ChestView>());
                chestController.Init(eventService, uiService, this, currencyService, commandInvoker);
                slots.Add(chestController);
            }
        }

        public void GenerateChest()
        {
            foreach (ChestController chestController in slots)
            {
                if (chestController.IsSlotEmpty == true)
                {
                    chestController.SetChestModel(GetRandomChest());
                    return;
                }
            }
            uiService.DisplayPopUpMessage(PopUpMessageTypes.SLOTSFULL);
        }

        public ChestSO GetRandomChest()
        {
            // Define the probabilities for each rarity chestTypeText
           // float commonProbability = 0.5f;     // 50%
            float rareProbability = 0.3f;       // 30%
            float epicProbability = 0.15f;      // 15%
            float legendaryProbability = 0.05f; // 5%
            float randomValue = UnityEngine.Random.Range(0f, 1f);

            ChestTypes type;

            if (randomValue <= legendaryProbability)
            {
                type = ChestTypes.LEGENDARY;
            }
            else if (randomValue <= legendaryProbability + epicProbability)
            {
                type = ChestTypes.EPIC;
            }
            else if (randomValue <= legendaryProbability + epicProbability + rareProbability)
            {
                type = ChestTypes.RARE;
            }
            else
            {
                type = ChestTypes.COMMON;
            }
            return chestSOs.Find(item => item.ChestTypes == type);

        }

        //Toggling selected chestSlot
        public void DefiningTheSelectedChest(ChestController chest)
        {
            foreach (ChestController chestController in slots)
            {
                if (chestController == chest)
                {
                    chestController.IsSelected = true;
                }
                else
                {
                    chestController.IsSelected = false;
                }
            }
        }

        public bool IsAnyChestUnlocking()
        {
            foreach(ChestController chestController in slots)
            {
                if (chestController.StateMachine.ChestStateEnum == ChestStates.UNLOCKING)
                {
                    return true;
                }
            }
            return false;
        }

        public void UndoingLastTransaction()
        {
            commandInvoker.UndoCommand();
        }

        ~ChestService() 
        {
            UnSubscribeToEvents();
        }
    }
}

