using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hearth.Player;

public abstract class CollectableBase : MonoBehaviour, ICollectable
{
    protected bool collected;
    public int value = 1;

    public virtual void Collect(CharacterController2D player)
    {
        collected = true;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !collected)
        {
            CharacterController2D player = collision.GetComponent<CharacterController2D>();
            Collect(player);
        }
    }
}
