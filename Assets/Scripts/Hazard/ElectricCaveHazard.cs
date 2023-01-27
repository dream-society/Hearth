using RoaREngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricCaveHazard : HazardBase
{
    private void Start()
    {
        RoarManager.CallPlay("ElectricCave", transform);
    }
}
