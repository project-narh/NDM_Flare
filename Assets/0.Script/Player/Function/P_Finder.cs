using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Finder : MonoBehaviour
{
    protected string my_tag = null;

    protected virtual void Init()
    {
        my_tag = "Friend";
    }

    protected string Find_tag(string tag)
    {
        if (tag.Equals("Enemy")) return "Friend";
        else if (tag.Equals("Friend")) return "Enemy";
        return null;
    }

    protected bool check_Enemy(GameObject g)
    {
        if (!g.CompareTag(my_tag))
        {
            if (!g.name.Equals("Attack_Range") && !g.name.Equals("Attack"))
            {
                return true; // 상대방의 몸통인 경우
            }
        }
        return false; // 상대방의 몸통이 아닌경우
    }

    protected bool Check_Core(GameObject g)
    {
        string f;
        if (my_tag.Equals("Enemy"))
        {
            f = "F";
        }
        else
        {
            f = "E";
        }

        if(g.CompareTag(f + "_Core"))
        {
            return true;
        }

        return false; // 
    }


    protected bool check_body(GameObject g)
    {
        
        if(g.tag.Equals(Find_tag(my_tag)) && !g.tag.Equals(Find_tag(my_tag) + "_Range") && !g.tag.Equals(Find_tag(my_tag) + "_Attack"))
        {
            return true;
        }
        return false;
    }

    protected virtual void Set_Update()
    {

    }

    private void Update()
    {
        Set_Update();
    }
    private void Start()
    {
        Init();
    }
}
