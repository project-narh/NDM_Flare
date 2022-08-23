using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_spawn : MonoBehaviour
{
    Enemy_Core core;
    [SerializeField] Spawner spawner;
    [SerializeField] GameObject obj;
    [SerializeField] public float Spawn_time;
    [SerializeField] public float wave;
    float time = 0;
    bool is_ = false;

    private void Awake()
    {
        core = gameObject.GetComponent<Enemy_Core>();
    }

    private void Update()
    {
        if (!is_)
        {
            if (Spawn_time == 0 && wave != 0)
            {
                if (core.Get_wave() == wave)
                {
                    obj.SetActive(true);
                    is_ = true;
                }
            }
            else if (Spawn_time != 0 && wave == 0)
            {
                time += Time.deltaTime;
                Debug.Log(time);
                if (time >= Spawn_time)
                {
                    obj.SetActive(true);
                    is_ = true;
                }
            }
            else if (Spawn_time == 0 && wave == 0)
            {
                Spawn_time = 0;
            }
        }
    }
}
