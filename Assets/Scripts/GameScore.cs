using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameScore: MonoBehaviour
{
    public static int Score;
    public static int Record;
    private TMP_Text _text;
    void Start()
    {
        Score = 0;
        Record = PlayerPrefs.GetInt("Record");
        _text = GetComponent<TMP_Text>();
    }

    void Update()
    {
        _text.text = Score.ToString();
        if (Score > Record)
        {
            Record = Score;
            PlayerPrefs.SetInt("Record", Record);
            PlayerPrefs.Save();
        }
    }
}
