using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBarHandler : MonoBehaviour
{
    private RectMask2D mask;

    private void Awake()
    {
        mask = GetComponent<RectMask2D>();
    }

    private void OnEnable()
    {
        PlayerUI.OnUpdateLife += OnUpdateLife;
    }

    private void OnDisable()
    {
        PlayerUI.OnUpdateLife -= OnUpdateLife;
    }

    private void OnUpdateLife(int currentValue)
    {
        mask.padding = new Vector4(0, 0, (3 - currentValue) * 118, 0);
    }
}
