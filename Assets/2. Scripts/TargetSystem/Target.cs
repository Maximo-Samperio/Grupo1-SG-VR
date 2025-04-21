using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Target : MonoBehaviour
{
    [Header("Hit Settings")]
    [Tooltip("Tag used by your bullet/projectile.")]
    public string bulletTag = "Bullet";

    [Header("Movement Settings")]
    [Tooltip("Enable to make the target patrol between waypoints.")]
    public bool isMoving = false;

    [Tooltip("Waypoints for the target to patrol. Requires Is Moving = true.")]
    public Transform[] waypoints;

    [Tooltip("Movement speed (units/sec) when patrolling.")]
    public float moveSpeed = 2f;

    // internals
    private int currentWaypoint = 0;

    void Start()
    {
        // sanity check
        if (isMoving && (waypoints == null || waypoints.Length == 0))
        {
            Debug.LogWarning($"[{name}] Is Moving is on but no waypoints assigned. Disabling movement.");
            isMoving = false;
        }
    }

    void Update()
    {
        if (isMoving)
            Patrol();
    }

    private void Patrol()
    {
        Transform targetPoint = waypoints[currentWaypoint];
        if (targetPoint == null) return;

        // Move towards the current waypoint
        transform.position = Vector3.MoveTowards(transform.position,
                                                 targetPoint.position,
                                                 moveSpeed * Time.deltaTime);

        // If close enough, advance to the next waypoint (looping)
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.05f)
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
    }

    private void OnTriggerEnter(Collider other)
    {
        // When hit by a bullet, destroy this target
        if (other.CompareTag(bulletTag))
        {
            Destroy(gameObject);
            // — If you also want the bullet to disappear:
            // Destroy(other.gameObject);
        }
    }
}
