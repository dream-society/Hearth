using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptionHazard : HazardBase
{
    private BoxCollider2D boxCollider;
    private float delta = 0.1f;
    private float counter = 0f;
    public float TimeToDie = 3f;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public override void Damage()
    {
        base.Damage();
        Debug.Log("Get damage from CorruptionHazard");
    }

    private void Death()
    {
        StartCoroutine(DeathCoroutine());
    }

    private IEnumerator DeathCoroutine()
    {
        while (counter < TimeToDie)
        {
            counter += Time.deltaTime;
            yield return null;
        }
        Debug.Log("Corruption destroyed");
        canDamage = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.bounds.min.y > boxCollider.bounds.max.y - delta)
            {
                if (counter < TimeToDie)
                {
                    Death();
                }
            }
            else
            {
                Damage();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (counter < TimeToDie)
        {
            StopAllCoroutines();
            counter = 0f;
        }
    }
}
