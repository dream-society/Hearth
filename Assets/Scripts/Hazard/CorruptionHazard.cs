using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hearth.Player;

public class CorruptionHazard : HazardBase
{
    private BoxCollider2D boxCollider;
    private SpriteRenderer sp;
    private float delta = 0.1f;
    private float counter = 0f;
    public float TimeToDie = 3f;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        sp = GetComponentInParent<SpriteRenderer>();
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
        CorruptionManager.CorruptionDeath?.Invoke();
        transform.parent.gameObject.SetActive(false);
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
                if (canDamage)
                {
                    CharacterRun player = collision.GetComponent<CharacterRun>();
                    Damage(player);
                }
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
