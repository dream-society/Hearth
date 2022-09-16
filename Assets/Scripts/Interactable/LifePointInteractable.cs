using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePointInteractable : InteractableBase
{
    public override void Interact()
    {
        base.Interact();
        Debug.Log("Interact with Life Point --> +1 life points");
    }
}
