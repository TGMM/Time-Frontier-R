using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Enemy
{
    public class WaveSpawner : MonoBehaviour
    {
        public int currentWave;
        public Vector3 spawnPosition = new Vector3(-5.5f, -3.5f, 0);
        private EnemyTypesManager _enemyTypesManager;
        private TextMeshProUGUI _waveCounter;

        private void Start()
        {
            _enemyTypesManager = FindObjectOfType<EnemyTypesManager>();
            
            var canvas = FindObjectOfType<Canvas>();
            var textObject = canvas.transform.GetChild(4);
            _waveCounter = textObject.GetComponent<TextMeshProUGUI>();
        }

        public void StopSpawns()
        {
            StopAllCoroutines();
        }

        public void SpawnNextWave()
        {
            currentWave++;
            _waveCounter.text = $"Wave: {currentWave}";

            var waveWeight = currentWave * 5;
            var enemiesToSpawn = new List<GameObject>();

            while (waveWeight > 0)
            {
                var currentWeight = waveWeight;
                
                var allowedEnemies = _enemyTypesManager.enemyTypes.Where(enemy => enemy.Weight <= currentWeight).ToList();
                var newEnemy = allowedEnemies[Random.Range(0, allowedEnemies.Count)];

                enemiesToSpawn.Add(newEnemy.EnemyPrefab);
                waveWeight -= newEnemy.Weight;
            }

            StartCoroutine(SpawnEnemies(enemiesToSpawn, 0.75f));
        }

        private IEnumerator SpawnEnemies(List<GameObject> enemiesToSpawn, float secondsToWait)
        {
            foreach (var enemy in enemiesToSpawn)
            {
                var newEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity);
                switch (enemy.name)
                {
                    case "Enemy_Goblin":
                        newEnemy.GetComponent<EnemyHealth>().SetHealth(10);
                        break;
                    case "Enemy_Orc":
                        newEnemy.GetComponent<EnemyHealth>().SetHealth(30);
                        break;
                }
                yield return new WaitForSeconds(secondsToWait);
            }
        }
    }
}
