using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameScore: MonoBehaviour
{
    public static int Score;
    public static int Record;
    
    void Start()
    {
        Score = 0;
        Record = PlayerPrefs.GetInt("Record");
    }

    void Update()
    {
        if (Score > Record)
        {
            Record = Score;
            PlayerPrefs.SetInt("Record", Record);
            PlayerPrefs.Save();
        }
    }  
}
