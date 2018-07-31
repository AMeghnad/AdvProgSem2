using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private int health = 100;

    public int Health
    {
        get
        {
            return health;
        }
    }
    public NavMeshAgent agent;
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
    }

    public void DealDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
