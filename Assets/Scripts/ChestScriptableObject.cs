using UnityEngine;

namespace ChestSO
{
    [CreateAssetMenu(fileName = "ChestScriptableObject", menuName = "Chest/ChestScriptableObjects/New Chest")]
    public class ChestScriptableObject : ScriptableObject
    {
        public ChestTypes chestType;
        public int unlockTime;
        public int minCoins;
        public int maxCoins;
        public int minGems;
        public int maxGems;
    }
}
