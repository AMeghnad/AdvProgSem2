using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public State currentState = State.Patrol;
    public NavMeshAgent agent;
    public Transform target;
    public Transform waypointParent;
    public float distanceToWaypoint;
    public float detectionRadius = 5f;
    public bool loop;

    private Transform[] waypoints;
    private bool pingPong;
    private int currentIndex = 1;
    private int health = 100;

    public int Health
    {
        get
        {
            return health;
        }
    }

    // Implement this OnDrawGizmosSelected if you want to draw gizmos only if the object is selected
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    void Start()
    {
        waypoints = waypointParent.GetComponentsInChildren<Transform>();
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

    void FixedUpdate()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (var hit in hits)
        {
            Player player = hit.GetComponent<Player>();
            if (player)
            {
                target = player.transform;
                return;
            }
        }

        target = null;
    }


    public void DealDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public enum State
    {
        Patrol,
        Seek
    }

    void Patrol()
    {
        if (currentIndex >= waypoints.Length)
        {
            // Reset back to "first" waypoint
            currentIndex = 1;
        }
        Transform point = waypoints[currentIndex];

        // Set distance from gameobject to waypoint
        float distance = Vector3.Distance(transform.position, point.position);
        if (distance <= distanceToWaypoint)
        {
            currentIndex++;
        }
        agent.SetDestination(point.position);
    }


    // This callback will be invoked at each frame after the state machines and the animations have been evaluated, but before OnAnimatorIK
    private void OnAnimatorMove()
    {

    }

    void Seek()
    {
        // Update the AI's target position
        agent.SetDestination(target.position);
    }
}
