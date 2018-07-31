using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public BoxCollider hitbox;
    public int damage = 50;
    // attribute
    [Tooltip("Duration hitbox is enabled(in seconds)")]
    public float hitDuration = 1f; // duration hitbox is enabled

    // Update is called once per frame
    void Update()
    {
        // Check if space is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Run hit sequence
            StartCoroutine(Hit());
        }
    }

    IEnumerator Hit()
    {
        hitbox.enabled = true;
        yield return new WaitForSeconds(hitDuration);
        hitbox.enabled = false;
    }

    // OnTriggerEnter is called when the Collider other enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        // detect enemy
        Enemy enemy = GetComponent<Enemy>();
        if (enemy != null)
        {
            // deal damage
            enemy.DealDamage(damage);
        }
    }
}
