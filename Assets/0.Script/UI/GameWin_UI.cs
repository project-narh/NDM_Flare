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
        GameManager.Instance.Replay();
    }

    private void On_Menu()
    {
        GameManager.Instance.loading(GameManager.Instance.Get_Scene(0));
    }
}
