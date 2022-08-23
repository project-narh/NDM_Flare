using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_Storage : MonoBehaviour
{
    public static Mob_Storage Instance = null;
    List <Mob> Team = new List<Mob> ();
    List <Mob> Enemy = new List<Mob> ();


    private void Awake()
    {
        if (Instance != null) Destroy(this);
        else
        {
            Instance = this;
        }
        Instance.Init();
    }

    public void Add_Mob(Mob mob, bool n) // 0 : Team    1 : Enemy
    {
        if(n)
        {
            Team.Add(mob);
        }
        else
        {
            Enemy.Add(mob);
        }
    }

    public void Remove_Mob(Mob mob, bool n)
    {
        if (n)
        {
            Team.Remove(mob);
        }
        else
        {
            Enemy.Remove(mob);
        }
    }

    public void Remove_M(Mob mob)
    {
        if (mob.gameObject.CompareTag("Enemy"))
        {
            Enemy.Remove(mob);
        }
        else
        {
            Team.Remove(mob);
        }
    }

    public void Remove_a()
    {
        Team.Clear();
        Enemy.Clear();
    }

    public void Init()
    {
        Remove_a();
        Debug.Log("초기화 완료");
    }

    public void Start_Debuff(DeBuff debuff)
    {
        for(int i = 0; i < Team.Count; i++)
        {
            Team[i].Debuff(debuff);
        }
    }

    public void Enemy_kill()
    {
        if (Enemy.Count != 0)
        {
            for (int i = 0; i < Enemy.Count; i++)
            {
                Debug.Log(i);
                if (!(Enemy[i].Get_neutrlity()))
                {
                    Enemy[i].Dead();
                }
            }
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            Enemy_kill();
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("L 눌림");
            Start_Debuff(DeBuff.stiffness);
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("J 눌림");
            Start_Debuff(DeBuff.Slow);
        }

    }
}
