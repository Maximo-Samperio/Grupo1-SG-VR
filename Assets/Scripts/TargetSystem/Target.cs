using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Target : NeedCustomUpdateObject
{
    public enum TargetType { Normal = 1, Upgraded = 2, Golden = 5 }

    [Header("Hit Settings")]
    [Tooltip("Tag used by your bullet/projectile.")]
    public string bulletTag = "Bullet";

    [Header("Score Settings")]
    [Tooltip("Which kind of target is this?")]
    public TargetType targetType = TargetType.Normal;

    [Header("Movement Settings (Patrol)")]
    public bool isMoving = false;
    public Transform[] waypoints;
    public float moveSpeed = 2f;

    [Header("Respawn Settings")]
    [Tooltip("Where this target can re-appear.")]
    public Transform[] respawnPoints;
    [Tooltip("Seconds to wait before respawning.")]
    public float respawnDelay = 5f;
    [Tooltip("Radius to check for existing targets.")]
    public float occupancyRadius = 0.5f;
    [Tooltip("Only objects with this tag count as 'occupied'.")]
    public string occupancyTag = "Target";

    // internals
    int currentWaypoint = 0;
    Coroutine respawnRoutine;

    // cache these so we can hide/show without deactivating the GameObject
    Renderer[] renderers;
    Collider mainCollider;

    void Awake()
    {
        // grab everything once
        renderers = GetComponentsInChildren<Renderer>();
        mainCollider = GetComponent<Collider>();

        if (isMoving && (waypoints == null || waypoints.Length == 0))
        {
            Debug.LogWarning($"[{name}] Patrol ON but no waypoints set. Disabling movement.");
            isMoving = false;
        }
    }
    public override void CustomUpdate()
    {
        if (isMoving)
            Patrol();
    }

    void Patrol()
    {
        var tp = waypoints[currentWaypoint];
        transform.position = Vector3.MoveTowards(
            transform.position,
            tp.position,
            moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, tp.position) < 0.05f)
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(bulletTag)) return;

        other.gameObject.GetComponent<Bullet>().Destroy();

        // score it
        ScoreManager.Instance.AddScore((int)targetType);

        // kick off hide+respawn
        if (respawnRoutine != null)
            StopCoroutine(respawnRoutine);
        respawnRoutine = StartCoroutine(HandleRespawn());
    }

    IEnumerator HandleRespawn()
    {
        // 1) Hide visuals & collider
        foreach (var r in renderers) r.enabled = false;
        mainCollider.enabled = false;

        // 2) Wait
        yield return new WaitForSeconds(respawnDelay);

        // 3) Choose a free respawn point
        Transform chosen = null;
        int count = respawnPoints != null ? respawnPoints.Length : 0;
        int[] idxs = new int[count];
        for (int i = 0; i < count; i++) idxs[i] = i;

        int attempts = 0;
        while (attempts < 5 && chosen == null)
        {
            // shuffle
            for (int i = 0; i < count; i++)
            {
                int j = Random.Range(i, count);
                var tmp = idxs[i]; idxs[i] = idxs[j]; idxs[j] = tmp;
            }

            // test each
            foreach (int i in idxs)
            {
                var pt = respawnPoints[i];
                var hits = Physics.OverlapSphere(pt.position, occupancyRadius);
                bool occupied = false;
                foreach (var c in hits)
                    if (c.CompareTag(occupancyTag))
                    { occupied = true; break; }

                if (!occupied)
                {
                    chosen = pt;
                    break;
                }
            }

            if (chosen == null)
            {
                attempts++;
                yield return new WaitForSeconds(1f);
            }
        }

        // fallback
        if (chosen == null && count > 0)
            chosen = respawnPoints[Random.Range(0, count)];

        // 4) Move, reset patrol index
        if (chosen != null)
            transform.position = chosen.position;
        currentWaypoint = 0;

        // 5) Show again
        foreach (var r in renderers) r.enabled = true;
        mainCollider.enabled = true;
    }

    void OnDrawGizmosSelected()
    {
        if (respawnPoints == null) return;
        Gizmos.color = Color.yellow;
        foreach (var pt in respawnPoints)
            if (pt != null)
                Gizmos.DrawWireSphere(pt.position, occupancyRadius);
    }
}
