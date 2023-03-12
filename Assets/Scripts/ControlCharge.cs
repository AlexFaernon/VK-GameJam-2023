using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlCharge : MonoBehaviour
{
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void Update()
    {
        _image.fillAmount = ControlEnemy.Charge / 3.0f;
    }
}
