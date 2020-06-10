using UnityEngine;

namespace Player
{
    public class PlayerValues : MonoBehaviour
    {
        public int maxHp;
        public int maxCoins;

        public int Hp { get; set; }
        public int Coins { get; set; }

        private const int StartingCoins = 250;
        private const int StartingHp = 500;

        private void Awake()
        {
            Coins = StartingCoins;
            Hp = StartingHp;
        }

        public int GetCoins()
        {
            return Coins;
        }

        public int GetHp()
        {
            return Hp;
        }

        public void AddCoins(int coinsToAdd)
        {
            if (coinsToAdd <= 0) return;
            Coins += coinsToAdd;
        }

        public void AddHp(int hpToAdd)
        {
            if (hpToAdd <= 0) return;
            Hp += hpToAdd;
        }

        public void RemoveCoins(int coinsToAdd)
        {
            if (coinsToAdd >= 0) return;
            Coins += coinsToAdd;
        }

        public void RemoveHp(int hpToAdd)
        {
            if (hpToAdd >= 0) return;
            Hp += hpToAdd;
        }
    }
}
