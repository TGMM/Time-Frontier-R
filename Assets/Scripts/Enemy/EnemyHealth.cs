using Player;
using UnityEngine;

namespace Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private int health = 10;
        private bool _destinedToDie = false;
        private ParticleSystem _particleSystem;
        private EnemyHealth _health;

        private void Start()
        {
            _particleSystem = GetComponent<ParticleSystem>();
            _health = GetComponent<EnemyHealth>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Projectile"))
            {
                _health.TakeDamage(5);
            }
        }

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
            
            _particleSystem.Simulate(0, true, true);
            _particleSystem.Play();
        }

        private void Die()
        {
            Destroy(GetComponent<SpriteRenderer>());
            Destroy(GetComponent<Collider2D>());
            if (!_destinedToDie)
            {
                _destinedToDie = true;
                FindObjectOfType<PlayerValues>().ChangeCoins(Random.Range(1,4));
            }
            Destroy(gameObject, 1.2f);
        }
    }
}
