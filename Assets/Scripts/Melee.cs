using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    public float duration = 1f;
    public int damage = 50;

    // This function is called when the object becomes enabled and active
    void OnEnable()
    {
        // Start timer
        StartCoroutine(Delay());
    }

    // OnTriggerEnter is called when the Collider other enters the trigger
    void OnTriggerEnter(Collider other)
    {
        // detect enemy
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy)
        {
            // deal damage
            enemy.DealDamage(damage);
            // disable the weapon
            gameObject.SetActive(false);
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }

}
