using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hearth.Player;

public class LifePointCollectable : CollectableBase
{
    public override void Collect(CharacterRun player)
    {
        player.GetHealed(value);
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("Collect");
        collected = true;
    }

    public void Deactive(CharacterRun player)
    {
        base.Collect(player);
    }
}
