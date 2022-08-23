using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neutrality : MonoBehaviour
{
    //[SerializeField] private GameObject prefeb_t;
    //[SerializeField] private GameObject prefeb_e;
    [Range(0.1f, 0.8f)]
    [SerializeField] public float ratio;
    private Core Enemy, Team;
    private bool is_use = false;
    private string tag = "";

    private void Awake()
    {
        Enemy = GameObject.FindWithTag("E_Core").GetComponent<Core>();
        Team = GameObject.FindWithTag("F_Core").GetComponent<Core>();
        is_use = false;
    }

    private void Update()
    {
        if(!is_use)
        {
            if(Check_HP(Enemy))
            {
                is_use = true;
                tag = "Enemy";
                Spawn(Enemy);
            }
            else if(Check_HP(Team))
            {
                is_use = true;
                tag = "Friend";
                Spawn(Team);
            }
        }
    }

    private bool Check_HP(Core c)
    {
        if ((c.Get_Max_Hp() * ratio) >= c.Get_Hp())
        {
            return true;
        }
        return false;
    }

    private void Spawn(Core c)
    {
        //Mob mob = Instantiate(prefeb,c.transform.position,Quaternion.identity).GetComponent<Mob>();
        c.Spawn_Gollem();
        //mob.Set_Tags(tag);
        if(tag.Equals("Enemy"))
        {
            //Vector3 size = mob.transform.localScale;
            //size.Set(-size.x, -size.y, -size.z);
            //mob.transform.localScale = size;
        }

        //mob.Send_AI();
    }
}
