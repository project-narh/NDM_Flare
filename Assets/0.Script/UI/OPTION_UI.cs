using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OPTION_UI : MonoBehaviour
{
    public Button[] buttons;

    private void Start()
    {
        buttons[0].onClick.AddListener(On_Menu);
        buttons[1].onClick.AddListener(On_Replay);
        buttons[2].onClick.AddListener(On_End);
    }

    public void On_Menu()
    {
        SoundManager.Instance.play_sfx("UI");
        GameManager.Instance.home();
    }

    public void On_Replay()
    {
        SoundManager.Instance.play_sfx("UI");
        GameManager.Instance.Replay();
    }

    public void On_End()
    {
        SoundManager.Instance.play_sfx("UI");
        Application.Quit();
    }



}
