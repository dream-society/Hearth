using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChimneyHazard : HazardBase
{
    public void Open()
    {
        canDamage= true;
    }

    public void Close()
    {
        canDamage= false;
    }
}
