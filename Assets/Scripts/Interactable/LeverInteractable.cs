using Hearth.Player;
using RoaREngine;
using UnityEngine;

public class LeverInteractable : InteractableBase
{
    [SerializeField] private MovingPlatform Platform;

    public override void Interact(CharacterRun player)
    {
        base.Interact(player);
        Debug.Log("Interact with Lever");
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("Interact");
        if (Platform != null)
        {
            Platform.Move();
        }
    }

    public void PlayLeverSFX()
    {
        RoarManager.CallPlay("Switch", transform);
    }

}
