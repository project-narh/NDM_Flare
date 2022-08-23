using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOver : MonoBehaviour
{
    [SerializeField] GameObject[] tip;

    private void Awake()
    {
        for(int i = 0; i < tip.Length; i++)
        {
            tip[i].SetActive(false);
        }
    }

    public void OpenTip(int num)
    {
        tip[num].SetActive(true);
    }

    public void CloseTip(int num)
    {
        tip[num].SetActive(false);
    }

}
