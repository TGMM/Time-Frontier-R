using TMPro;
using UnityEngine;

namespace Player
{
    public class PlayerValues : MonoBehaviour
    {
        public int maxHp;
        public int maxCoins;

        public int Hp { get; set; }
        public int Coins { get; set; }

        private const int StartingCoins = 0;
        private const int StartingHp = 300;

        private TextMeshProUGUI _hpText;
        private TextMeshProUGUI _coinsText;

        private void Awake()
        {
            Coins = StartingCoins;
            Hp = StartingHp;
        }

        private void Start()
        {
            _hpText = GameObject.Find("HealthPoints").GetComponent<TextMeshProUGUI>();
            _coinsText = GameObject.Find("Money").GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            _hpText.text = Hp.ToString();
            _coinsText.text = Coins.ToString();
        }

        public int GetCoins()
        {
            return Coins;
        }

        public int GetHp()
        {
            return Hp;
        }

        public void ChangeCoins(int coinsToAdd)
        {
            Coins += coinsToAdd;
        }
        public void ChangeHp(int hpToAdd)
        {
            Hp += hpToAdd;
        }
    }
}
