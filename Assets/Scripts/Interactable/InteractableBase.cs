using Hearth.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBase : MonoBehaviour, IInteractable
{
    protected bool interacted;

    public virtual void Interact()
    {
        
    }
}
