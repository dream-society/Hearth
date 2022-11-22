
using Hearth.Player;
using UnityEngine;

public class CoyoteTime : MonoBehaviour
{
    [SerializeField] private float graceTime;
    public bool Active { get; private set; }

    private float coyoteTimeStart;
    private CharacterController2D controller;
    private bool groundedPreviousFrame = false;

    private void Awake()
    {
        controller = GetComponent<CharacterController2D>();
    }

    private void Update()
    {
        if (groundedPreviousFrame && !controller.isGrounded)
        {
            StartCoyoteTime();
        }

        CheckCoyoteTime();

        groundedPreviousFrame = controller.isGrounded;
    }

    public void StartCoyoteTime()
    {
        if (Active) return;

        Active = true;
        coyoteTimeStart = Time.time;
    }

    public void CheckCoyoteTime()
    {
        if (Active && Time.time > coyoteTimeStart + graceTime)
        {
            Active = false;
        }
    }
}
