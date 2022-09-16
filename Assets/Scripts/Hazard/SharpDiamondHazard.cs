using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharpDiamondHazard : HazardBase
{
    public override void Damage()
    {
        base.Damage();
        Debug.Log("Get damage from SharpDiamond");
    }
}
