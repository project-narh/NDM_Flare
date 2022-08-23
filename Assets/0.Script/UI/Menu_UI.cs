using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_UI : MonoBehaviour
{
    [SerializeField] Button start;
    [SerializeField] Button Exit;
    [SerializeField] Button option;


    private void Start()
    {
        start.onClick.AddListener(On_Start);
        Exit.onClick.AddListener(On_Exit);
        option.onClick.AddListener(On_option);
        SoundManager.Instance.play_BGM("Main");
    }

    private void On_Start()
    {
        SoundManager.Instance.play_sfx("UI");
        GameManager.Instance.GameStart();
    }

    private void On_Exit()
    {
        SoundManager.Instance.play_sfx("UI");
        Application.Quit();
    }
    private void On_option()
    {
        UIManager.Instance.Open_();
    }
}
