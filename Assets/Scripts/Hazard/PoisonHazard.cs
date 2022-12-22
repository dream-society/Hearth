using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonHazard : HazardBase
{
    private BoxCollider2D bc2D;
    private void Awake()
    {
        bc2D = GetComponent<BoxCollider2D>();
    }

    public void Open()
    {
        bc2D.enabled = true;
        canDamage = true;
    }

    public void Close()
    {
        bc2D.enabled = false;
        canDamage = false;
    }
}
