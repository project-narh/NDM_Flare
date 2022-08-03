using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friendly : Mob
{
    protected override void Init() // �ʱ�ȭ
    {
        base.Init();
        Set_Tag("Friend");
    }

    private void Start()
    {
        Init();
        ai = GetComponent<Mob_AI>();
        if(!is_neutrlity)
            Send_AI(); //AI ���� ����
    }

    public override void Attack()
    {
        base.Attack();

    }

    public override void Dead()
    {
        base.Dead();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    public override void Send_AI()
    {
        ai.Init(this, "E_Core");
    }
}
