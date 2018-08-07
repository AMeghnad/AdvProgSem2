using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform target;
    public Transform waypointParent;
    public float distanceToWaypoint;
    public bool loop = false;

    private bool pingPong = false;
    private Transform[] waypoints;
    private int currentIndex = 1;
    private int health = 100;


    public int Health
    {
        get
        {
            return health;
        }
    }

    void Start()
    {
        waypoints = waypointParent.GetComponentsInChildren<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        // If a target is set
        if (target)
        {
            // Update the AI's target position
            agent.SetDestination(target.position);
        }
        else
        {
            if (currentIndex >= waypoints.Length)
            {
                if (loop)
                {
                    // Reset back to "first" waypoint
                    currentIndex = 1;
                }
                else
                {
                    // Cap the index if it's greater
                    currentIndex = waypoints.Length - 1;
                    // Reverse it!
                    pingPong = true;
                }
            }
            if (currentIndex <= 0)
            {
                if (loop)
                {
                    // Reset back to "first" waypoint
                    currentIndex = waypoints.Length - 1;
                }
                else
                {
                    // Cap the index if it's greater
                    currentIndex = 1;
                    // Reverse it!
                    pingPong = false;
                }
            }

            Transform point = waypoints[currentIndex];

            // set distance from gameobject to waypoint
            float distance = Vector3.Distance(transform.position, point.position);
            // if within vicinity of waypoint
            if (distance <= distanceToWaypoint)
            {
                if (pingPong)
                {
                    // Move to previous waypoint
                    currentIndex--;
                }
                else
                {
                    // Move to next waypoint
                    currentIndex++;
                }
            }

            agent.SetDestination(point.position);
        }
    }

    public void DealDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
