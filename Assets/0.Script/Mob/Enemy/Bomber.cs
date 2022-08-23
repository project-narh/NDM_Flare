using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : Enemy
{
    private Animator ani;
    private bool _isUse = false;

    private void Awake()
    {
        ani = gameObject.transform.GetChild(1).GetComponentInChildren<Animator>();
        //ani.Play("Effect_Bomber_Exokisuib", -1, 0f);
        ani.gameObject.SetActive(false);

    }

    private void OnEnable()
    {
        _isUse = false;
        ani.Play("Effect_Bomber_Exokisuib", -1, 0f);
        ani.gameObject.SetActive(false);
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if(ai.state == AI_State.Attack)
        {
            if(!_isUse)
            {
                _isUse = true;
                Boom();
            }
        }
    }

    private void Boom()
    {
        ani.gameObject.SetActive(true);
        ani.Play("Effect_Bomber_Exokisuib", -1, 0f);
        Invoke("Dead_cool", 0.4f);
    }

    private void Dead_cool()
    {
        ai.Set_State(AI_State.dead);
    }
}
