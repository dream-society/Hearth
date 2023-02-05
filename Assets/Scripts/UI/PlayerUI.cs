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
    public Button PowerUIFocus;
    public static Action<int> OnUpdatePlasticBottle;
    public static Action OnTogglePowersUI;
    public static Action<float> OnUsePower;
    public static Action<int> OnUpdateLife;
    public Sprite[] bordersPowers;
    public Sprite[] circlePowers;
    [SerializeField] private InputHandler input;

    private void OnEnable()
    {
        OnUpdatePlasticBottle += UpdatePlasticBottleText;
        OnTogglePowersUI += TogglePowersUI;
        OnUsePower += UsePower;
    }

    private void OnDisable()
    {
        OnUpdatePlasticBottle -= UpdatePlasticBottleText;
        OnTogglePowersUI -= TogglePowersUI;
        OnUsePower -= UsePower;
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

        if (PowersUI.gameObject.activeInHierarchy)
        {
            PowerUIFocus.Select();
            // enable ui input
            input.EnableUIInput();
        }
        else
        {
            // Enable player input
            input.EnablePlayerInput();
        }

        Cursor.visible = PowersUI.gameObject.activeSelf;
    }
    
    public void GazzaPower(Sprite sprite)
    {
        if (PlayerPowerManagement.CanUsePower.Invoke(Power.GAZZA))
        {
            PlayerPowerManagement.OnChangePower?.Invoke(Power.GAZZA);
            ChangeActivePowerImage(sprite);
            borderPowerImage.sprite = bordersPowers[0];
        }
    }

    public void OrsoPower(Sprite sprite)
    {
        if (PlayerPowerManagement.CanUsePower.Invoke(Power.ORSO))
        {
            PlayerPowerManagement.OnChangePower?.Invoke(Power.ORSO);
            ChangeActivePowerImage(sprite);
            borderPowerImage.sprite = bordersPowers[1];
        }
    }

    public void RagnoPower(Sprite sprite)
    {
        if (PlayerPowerManagement.CanUsePower.Invoke(Power.RAGNO))
        {
            PlayerPowerManagement.OnChangePower?.Invoke(Power.RAGNO);
            ChangeActivePowerImage(sprite);
            borderPowerImage.sprite = bordersPowers[2];
        }
    }

    private void ChangeActivePowerImage(Sprite sprite)
    {
        borderPowerImage.enabled = true;
        activePowerImage.sprite = sprite;
    }

    private void UsePower(float time)
    {
        StartCoroutine(StartPowerCD(time));
        GazzaPower(circlePowers[0]);
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
