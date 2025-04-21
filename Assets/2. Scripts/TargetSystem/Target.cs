using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider))]
public class Target : MonoBehaviour
{
    public enum TargetType
    {
        Normal = 1,   // +1 point
        Upgraded = 2,   // +2 points
        Golden = 5    // +5 points
    }

    [Header("Hit Settings")]
    [Tooltip("Tag used by your bullet/projectile.")]
    public string bulletTag = "Bullet";

    [Header("Score Settings")]
    [Tooltip("Which kind of target is this?")]
    public TargetType targetType = TargetType.Normal;

    [Header("Movement Settings")]
    public bool isMoving = false;
    public Transform[] waypoints;
    public float moveSpeed = 2f;

    private int currentWaypoint = 0;

    void Start()
    {
        // Disable movement if no waypoints provided
        if (isMoving && (waypoints == null || waypoints.Length == 0))
        {
            Debug.LogWarning($"[{name}] No waypoints set — disabling movement.");
            isMoving = false;
        }
    }

    void Update()
    {
        if (isMoving) Patrol();
    }

    private void Patrol()
    {
        var targetPoint = waypoints[currentWaypoint];
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPoint.position,
            moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPoint.position) < 0.05f)
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(bulletTag)) return;

        // Award points based on the enum value
        int points = (int)targetType;
        ScoreManager.Instance.AddScore(points);

        // Destroy this target (and optionally the bullet)
        Destroy(gameObject);
        // Destroy(other.gameObject);
    }
}
