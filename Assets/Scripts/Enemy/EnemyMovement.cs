using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    //Speed at which the enemy moves, set to public so it can be edited in inspector.
    public float speed = 1f;
    //Target is the current waypoint at which is headed, changes after it arrives there. Ex. Waypoint 1 is target
    //enemy reaches waypoint 1, now waypoint 2 is target
    public Transform target;
    //Same as above but this is just the index, not the actual transform position (x, y coordinates)
    public int waypointIndex = 1;

    void Start ()
    {
        //Set target to the first waypoint
        target = Waypoints.waypoints[1];
	}
	
	void Update ()
    {
        //Set direction
        Vector2 dir = target.position - transform.position;
        //Move to target using direction above
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        //Rotate
        var dirRot = target.position - transform.position;
        var angle = Mathf.Atan2(-dir.x, dir.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (Vector2.Distance(transform.position,target.position) <= 0.02f)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        if ( waypointIndex >= Waypoints.waypoints.Length - 1)
        {
            Destroy(gameObject);
            return;
        }

        waypointIndex++;
        target = Waypoints.waypoints[waypointIndex];
    }
}
