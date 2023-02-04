using Hearth.Player;
using RoaREngine;
using UnityEngine;

public class LeverInteractable : InteractableBase
{
    [SerializeField] private MovingPlatform Platform;

    private Animator animator;

    public bool Interacted { get => interacted; }
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public override void Interact()
    {
        interacted = true;
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
