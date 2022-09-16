using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollectableBase : MonoBehaviour, ICollectable
{
    protected bool collected;

    public virtual void Collect()
    {
        collected = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !collected)
        {
            Collect();
        }
    }
}
