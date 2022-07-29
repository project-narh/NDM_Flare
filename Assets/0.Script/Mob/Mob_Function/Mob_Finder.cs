using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_Finder : Finder
{
    private List<GameObject> list = new List<GameObject>();
    public int Count = 0;
    bool is_mob = false;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (check_Enemy(col.gameObject) || Check_Core(col.gameObject))//적을 발견
        {
            list.Add(col.gameObject);
            if (ai.state != AI_State.Attack)
                ai.Set_State(AI_State.Attack);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (check_Enemy(col.gameObject))
        {
            list.Remove(col.gameObject);
        }
    }

    protected override void Set_Update()
    {
        if(!check_mob())
        {
            if (ai.state != AI_State.move)
                ai.Set_State(AI_State.move);
        }
        else
        {
            if (ai.state != AI_State.Attack)
                ai.Set_State(AI_State.Attack);
        }
    }

    private bool check_mob()
    {
        if (list.Count > 0)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                float t = transform.position.x - list[i].transform.position.x;
                BoxCollider2D col;
                bool is_ = list[i].TryGetComponent<BoxCollider2D>(out col);
                if (is_)
                {
/*                    float pos = (col.offset.x + (col.size.x/2));
                    if (Mathf.Abs(t) > pos)
                    {
                        Remove(list[i]);
                    }
                    else
                    {
                        if (!list[i].active)
                        {
                            Remove(list[i]);
                        }
                    }*/
                }
                else
                {
                    Remove(list[i]);
                }
            }

            if (list.Count > 0)
            {
                return true; // 적 있음
            }
        }
        return false; // 적 없음
    }

    private void Remove(GameObject n)
    {
        list.Remove(n);
    }


}
