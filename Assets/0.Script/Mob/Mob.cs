using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Team { Enemy, Friend }

public class Mob : MonoBehaviour
{
    [SerializeField] Mob_Stat stat;
    [field: SerializeField] protected float HP { get; set; }
    /*    [field: SerializeField] protected float Max_HP;
        [SerializeField] private float Spawn_Cooltime;
        [field: SerializeField] protected float Atteck_cooltime { get; set; }
        [field: SerializeField] protected float Atteck_Damage { get; set; }
        [field: SerializeField] public float Speed { get; protected set; }*/

    protected Mob_AI ai;
    protected Pooling pool;
    protected bool is_wait = false;
    protected IEnumerator coroutine;

    protected virtual void Init()
    {
        HP = stat.Max_HP;

    }

    private void OnEnable()
    {

        HP = stat.Max_HP;
    }

    protected void Set_Tags(string tag)
    {
        gameObject.tag = tag;
        transform.GetChild(0).tag = tag + "_Range";
        transform.GetChild(1).tag = tag + "_Attack";
        if (tag.Equals("Enemy"))
        {
            //Debug.Log("여기 실행");
            this.gameObject.layer = 8;
            transform.GetChild(0).gameObject.layer = 10;
            transform.GetChild(1).gameObject.layer = 12;
        }
        else if (tag.Equals("Friend"))
        {
            //Debug.Log("여기 실행2");
            this.gameObject.layer = 7;
            transform.GetChild(0).gameObject.layer = 9;
            transform.GetChild(1).gameObject.layer = 11;
        }
    }

    protected void Set_Tag(string tag)
    {
        gameObject.tag = tag;
        foreach (Transform t in transform)
        {
            t.tag = tag;
        }
    }

    public float Get_Speed()
    {
        return stat.Speed;
    }

    public float Get_Attack_Damage()
    {
        return stat.Attack_Damage;
    }
    public void AddDamage(float damage) //상대방 공격을 받았을 때
    {
        HP -= damage;
        Debug.Log("[Enemy] HP : " + HP);
        if (HP <= 0)
        {
            Dead();
        }
    }

    public virtual void Attack()
    {
        coroutine = Attack_Cool();
        StartCoroutine(coroutine);
    }

    public void Set_Pool(Pooling pool)
    {
        this.pool = pool;
    }

    public virtual void Dead()
    {
        ai.Set_State(AI_State.dead);
    }

    IEnumerator Attack_Cool()
    {
        is_wait = true;
        yield return new WaitForSeconds(stat.Attack_Cooltime);
        is_wait = false;
        ai.Set_State(AI_State.Attack);
    }

    public void Attack_cancel()
    {
        if (is_wait)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
                coroutine = null;
            }
        }
    }

    public float Get_spawn_c()
    {
        return stat.Spawn_Cooltime;
    }

    public void Pooling()
    {
        pool.Disabled(this.gameObject);
    }
}
