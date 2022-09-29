using UnityEngine;
using TMPro;
using System;

public class PlayerUI : MonoBehaviour
{
    public TextMeshProUGUI lifesTxt;
    public TextMeshProUGUI plasticBottleTxt;
    public RectTransform PowersUI;
    public static Action<int> OnUpdateLife;
    public static Action<int> OnUpdatePlasticBottle;
    public static Action OnTogglePowersUI;

    private void OnEnable()
    {
        OnUpdateLife += UpdateLifeText;
        OnUpdatePlasticBottle += UpdatePlasticBottleText;
        OnTogglePowersUI += TogglePowersUI;
    }

    private void OnDisable()
    {
        OnUpdateLife -= UpdateLifeText;
        OnUpdatePlasticBottle -= UpdatePlasticBottleText;
        OnTogglePowersUI -= TogglePowersUI;
    }

    private void UpdateLifeText(int life)
    {
        lifesTxt.text = "Lifes: " + life.ToString();
    }

    private void UpdatePlasticBottleText(int plasticBottle)
    {
        plasticBottleTxt.text = "Plastic Bottle: " + plasticBottle.ToString();
    }

    private void TogglePowersUI()
    {
        PowersUI.gameObject.SetActive(!PowersUI.gameObject.activeSelf);
    }
    
    public void GazzaPower()
    { 
        PlayerPowerManagement.OnChangePower?.Invoke(Power.GAZZA);
    }

    public void OrsoPower()
    {
        PlayerPowerManagement.OnChangePower?.Invoke(Power.ORSO);
    }

    public void RagnoPower()
    {
        PlayerPowerManagement.OnChangePower?.Invoke(Power.RAGNO);
    }

}
