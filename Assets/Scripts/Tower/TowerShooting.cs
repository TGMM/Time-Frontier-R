using UnityEngine;

namespace Tower
{
    public class TowerShooting : MonoBehaviour
    {
        private const float Range = 1.5f;
        private GameObject _cannon;

        private void Start()
        {
            _cannon = transform.GetChild(0).gameObject;
        }

        private void Update()
        {
            Closest();
        }

        private void Closest()
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

            if (tMin == null) return;
            var dir = tMin.position - _cannon.transform.position;
            var angle = Mathf.Atan2(-dir.x, dir.y) * Mathf.Rad2Deg;
            _cannon.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 1.5f);
        }
    }
}
