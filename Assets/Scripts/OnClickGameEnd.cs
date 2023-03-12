using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class OnClickGameEnd : MonoBehaviour
{
    [SerializeField] private Button repeat;
    [SerializeField] private Button toMenu;
    [SerializeField] private TMP_Text score;
    [SerializeField] private TMP_Text record;

    private void Awake()
    {
        repeat.onClick.AddListener((() => SceneManager.LoadScene("SampleScene")));
        toMenu.onClick.AddListener((() => SceneManager.LoadScene("MainMenu")));
        score.text = $"Счет: {GameScore.Score.ToString()}";
        record.text = $"Ваш рекорд: {GameScore.Record.ToString()}";
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
