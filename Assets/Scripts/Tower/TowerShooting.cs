using System.Collections;
using UnityEngine;

namespace Tower
{
    public class TowerShooting : MonoBehaviour
    {
        private const float Range = 1.5f;
        
        private GameObject _cannon;
        public GameObject cannonBall;

        private bool _enemyDetected;
        public float cooldown;
        private const float MaxCooldown = 2f;

        private void Start()
        {
            _cannon = transform.GetChild(0).gameObject;
            StartCoroutine(nameof(Closest));
        }

        private IEnumerator Closest()
        {
            var detectedEnemies = Physics2D.OverlapCircleAll(transform.position, Range);

            Transform tMin = null;
            float minDist = Mathf.Infinity;

            foreach (Collider2D enemy in detectedEnemies)
            {
                if (!enemy.CompareTag("Enemy")) continue;

                float dist = Vector2.Distance(enemy.transform.position, _cannon.transform.position);
                if (dist < minDist)
                {
                    tMin = enemy.transform;
                    minDist = dist;
                }
            }

            if (tMin == null)
            {
                _enemyDetected = false;
                yield return new WaitForSeconds(0.2f);
                StartCoroutine(nameof(Closest));
                yield break;
            }
            
            _enemyDetected = true;
            
            var dir = tMin.position - _cannon.transform.position;
            var angle = Mathf.Atan2(-dir.x, dir.y) * Mathf.Rad2Deg;
            _cannon.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            ShootCannon();

            yield return new WaitForSeconds(0.5f);

            StartCoroutine(nameof(Closest));
        }

        private void ShootCannon()
        {
            if (!_enemyDetected) return;
            var firedBullet = Instantiate(cannonBall, _cannon.transform.position, _cannon.transform.rotation);
            firedBullet.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, 500), ForceMode2D.Force);
            cooldown = MaxCooldown;
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 1.5f);
        }
    }
}
