using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Chests;

namespace Messages
{
    public class MessageManager : SingletonGeneric<MessageManager>
    {
        [SerializeField] private GameObject messageScreen;
        [SerializeField] private TextMeshProUGUI messageTxt;
        [SerializeField] private Button timerBtn;
        [SerializeField] private Button gemsBtn;
        [SerializeField] private TextMeshProUGUI timerBtnTxt;
        [SerializeField] private TextMeshProUGUI gemsBtnTxt;

        public void displayMessageWithBtns(string message, int gems, bool isChestInList)
        {
            messageScreen.SetActive(true);
            string msg;
            if (ChestService.Instance.isChestTimerStarted && ChestService.Instance.noOfChestCanUnlock > 1)
            {
                msg = "Add Chest!";
                timerBtnTxt.text = msg;
            }
            else
            {
                msg = "Start Timer!";
                timerBtnTxt.text = msg;
            }

            gemsBtnTxt.text = gems.ToString();
            messageTxt.text = message;

            isChestAdded(isChestInList);
        }

        private void isChestAdded(bool isChestInList)
        {
            if (isChestInList)
            {
                timerBtn.gameObject.SetActive(false);
            }
            else
            {
                timerBtn.gameObject.SetActive(true);
            }
            gemsBtn.gameObject.SetActive(true);
        }

        public void onTimerBtnClicked()
        {
            messageScreen.SetActive(false);
            if (ChestService.Instance.isChestTimerStarted)
            {
                ChestService.Instance.addChestToUnlockList();
            }
            else
            {
                ChestService.Instance.unlockChest();
            }
        }

        public void onGemsBtnClicked()
        {
            messageScreen.SetActive(false);
            ChestService.Instance.unlockUsingGems();
        }

        public void displayMessage(string msg)
        {
            messageScreen.SetActive(true);
            messageTxt.text = msg;
            timerBtn.gameObject.SetActive(false);
            gemsBtn.gameObject.SetActive(false);
            disableMessage();
        }

        private async void disableMessage()
        {
            await new WaitForSeconds(2f);
            messageScreen.gameObject.SetActive(false);
        }
    }
}
