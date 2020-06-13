using UnityEngine;

namespace Tower
{
    public class Projectile : MonoBehaviour 
    {
        private void Start()
        {
            Destroy(gameObject, 2);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                Destroy(gameObject);
            }
        }
    }
}
