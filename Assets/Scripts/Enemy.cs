using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Include Artificial Intelligence part of API
using UnityEngine.AI;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public enum State
    {
        Patrol,
        Seek
    }
    // Property
    public int Health
    {
        get
        {
            return health;
        }
    }
    public State currentState = State.Patrol;
    public NavMeshAgent agent;
    public Transform target;
    public Transform waypointParent;
    public float distanceToWaypoint = 1f;
    public float detectionRadius = 5f;
    public FieldOfView fov;
    public AudioSource alertSound;
    public GameObject alertSymbol;

    private int health = 100;
    private int currentIndex = 1;
    private Transform[] waypoints;

    IEnumerator SeekDelay()
    {
        yield return new WaitForSeconds(1f);
        // Switch to seek state
        currentState = State.Patrol;
        // Reset the target
        target = null;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
    void Start()
    {
        waypoints = waypointParent.GetComponentsInChildren<Transform>();
    }
    void Patrol()
    {
        // If the currentIndex is out of waypoint range
        if (currentIndex >= waypoints.Length)
        {
            // Go back to "first" (actually second) waypoint
            currentIndex = 1;
        }
        // Set the current waypoint
        Transform point = waypoints[currentIndex];
        // Get distance to waypoint
        float distance = Vector3.Distance(transform.position, point.position);
        // If waypoint is within range
        if (distance <= distanceToWaypoint)
        {
            // Move to next waypoint (Next Frame)
            currentIndex++;
        }
        // Generate path to current waypoint
        agent.SetDestination(point.position);
        //Generate path to current waypoint
        if (fov.visibleTargets.Count > 0)
        {
            // Set the target to the first one
            target = fov.visibleTargets[0];
            // Switch to "Seek" state
            currentState = State.Seek;
            // Detected!
            alertSound.Play();  // Audiosource
            alertSymbol.SetActive(true);    // gameObject
        }
    }
    void Seek()
    {
        // Update the AI's target position
        agent.SetDestination(target.position);
        // Get the distance to the target
        float distToTarget = Vector3.Distance(transform.position, target.position);
        // If the target is within detection range
        if (distToTarget > detectionRadius)
        {
            StartCoroutine(SeekDelay());
        }
    }
    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case State.Patrol:
                Patrol();
                break;
            case State.Seek:
                Seek();
                break;
            default:
                break;
        }
    }
    //void FixedUpdate()
    //{
    //    Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius);
    //    foreach (var hit in hits)
    //    {
    //        Player player = hit.GetComponent<Player>();
    //        if (player)
    //        {
    //            target = player.transform;
    //            return;
    //        }
    //    }
    //
    //    target = null;
    //}
    public void DealDamage(int damageDealt)
    {
        health -= damageDealt;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
