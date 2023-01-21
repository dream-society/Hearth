using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using System.Collections;

public class PlayerUI : MonoBehaviour
{
    public Image activePowerImage;
    public Image borderPowerImage;
    public TextMeshProUGUI plasticBottleTxt;
    public RectTransform PowersUI;
    public static Action<int> OnUpdateLife;
    public static Action<int> OnUpdatePlasticBottle;
    public static Action OnTogglePowersUI;
    public static Action<float> OnUsePower;
    public Sprite[] bordersPowers;

    private void OnEnable()
    {
        OnUpdateLife += UpdateLifeText;
        OnUpdatePlasticBottle += UpdatePlasticBottleText;
        OnTogglePowersUI += TogglePowersUI;
        OnUsePower += UsePower;
    }

    private void OnDisable()
    {
        OnUpdateLife -= UpdateLifeText;
        OnUpdatePlasticBottle -= UpdatePlasticBottleText;
        OnTogglePowersUI -= TogglePowersUI;
        OnUsePower -= UsePower;
    }

    private void UpdateLifeText(int life)
    {
        
    }

    private void UpdatePlasticBottleText(int plasticBottle)
    {
        int prev = int.Parse(plasticBottleTxt.text);
        plasticBottle += prev;
        plasticBottleTxt.text = plasticBottle.ToString();
    }

    private void TogglePowersUI()
    {
        PowersUI.gameObject.SetActive(!PowersUI.gameObject.activeSelf);
    }
    
    public void GazzaPower(Image image)
    {
        if (PlayerPowerManagement.CanUsePower.Invoke(Power.GAZZA))
        {
            PlayerPowerManagement.OnChangePower?.Invoke(Power.GAZZA);
            ChangeActivePowerImage(image);
            borderPowerImage.sprite = bordersPowers[0];
        }
    }

    public void OrsoPower(Image image)
    {
        if (PlayerPowerManagement.CanUsePower.Invoke(Power.ORSO))
        {
            PlayerPowerManagement.OnChangePower?.Invoke(Power.ORSO);
            ChangeActivePowerImage(image);
            borderPowerImage.sprite = bordersPowers[1];
        }
    }

    public void RagnoPower(Image image)
    {
        if (PlayerPowerManagement.CanUsePower.Invoke(Power.RAGNO))
        {
            PlayerPowerManagement.OnChangePower?.Invoke(Power.RAGNO);
            ChangeActivePowerImage(image);
            borderPowerImage.sprite = bordersPowers[2];
        }
    }

    private void ChangeActivePowerImage(Image image)
    {
        borderPowerImage.enabled = true;
        activePowerImage.sprite = image.sprite;
    }

    private void UsePower(float time)
    {
        StartCoroutine(StartPowerCD(time));
    }

    private IEnumerator StartPowerCD(float time)
    {
        activePowerImage.fillAmount = 0;
        float counter = 0;
        while (counter < time)
        {
            activePowerImage.fillAmount = Mathf.Lerp(0, 1, counter / time);
            counter += Time.deltaTime;
            yield return null;
        }
        activePowerImage.fillAmount = 1;
    }
}
