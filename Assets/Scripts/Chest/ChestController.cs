using ChestSO;
using UnityEngine;
using Player;
using Messages;
using System;

namespace Chests
{
    [Serializable]
    public class ChestController
    {
        public ChestModel chestModel { get; }
        public ChestView chestView { get; }

        public ChestTypes chestType;
        public string chestStatus;
        public int coins;
        public int gems;
        public int unlockTime;
        public int unlockWithGems;
        public bool addedToUnlockingList;
        public bool isStartTime;

        public bool isEmpty;
        public bool isLocked;
        private Sprite sprite;

        public ChestController(ChestModel model, ChestView view, Sprite _sprite)
        {
            chestModel = model;
            chestView = GameObject.Instantiate<ChestView>(view);
            sprite = _sprite;
            chestView.setChestController(this);
        }

        public void addChest(ChestScriptableObject chestSO, Sprite _lockedSprite, Sprite _unlockedSprite)
        {
            chestType = chestSO.chestType;
            coins = UnityEngine.Random.Range(chestSO.minCoins, chestSO.maxCoins + 1);
            gems = UnityEngine.Random.Range(chestSO.minGems, chestSO.maxGems + 1);
            unlockTime = chestSO.unlockTime;
            chestView.lockedChestSprite = _lockedSprite;
            chestView.unlockedChestSprite = _unlockedSprite;
            chestStatus = "Locked";
            isEmpty = false;
            isLocked = true;
            unlockWithGems = countGemsToUnlock(unlockTime);
            chestView.showChestData();
        }

        public int countGemsToUnlock(int unlockTime)
        {
            int gems = (int)Mathf.Ceil(unlockTime / 10);
            return gems;
        }

        public void instantiateEmptyChest()
        {
            coins = 0;
            gems = 0;
            unlockWithGems = 0;
            chestType = ChestTypes.None;
            chestStatus = "Empty";
            isEmpty = true;
            chestView.lockedChestSprite = sprite;
            addedToUnlockingList = false;
            chestView.showChestData();
        }

        public bool isChestEmpty()
        {
            return isEmpty;
        }

        public void unlockUsingGems()
        {
            bool canUnlock = PlayerData.Instance.removeGems(unlockWithGems);
            if (canUnlock)
            {
                chestUnlocked();
            }
            else
            {
                string msg = "Can't unlock chest!";
                MessageManager.Instance.displayMessage(msg);
            }
        }

        public void chestBtnClicked()
        {
            string msg;
            if (isEmpty)
            {
                msg = "Chest Slot is Empty!";
                MessageManager.Instance.displayMessage(msg);
                return;
            }
            if (isLocked)
            {
                msg = "Unlock This Chest!";
                ChestService.Instance.setChestView(chestView);
                MessageManager.Instance.displayMessageWithBtns(msg, unlockWithGems, addedToUnlockingList);
            }
            else
            {
                PlayerData.Instance.addCoinsAndGems(coins, gems);
                msg = coins + " Coins and " + gems + " Gems Added!";
                MessageManager.Instance.displayMessage(msg);
                instantiateEmptyChest();
            }
        }

        private void chestUnlocked()
        {
            chestStatus = "Unlocked";
            isLocked = false;
            unlockWithGems = 0;
            isStartTime = false;
            ChestService.Instance.isChestTimerStarted = false;
            unlockTime = 0;
            chestView.showChestData();
            ChestService.Instance.unlockNextChest(chestView);
        }

        public async void startTime()
        {
            isStartTime = true;
            ChestService.Instance.isChestTimerStarted = true;
            while (unlockTime > 0)
            {
                await new WaitForSeconds(1f);
                unlockTime -= 1;
            }
        }

        public void startUnlocking()
        {
            unlockWithGems = countGemsToUnlock(unlockTime);
            chestView.showUnlockGems(unlockWithGems);
            chestView.showUnlockTime(unlockTime);
            if (unlockTime <= 0)
            {
                chestUnlocked();
            }
        }

        public async void UnlockingWithGem()
        {
            isLocked = true;
            await new WaitForSeconds(5f);

        }
    }
}
