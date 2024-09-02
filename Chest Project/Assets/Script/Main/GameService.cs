using ChestProject.Chest;
using ChestProject.Command;
using ChestProject.Currency;
using ChestProject.Event;
using ChestProject.UI;
using System.Collections.Generic;
using UnityEngine;

namespace ChestProject.Main
{
    public class GameService : MonoBehaviour
    {
        private EventService eventService;
        private ChestService chestService;
        private CommandInvoker commandInvoker;

        [SerializeField] private UIService uIService;
        [SerializeField] private CurrencyService currencyService;
        [SerializeField] private List<ChestSO> chestSOs;
        [SerializeField] private AudioSource sfxSource;

        private void Start()
        {
            InitializeServices();
            InjectDependencies();
        }
        private void InitializeServices()
        {
            eventService = new();
            chestService = new(uIService, chestSOs);
            commandInvoker = new();
        }
        private void InjectDependencies()
        {
            uIService.init(eventService);
            chestService.Init(eventService, currencyService, commandInvoker);
            chestService.GenerateEmptySlots();
        }
    }
}

