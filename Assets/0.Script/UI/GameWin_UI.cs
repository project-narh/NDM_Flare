using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameWin_UI : MonoBehaviour
{
    [SerializeField] Button Next;
    [SerializeField] Button Menu;

    private void Start()
    {
        Next.onClick.AddListener(On_Next);
        Menu.onClick.AddListener(On_Menu);
    }

    private void On_Next()
    {
        SoundManager.Instance.play_sfx("UI");
        GameManager.Instance.Next_Scene();
    }

    private void On_Menu()
    {
        SoundManager.Instance.play_sfx("UI");
        GameManager.Instance.loading(GameManager.Instance.Get_Scene(0));
    }
}
