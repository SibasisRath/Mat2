using ChestProject.Chest;
using System.Collections;
using TMPro;
using UnityEngine;

namespace ChestProject.UI
{
    public class PopUpMessageUIView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI popUpMessage;

        [SerializeField] private string slotsAreFullMessage;
        [SerializeField] private string notEnoughMoneyMessage;
        private string chestInfoMessage;

        private ChestService chestService;

        public void DisplayMessage(PopUpMessageTypes popUpMessageTypes)
        {
            switch (popUpMessageTypes)
            {
                case PopUpMessageTypes.SLOTSFULL:
                    popUpMessage.text = slotsAreFullMessage;
                    break;
                case PopUpMessageTypes.CHESTINFO:
                    popUpMessage.text = chestInfoMessage;
                    break;
                case PopUpMessageTypes.NOT_ENOUGH_GEMS:
                    popUpMessage.text = notEnoughMoneyMessage;
                    break;
                default:
                    Debug.Log("wrong message.");
                    break;
            }
            StartCoroutine(MessageLifeSpan());
            
        }

        public void ConstruckChestInfoMessage(ChestModel chestModel) 
        {
            chestInfoMessage = "You got a " + chestModel.ChestTypes.ToString() + " type treasure.\nYou will get " + chestModel.GemsWillBeCollected + " amount of gems\nand " + chestModel.GoldCoinsWillBeCollected + " amount of gold coins.";
        }

        private IEnumerator MessageLifeSpan()
        {
            yield return new WaitForSeconds(2);
            this.gameObject.SetActive(false);
        }

    }

}
