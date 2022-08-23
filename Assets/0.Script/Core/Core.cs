using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Spawner))]
public class Core : MonoBehaviour
{
    [SerializeField] float HP;
    [SerializeField] float MaxHP;
    List<Mob> list = new List<Mob>();
    public int num;
    protected Spawner spawner;
    Player p;

    private void Awake()
    {
        p = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    private void Start()
    {
        //p = GameObject.FindWithTag("Player").GetComponent<Player>();
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
    public void AddDamage(float damage) //상대방 공격을 받았을 때
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

/*    public void Spawn(int n, bool team = true)
    {
        Mob_Storage.Instance.Add_Mob(spawner.Spawn(n).GetComponent<Mob>(), team);
    }*/

    public void Spawn(int n)
    {
        Mob_Storage.Instance.Add_Mob(spawner.Spawn(n).GetComponent<Mob>(), true);
    }

    public void Spawn_e(int n)
    {
        Mob_Storage.Instance.Add_Mob(spawner.Spawn(n).GetComponent<Mob>(), false);
    }

    public virtual void Spawn_Gollem()
    {
        spawner.Spawn(3);
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
