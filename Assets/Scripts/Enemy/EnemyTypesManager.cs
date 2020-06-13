using System;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    [Serializable]
    public class EnemyTypesManager : MonoBehaviour
    {
        public List<EnemyType> enemyTypes = new List<EnemyType>();
        [SerializeField] private GameObject goblin;
        [SerializeField] private GameObject orc;

        private void Start()
        {
            enemyTypes.Add(new EnemyType(goblin, 0, 1));
            enemyTypes.Add(new EnemyType(orc, 1, 3));
        }
    }

    public class EnemyType
    {

        public EnemyType(GameObject ep, int i, int w)
        {
            EnemyPrefab = ep;
            Index = i;
            Weight = w;
            Name = EnemyPrefab.name;
        }

        public readonly GameObject EnemyPrefab;
        public string Name;
        public int Index;
        public int Weight;
    }
}