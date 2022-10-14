using UnityEngine;

public class LeverInteractable : InteractableBase
{
    [SerializeField] private MovingPlatform Platform;

    public override void Interact()
    {
        base.Interact();
        Debug.Log("Interact with Lever");
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("Interact");
        if (Platform != null)
        {
            Platform.Move();
        }
    }
}
