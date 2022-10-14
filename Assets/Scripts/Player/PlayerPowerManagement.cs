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
    public static Func<Power, bool> CanUsePower;
    public static Action<Power> OnUnlockPower;
    private bool haveGazzaPower;
    private bool haveOrsoPower;
    private bool haveRagnoPower;

    private void Start()
    {
        currentPower = Power.NONE;
    }

    private void OnEnable()
    {
        inputHandler.switchPressed += TogglePowerUI;
        inputHandler.powerPressed += UsePower;
        OnChangePower += ChangePower;
        CanUsePower += CheckForPower;
        OnUnlockPower += UnlockPower;
    }

    private void OnDisable()
    {
        inputHandler.switchPressed -= TogglePowerUI;
        inputHandler.powerPressed -= UsePower;
        OnChangePower -= ChangePower;
        CanUsePower -= CheckForPower;
        OnUnlockPower -= UnlockPower;
    }

    public bool CheckForPower(Power power)
    {
        switch (power)
        {
            case Power.GAZZA:
                return haveGazzaPower;
            case Power.ORSO:
                return haveOrsoPower;
            case Power.RAGNO:
                return haveRagnoPower;
            case Power.NONE:
                return false;
        }
        return false;
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

    private void UnlockPower(Power power)
    {
        switch (power)
        {
            case Power.GAZZA:
                haveGazzaPower = true;
                break;
            case Power.ORSO:
                haveOrsoPower = true;
                break;
            case Power.RAGNO:
                haveRagnoPower = true;
                break;
            case Power.NONE:
                break;
        }
    }
}
