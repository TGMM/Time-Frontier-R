using Enemy;
using UnityEngine;

namespace Player
{
    public class PlayerMelee : MonoBehaviour
    {
        private const float Range = 0.3f;
        private const int DamageDone = 5;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                DamageNearby();
            }
        }

        private void DamageNearby()
        {
            var detectedEnemies = Physics2D.OverlapCircleAll(transform.position, Range);

            Transform closestEnemy = null;
            float minDist = Mathf.Infinity;

            foreach (Collider2D enemy in detectedEnemies)
            {
                if (!enemy.CompareTag("Enemy")) continue;

                var dist = Vector2.Distance(transform.position, enemy.transform.position);
                if (dist < minDist)
                {
                    closestEnemy = enemy.transform;
                    minDist = dist;
                }
            }

            if (closestEnemy != null)
            {
                closestEnemy.GetComponent<EnemyHealth>().TakeDamage(DamageDone);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, Range);
        }
    }
}
