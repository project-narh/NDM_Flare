using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 시간이 지나면 디버프를 주기 위한 스크립트
public class Timer : MonoBehaviour
{
    [SerializeField] private float timer;
    private float N_timer = 0;
    private bool is_Use = false;

    private void Awake()
    {
        N_timer = 0;
        is_Use = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!is_Use)
        {
            N_timer+= Time.deltaTime;
            if(N_timer >=timer)
            {
                is_Use=true;
                
            }
        }
    }
}
