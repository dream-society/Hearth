using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputHandler", menuName = "DreamSociety/Input/Input Handler")]
public class InputHandler : ScriptableObject, Input.IPlayerActions, Input.IUIActions
{
    public event UnityAction<Vector2> move = delegate { };
    public event UnityAction jumpPressed = delegate { };
    public event UnityAction jumpReleased = delegate { };
    public event UnityAction interactPressed = delegate { };
    public event UnityAction interactReleased = delegate { };
    public event UnityAction powerPressed = delegate { };
    public event UnityAction powerReleased = delegate { };
    public event UnityAction switchPressed = delegate { };
    public event UnityAction pausePressed = delegate { };
    public event UnityAction runPressed = delegate { };
    public event UnityAction runReleased = delegate { };

    private Input input;

    private void OnEnable()
    {
        if (input == null)
        {
            input = new Input();
            input.Player.SetCallbacks(this);
            input.UI.SetCallbacks(this);

            // FIXME: This is a hack to move the player
            input.Player.Enable();
        }
    }

    private void OnDisable() => DisableAllInput();

    public void DisableAllInput()
    {
        input.Player.Disable();
        input.UI.Disable();
    }

    public void EnablePlayerInput()
    {
        input.Player.Enable();
        input.UI.Disable();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void EnableUIInput()
    {
        input.Player.Disable();
        input.UI.Enable();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        move?.Invoke(value);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            jumpPressed?.Invoke();
        }

        if (context.canceled)
        {
            jumpReleased?.Invoke();
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            interactPressed?.Invoke();
        }

        if (context.canceled)
        {
            interactReleased?.Invoke();
        }
    }

    public void OnPower(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            powerPressed?.Invoke();
        }

        if (context.canceled)
        {
            powerReleased?.Invoke();
        }
    }

    public void OnSwitch(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            switchPressed?.Invoke();
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            pausePressed?.Invoke();
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            runPressed?.Invoke();
        }
        if (context.canceled)
        {
            runReleased?.Invoke();
        }
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
    }

    public void OnClick(InputAction.CallbackContext context)
    {
    }

    public void OnNavigate(InputAction.CallbackContext context)
    {
    }


    public void OnPoint(InputAction.CallbackContext context)
    {
    }


    public void OnSubmit(InputAction.CallbackContext context)
    {
    }
}
