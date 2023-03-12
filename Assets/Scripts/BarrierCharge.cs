using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarrierCharge : MonoBehaviour
{
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void Update()
    {
        Barrier.Charge += 0.1f * Time.deltaTime;
        _image.fillAmount = Barrier.Charge;
    }
}
