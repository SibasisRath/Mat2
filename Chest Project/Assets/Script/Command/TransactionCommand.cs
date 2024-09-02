using ChestProject.Chest;
using ChestProject.Currency;
using ChestProject.UI;
using UnityEngine;

namespace ChestProject.Command
{
    public class TransactionCommand : ChestCommand
    {
        private CurrencyService currencyService;
        private UIService uiService;

        private ChestController chestController;
        public TransactionCommand(ChestController chestController, int gemCount)
        {
            this.chestController = chestController;
            this.gemCount = gemCount;
        }
        public void Init(CurrencyService currencyService, UIService uIService)
        {
            this.uiService = uIService;
            this.currencyService = currencyService;
        }
        public override void Execute()
        {
            this.currencyService.Gems -= gemCount;
        }

        public override void Undo()
        {
            this.currencyService.Gems += gemCount;
            chestController.SetLockedState();
        }
    }
}

