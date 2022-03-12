using UnityEngine;
using TMPro;

namespace PlayerData
{
    public class PlayerData : SingletonGeneric<PlayerData>
    {
        private bool isEnoughGems;

        [SerializeField] private int coins;
        [SerializeField] private int gems;
        [SerializeField] private string Name;
        [SerializeField] private TextMeshProUGUI coinTxt;
        [SerializeField] private TextMeshProUGUI gemsTxt;
        [SerializeField] private TextMeshProUGUI PlayerName;

        private void Start()
        {
            showPlayerData();
        }

        private void showPlayerData()
        {
            coinTxt.text = coins.ToString();
            gemsTxt.text = gems.ToString();
            PlayerName.text = Name.ToString();
        }

        public void addCoinsAndGems(int _coins, int _gems)
        {
            coins += _coins;
            gems += _gems;
            showPlayerData();
        }

        public bool removeGems(int _gems)
        {
            if (_gems <= gems)
            {
                gems -= _gems;
                isEnoughGems = true;
            }
            else
            {
                isEnoughGems = false;
            }
            showPlayerData();
            return isEnoughGems;
        }
    }
}
