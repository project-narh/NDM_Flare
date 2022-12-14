using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver_UI : MonoBehaviour
{
    [SerializeField] Button Replay;
    [SerializeField] Button Menu;

    private void Start()
    {
        Replay.onClick.AddListener(On_Replay);
        Menu.onClick.AddListener(On_Menu);
    }

    private void On_Replay()
    {
        SoundManager.Instance.play_sfx("UI");
        GameManager.Instance.Replay();
    }

    private void On_Menu()
    {
        SoundManager.Instance.play_sfx("UI");
        GameManager.Instance.loading(GameManager.Instance.Get_Scene(0));
    }
}
