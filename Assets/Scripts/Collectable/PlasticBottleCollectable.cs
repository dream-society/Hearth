using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hearth.Player;

public class PlasticBottleCollectable : CollectableBase
{
    public override void Collect(CharacterRun player)
    {
        player.CollectPlasticBottle(value);
        base.Collect(player);
    }

}
