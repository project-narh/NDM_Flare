using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Testing : MonoBehaviour
{
    [SerializeField] GameObject Test_Panel;
    //[SerializeField] GameObject mob;
    //[SerializeField] GameObject wave;

    int num = 0;


    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Set_panel();
        }
        else if(Input.GetKeyDown(KeyCode.Z))
        {
            SceneManager.LoadScene("Stage1");
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            SceneManager.LoadScene("Stage2");
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            SceneManager.LoadScene("Stage3");
        }

    }

    private void Set_panel()
    {
        if (Test_Panel.active)
        {
            Time.timeScale = 1;
            Test_Panel.SetActive(false);
        }
        else
        {
            Test_Panel.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
