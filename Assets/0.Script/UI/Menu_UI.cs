using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_UI : MonoBehaviour
{
    [SerializeField] Button start;
    [SerializeField] Button Exit;


    private void Start()
    {
        start.onClick.AddListener(On_Start);
        Exit.onClick.AddListener(On_Exit);
    }

    private void On_Start()
    {
        GameManager.Instance.GameStart();
    }

    private void On_Exit()
    {
        Application.Quit();
    }
}
