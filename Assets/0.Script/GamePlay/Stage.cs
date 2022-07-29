using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    Spawner spawner;
    [SerializeField] int[] Wave;
    [SerializeField] float Timer; // ��� Ÿ�̸�

    private void Awake()
    {
        spawner = GameObject.FindWithTag("E_Core").GetComponent<Spawner>();
    }
}
