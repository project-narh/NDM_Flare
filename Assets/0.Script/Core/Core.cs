using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Spawner))]
public class Core : MonoBehaviour
{
    [SerializeField] float HP;
    [SerializeField] float MaxHP;
    public int num;
    protected Spawner spawner;
    Player p;

    private void Start()
    {
        p = GameObject.FindWithTag("Player").GetComponent<Player>();
        spawner = GetComponent<Spawner>();
        HP = MaxHP;
        Init();
    }

    protected virtual void Init()
    {

    }

    private void Update()
    {
        Core_Update();
    }
    public void AddDamage(float damage) //���� ������ �޾��� ��
    {
        HP -= damage;
        if (HP <= 0)
        {
            Dead();
        }
    }

    protected virtual void Dead()
    {
        p.Dead();
    }

    public void Spawn(int n)
    {
        spawner.Spawn(n);
       //Debug.Log("��ȯ");
    }

    public float Get_Max_Hp()
    {
        return MaxHP;
    }
    public float Get_Hp()
    {
        return HP;
    }

    public void Set_Hp(float num)
    {
        MaxHP = num;
        HP = num;
    }

    protected virtual void Core_Update()
    {

    }
}
