using Hearth.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBase : MonoBehaviour, IInteractable
{
    protected bool interacted;

    public virtual void Interact(CharacterRun player)
    {
        player.Interact();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !interacted)
        {
            CharacterRun player = collision.GetComponent<CharacterRun>();
            Interact(player);
        }
    }
}
