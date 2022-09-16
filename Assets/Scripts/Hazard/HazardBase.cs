using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardBase : MonoBehaviour, IHazard
{
    public virtual void Damage()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Damage();
        }
    }
}
