using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePointCollectable : CollectableBase
{
    public override void Collect()
    {
        base.Collect();
        Debug.Log("LifePoint + 1");
    }
}
