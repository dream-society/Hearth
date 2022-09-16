using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasticBottleCollectable : CollectableBase
{
    public override void Collect()
    {
        base.Collect();
        Debug.Log("PlasticBottle + 1");
    }

}
