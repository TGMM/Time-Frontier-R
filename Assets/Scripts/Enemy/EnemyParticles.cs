using UnityEngine;

namespace Enemy
{
    public class EnemyParticles : MonoBehaviour
    {
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
                _particleSystem.Simulate(0,true,true);
                _particleSystem.Play();
                _health.TakeDamage(10);
            }
        }
    }
}
