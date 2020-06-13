using Enemy;
using Map;
using Menu;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        private GameObject _gameMaster;

        private void Awake()
        {
            Coins = StartingCoins;
            Hp = StartingHp;
        }

        private void Start()
        {
            _hpText = GameObject.Find("HealthPoints").GetComponent<TextMeshProUGUI>();
            _coinsText = GameObject.Find("Money").GetComponent<TextMeshProUGUI>();
            _gameMaster = GameObject.FindGameObjectWithTag("GameMaster");
        }

        private void Update()
        {
            _hpText.text = Hp.ToString();
            _coinsText.text = Coins.ToString();

            if (Hp <= 0 && SceneManager.GetActiveScene() != SceneManager.GetSceneByBuildIndex(3))
            {
                LoseGame();
            }
        }

        private void LoseGame()
        {
            DontDestroyOnLoad(_gameMaster);
            Destroy(_gameMaster.GetComponent<EnemyTypesManager>());
            Destroy(_gameMaster.GetComponent<TileTypesManager>());
            _gameMaster.GetComponent<WaveSpawner>().StopSpawns();
            _gameMaster.GetComponent<ButtonDelay>().StopAllCoroutines();
            SceneManager.LoadScene(3);
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
