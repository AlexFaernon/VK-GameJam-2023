using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OnClickPause : MonoBehaviour
{
    private void OnEnable()
     {
         Time.timeScale = 0;
     }

     private void OnDisable()
     {
         Time.timeScale = 1;
     }
}
