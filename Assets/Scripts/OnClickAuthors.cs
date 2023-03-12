using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OnClickAuthors : MonoBehaviour
{
    [SerializeField] private Button back; 
    private void Awake()
    {
        back.onClick.AddListener((() => SceneManager.LoadScene("MainMenu")));
    }
}
