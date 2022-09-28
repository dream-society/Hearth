using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverInteractable : InteractableBase
{
    public MovingPlatform Platform;

    public override void Interact()
    {
        base.Interact();
        Debug.Log("Interact with Lever");
        Platform.Move();
    }
}
