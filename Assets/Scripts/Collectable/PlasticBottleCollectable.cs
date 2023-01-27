using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hearth.Player;
using RoaREngine;

public class PlasticBottleCollectable : CollectableBase
{
    public override void Collect(CharacterRun player)
    {
        RoarManager.CallPlay("PlasticBottle", null);
        player.CollectPlasticBottle(value);
        base.Collect(player);
    }

}
