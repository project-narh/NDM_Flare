using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
