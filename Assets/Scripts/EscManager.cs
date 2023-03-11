using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscManager : MonoBehaviour
{
    [SerializeField] private GameObject pause;
    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        pause.SetActive(!pause.activeSelf);
    }
}
