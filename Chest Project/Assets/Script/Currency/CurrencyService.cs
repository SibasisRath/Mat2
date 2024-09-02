using TMPro;
using UnityEngine;

namespace ChestProject.Currency
{
    public class CurrencyService : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI goldCoinsText;
        [SerializeField] private TextMeshProUGUI gemsText;
        private int goldCoins = 100; // remove serialize field tags
        private int gems = 0;

        public int Gems
        {
            get => gems;
            set
            {
                if (value < 0)
                {
                    gems = 0;
                }
                else
                {
                    gems = value;
                }
                UpdateGemText();
            }
        }
        public int GoldCoins 
        {
            get => goldCoins;
            set
            {
                if (value < 0)
                {
                    goldCoins = 0;
                }
                else
                {
                    goldCoins = value;
                }
                UpdateGoldCoinText();
            }
        }
        public bool IsValidTransaction(int chestGem) 
        {
            return (chestGem < Gems); 
        }

        public void CollectFromChest(int goldCoins, int gems)
        {
            GoldCoins += goldCoins;
            Gems += gems;
        }

        private void UpdateGoldCoinText()
        {
            goldCoinsText.text = " : " + goldCoins.ToString();
        }

        private void UpdateGemText()
        {
            gemsText.text = " : " + gems.ToString();
        }

        private void Awake()
        {
            UpdateGemText();
            UpdateGoldCoinText();
        }
       
    }
}

