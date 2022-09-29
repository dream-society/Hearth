using System;
using UnityEngine;

public enum Power
{
    GAZZA,
    ORSO,
    RAGNO,
    NONE
}

public class PlayerPowerManagement : MonoBehaviour
{
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private Power currentPower;
    public static Action<Power> OnChangePower;

    private void Start()
    {
        currentPower = Power.NONE;
    }

    private void OnEnable()
    {
        inputHandler.switchPressed += TogglePowerUI;
        inputHandler.powerPressed += UsePower;
        OnChangePower += ChangePower;
    }

    private void OnDisable()
    {
        inputHandler.switchPressed -= TogglePowerUI;
        inputHandler.powerPressed -= UsePower;
        OnChangePower -= ChangePower;
    }

    private void TogglePowerUI()
    {
        PlayerUI.OnTogglePowersUI();
    }

    private void UsePower()
    {
        switch (currentPower)
        {
            case Power.GAZZA:
                GazzaPower();
                break;
            case Power.ORSO:
                break;
            case Power.RAGNO:
                break;
            case Power.NONE:
                break;
            default:
                break;
        }
    }

    private void ChangePower(Power power)
    {
        currentPower = power;
    }

    private void GazzaPower()
    {
        
    }
}
