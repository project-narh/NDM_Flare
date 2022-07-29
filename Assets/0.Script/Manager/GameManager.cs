using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    [SerializeField] private string[] scene;
    //0 : Title , 1 : Stage 1
    private int scene_number = 0;
    [SerializeField] private GameObject[] panel;
    public bool is_Game = false;
    //0 : over, 1: win

    private void Awake()
    {
        if (Instance != null) Destroy(this);
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void GameStart()
    {
        scene_number = 1;
        Loading.Load(scene[scene_number]);
    }

    public void GameOver()
    {
        is_Game = false;
        Instantiate(panel[0],GameObject.Find("Canvas").transform);
        Time.timeScale = 0;
    }

    public void Win()
    {
        is_Game = false;
        Instantiate(panel[1], GameObject.Find("Canvas").transform);
        Time.timeScale = 0;
    }

   public void loading(string name)
    {
        Loading.Load(name);
    }

    public void Replay()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        is_Game = true;

    }

    public string Get_Scene(int n)
    {
        Time.timeScale = 1;
        return scene[n];
    }


}
