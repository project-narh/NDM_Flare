using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance = null;
    public GameObject Menu, option;
    private bool is_option = false; // true : 메뉴  false : 옵션
    private bool is_Open = false;
    private GameObject open;

    private void Awake()
    {
        if (Instance != null) Destroy(this);
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

    }

    private void Start()
    {
        Check();
    }

    public void Check()
    {
        if (GameManager.Instance.Get_Scene() != "Menu")
        {
            is_option = false;
        }
        else
        {
            is_option = true;
        }
        is_Open = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Open_();
        }
    }

    public void Open_()
    {
        if (!is_Open)
        {
            SoundManager.Instance.play_sfx("UI");
            if (is_option)
            {
                open = Instantiate(Menu, GameObject.Find("Canvas").transform);
            }
            else
            {
                open = Instantiate(option, GameObject.Find("Canvas").transform);
            }
            is_Open = true;
        }
        else
        {
            SoundManager.Instance.play_sfx("UI");
            Destroy(open);
            is_Open = false;
            Time.timeScale = 1;
        }
    }



}
