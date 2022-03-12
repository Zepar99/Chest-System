using UnityEngine;

namespace ChestSO
{
    [CreateAssetMenu(fileName = "ChestScriptableObjectList", menuName = "Chest/ChestScriptableObjects/Chest List")]
    public class ChestScriptableObjectsList : ScriptableObject
    {
        public ChestScriptableObject[] chestSOL;
    }
}
