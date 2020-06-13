using Player;
using UnityEngine;

namespace Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private int health = 10;
        private bool _destinedToDie = false;

        public void Update()
        {
            if (health <= 0)
            {
                Die();
            }
        }

        public int GetHealth()
        {
            return health;
        }

        public void SetHealth(int h)
        {
            health = h;
        }

        public void TakeDamage(int d)
        {
            if (d <= 0) return;

            health -= d;
        }

        private void Die()
        {
            Destroy(GetComponent<SpriteRenderer>());
            Destroy(GetComponent<Collider2D>());
            if (!_destinedToDie)
            {
                _destinedToDie = true;
                FindObjectOfType<PlayerValues>().ChangeCoins(Random.Range(2,10));
            }
            Destroy(gameObject, 1.2f);
        }
    }
}
