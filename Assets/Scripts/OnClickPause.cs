using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OnClickPause : MonoBehaviour
{
    [SerializeField] private Button returnButton;
    [SerializeField] private Button toMenu;

    private void Awake()
    {
        returnButton.onClick.AddListener((() => gameObject.SetActive(false)));
        toMenu.onClick.AddListener((() => SceneManager.LoadScene("MainMenu")));
    }

    private void OnEnable()
     {
         Time.timeScale = 0;
     }

     private void OnDisable()
     {
         Time.timeScale = 1;
     }
}
