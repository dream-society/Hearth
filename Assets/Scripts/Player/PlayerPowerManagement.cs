using System;
using System.Collections;
using Hearth.Player;
using RoaREngine;
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
    public static Func<float> OnUsePower;
    public bool haveGazzaPower = false;
    private bool haveOrsoPower = false;
    private bool haveRagnoPower = false;
    private bool isOnGazzaForm = false;
    public float timeIngGazzaForm;
    private CharacterRun runComponent;
    private CharacterFly flyComponent;
    [SerializeField] private SpriteRenderer sciarpa;
    [SerializeField] private Color[] sciarpaColorPowers;
    [SerializeField] private float interactTime;

    public bool IsOnGazzaForm { get => isOnGazzaForm; } 

   private void Awake()
    {
        runComponent = GetComponent<CharacterRun>();
        flyComponent = GetComponent<CharacterFly>();
    }

    private void Start()
    {
        currentPower = Power.NONE;
        sciarpa.color = sciarpaColorPowers[0];
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
        if (CheckForPower(power))
        {
            currentPower = power;
        }
    }

    public void ChangePowerGazza() 
    {
        if (haveGazzaPower)
        {
            currentPower = Power.GAZZA;
            PlayerUI.OnTogglePowersUI();
        }
    }

    private void GazzaPower()
    {
        if (!isOnGazzaForm)
        {
            RoarManager.CallPlay("GazzaPower", null);
            isOnGazzaForm = true;
            PlayerUI.OnUsePower(timeIngGazzaForm);
            sciarpa.color = sciarpaColorPowers[1];
            StartCoroutine(GazzaPowerCoroutine());
        }
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

    private IEnumerator GazzaPowerCoroutine()
    {
        runComponent.enabled = false;
        flyComponent.enabled = true;
        yield return new WaitForSeconds(timeIngGazzaForm);
        runComponent.ResetVelocity();
        runComponent.enabled = true;
        flyComponent.enabled = false;
        isOnGazzaForm = false;
        sciarpa.color = sciarpaColorPowers[0];
    }

    private IEnumerator InteractCoroutine()
    {
        yield return new WaitForSeconds(interactTime);
        if (sciarpa.color == sciarpaColorPowers[2])
        {
            sciarpa.color = sciarpaColorPowers[0];
            runComponent.CanInteract = false;
        }
    }

    public void Interact()
    {
        sciarpa.color = sciarpaColorPowers[2];
        StartCoroutine(InteractCoroutine());
    }
}
