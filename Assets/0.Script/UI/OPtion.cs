using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OPtion : MonoBehaviour
{
    [SerializeField] Button can;
    [SerializeField] Button Exit;


    private void Start()
    {
        can.onClick.AddListener(On_Start);
        Exit.onClick.AddListener(On_Exit);
    }

    private void On_Start()
    {
        SoundManager.Instance.play_sfx("UI");
        UIManager.Instance.Open_();
    }

    private void On_Exit()
    {
        SoundManager.Instance.play_sfx("UI");
        Application.Quit();
    }
}
