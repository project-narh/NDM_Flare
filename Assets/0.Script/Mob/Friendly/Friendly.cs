using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friendly : Mob
{
    public List_Counter counter_f;
    protected override void Init() // 초기화
    {
        base.Init();
        Set_Tag("Friend");
    }

    private void Start()
    {
        Init();
        core = GameObject.FindWithTag("F_Core").GetComponent<Core>();
        ai = GetComponent<Mob_AI>();
        //if(!is_neutrlity)
        //Send_AI(); //AI 정보 전달
        ai.Init(this, "E_Core");
    }

    public override void Attack()
    {
        base.Attack();

    }

    public override void Dead()
    {
        base.Dead();
        Mob_Storage.Instance.Remove_Mob(this,true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

/*    public override void Send_AI()
    {
        ai.Init(this, "E_Core");
    }*/
}
