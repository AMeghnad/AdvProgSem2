using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float range;
    public float duration;
    public int damage;

    protected virtual void Start()
    {
        range = 5f;
        damage = 50;
        duration = 1f;
    }

    // This function is called when the object becomes enabled and active
    protected virtual void OnEnable()
    {
        StartCoroutine(Delay());
    }

    protected IEnumerator Delay()
    {
        yield return new WaitForSeconds(duration);
        // Disables the game object after duration
        gameObject.SetActive(false);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy)
        {
            // enemy.Health();
            enemy.DealDamage(damage);
            gameObject.SetActive(false);
        }
    }
}