using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OnClickMainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text record;
    [SerializeField] private GameObject helpWindow;
    [SerializeField] private Button play;
    [SerializeField] private Button exit;
    [SerializeField] private Button help;
    [SerializeField] private Button helpExit;
    private void Awake()
    {
        
        record.text = $"Лучший счет: {PlayerPrefs.GetInt("Record")}";
        play.onClick.AddListener((() => SceneManager.LoadScene("SampleScene")));
        exit.onClick.AddListener((() => Application.Quit()));
        help.onClick.AddListener((() => helpWindow.SetActive(true)));
        helpExit.onClick.AddListener((() => helpWindow.SetActive(false)));
    }
}
