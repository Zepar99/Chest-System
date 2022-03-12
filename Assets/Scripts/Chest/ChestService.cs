using ChestSO;
using Messages;
using System.Collections.Generic;
using UnityEngine;

namespace Chests
{
    public class ChestService : SingletonGeneric<ChestService>
    {
        private ChestController[] chestSlots;
        private ChestView chestToUnlock;
        private int chestSlotFull;

        public GameObject chestSlotGroup;
        public int noOfChestCanUnlock;
        [HideInInspector] public bool isChestTimerStarted;

        [SerializeField] private int noOfSlots;
        [SerializeField] private ChestView chestView;
        [SerializeField] private ChestScriptableObjectsList chestScriptableObjectList;
        [SerializeField] private List<ChestView> chestUnlockingList;
        [SerializeField] private Sprite[] chestLockedSprites;
        [SerializeField] private Sprite[] chestUnlockedSprites;

        private void Start()
        {
            chestSlots = new ChestController[noOfSlots];
            for (int i = 0; i < noOfSlots; i++)
            {
                chestSlots[i] = createChestController(chestScriptableObjectList.chestSOL[chestScriptableObjectList.chestSOL.Length - 1], chestView, chestLockedSprites[chestLockedSprites.Length - 1]);
            }
        }

        private ChestController createChestController(ChestScriptableObject chestSO, ChestView view, Sprite _sprite)
        {
            ChestModel model = new ChestModel(chestSO);
            ChestController controller = new ChestController(model, view, _sprite);
            return controller;
        }

        public void setChestView(ChestView _view)
        {
            chestToUnlock = _view;
        }

        public void unlockChest()
        {
            chestUnlockingList.Add(chestToUnlock);
            chestToUnlock.chestController.addedToUnlockingList = true;
            chestToUnlock.chestController.startTime();
        }

        public void unlockUsingGems()
        {
            chestToUnlock.chestController.unlockUsingGems();
        }

        public void createAndAddChest()
        {
            int random = Random.Range(0, chestScriptableObjectList.chestSOL.Length);
            chestSlotFull = 0;
            for (int i = 0; i < chestSlots.Length; i++)
            {
                if (chestSlots[i].isChestEmpty())
                {
                    chestSlots[i].addChest(chestScriptableObjectList.chestSOL[random], chestLockedSprites[random], chestUnlockedSprites[random]);
                    i = chestSlots.Length + 1;
                }
                else
                {
                    chestSlotFull++;
                }
            }
            if (chestSlotFull == chestSlots.Length)
            {
                string msg = "All Chest Slots are Full!";
                MessageManager.Instance.displayMessage(msg);
            }
        }

        public void unlockNextChest(ChestView view)
        {
            chestUnlockingList.Remove(view);
            if (chestUnlockingList.Count > 0)
            {
                chestUnlockingList[0].chestController.startTime();
            }
        }

        public void addChestToUnlockList()
        {
            string msg;
            if (isChestTimerStarted && noOfChestCanUnlock == chestUnlockingList.Count)
            {
                msg = "Can't Unlock More Chest!";
                MessageManager.Instance.displayMessage(msg);
            }
            else
            {
                msg = "Chest Added To the List!";
                MessageManager.Instance.displayMessage(msg);
                chestUnlockingList.Add(chestToUnlock);
                chestToUnlock.chestController.addedToUnlockingList = true;
            }
        }
    }
}
