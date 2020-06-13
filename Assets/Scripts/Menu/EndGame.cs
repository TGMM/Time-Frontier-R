using Enemy;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class EndGame : MonoBehaviour
    {
        private GameObject _gameMaster;
        private TextMeshProUGUI _waveReached;
        private int _wave;

        private void Start()
        {
            _gameMaster = GameObject.FindGameObjectWithTag("GameMaster");
            _waveReached = GameObject.Find("Wave").GetComponent<TextMeshProUGUI>();

            WaveSpawner waveSpawner = null;
            
            if (_gameMaster != null)
                waveSpawner = _gameMaster.GetComponent<WaveSpawner>();

            _wave = waveSpawner != null ? waveSpawner.currentWave : 0;

            _waveReached.text = $"You reached wave:\n {_wave}";
        }

        public void BackToMainMenu()
        {
            Destroy(_gameMaster);
            SceneManager.LoadScene(0);
        }
    }
}
