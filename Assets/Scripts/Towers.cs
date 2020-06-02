using UnityEngine;

public class Towers : MonoBehaviour {

	int level = 1;
	float range;
	public float cooldown;
	float maxCooldown = 2f;
	public enum TurretType { Cannon };
	public enum TurretMode { MostHealth, First, Closest }; //First means it's the one that will reach the end first if uninterrupted.
	public TurretMode mode;
	public TurretType type;

	bool _enemydetected;
	GameObject _cannon;
	public GameObject cannonBall;

	private void Awake()
	{
		cooldown = 0;
		_cannon = transform.GetChild(1).gameObject;
	}

	private void Update ()
	{
		DetectTower();
		ShootCannon();
	}

	private void DetectTower()
	{
		switch (type)
		{
			case TurretType.Cannon:
				Cannon();
				return;
			default:
				return;
		}
	}

	void Cannon()
	{
		int layerMaskZero = 1 << 0;
		range = 1.5f;
		Collider2D[] detectedEnemies = Physics2D.OverlapCircleAll(transform.position, range, layerMaskZero);
		Transform tMin = null;
		float minDist = Mathf.Infinity;
		int farthestWaypoint = 0;

		if (detectedEnemies.Length > 0)
		{
			switch (mode)
			{
				case TurretMode.Closest:
					{
						foreach (Collider2D enemy in detectedEnemies)
						{
							float dist = Vector2.Distance(enemy.transform.position, _cannon.transform.position);
							if (dist < minDist)
							{
								tMin = enemy.transform;
								minDist = dist;
							}
						}
						var dir = tMin.position - _cannon.transform.position;
						var angle = Mathf.Atan2(-dir.x, dir.y) * Mathf.Rad2Deg;
						_cannon.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
						break;
					}
				case TurretMode.First:
					{
						foreach (Collider2D enemy in detectedEnemies)
						{
							if (enemy.GetComponent<EnemyMovement>().waypointIndex > farthestWaypoint)
							{
								tMin = enemy.transform;
								farthestWaypoint = enemy.GetComponent<EnemyMovement>().waypointIndex;
								minDist = Vector2.Distance(enemy.transform.position, enemy.GetComponent<EnemyMovement>().target.position);
							}
							else if (enemy.GetComponent<EnemyMovement>().waypointIndex == farthestWaypoint)
							{
								float dist = Vector2.Distance(enemy.transform.position, enemy.GetComponent<EnemyMovement>().target.position);
								if (dist < minDist)
								{
									tMin = enemy.transform;
									minDist = dist;
								}
							}                    
						}
						var dir = tMin.position - _cannon.transform.position;
						var angle = Mathf.Atan2(-dir.x, dir.y) * Mathf.Rad2Deg;
						_cannon.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
						break;
					}
			}
			_enemydetected = true;
		}
		else
		{
			_enemydetected = false;
		}
	}

	private void ShootCannon()
	{
		if (_enemydetected == true && cooldown <= 0)
		{
			GameObject firedBullet;
			firedBullet = Instantiate(cannonBall, _cannon.transform.position, _cannon.transform.rotation);
			firedBullet.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, 500), ForceMode2D.Force);
			cooldown = maxCooldown;
		}

		if (cooldown != 0 && cooldown > 0)
		{
			cooldown -= Time.deltaTime;
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, 1.5f);
	}
}
