using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_Attack : Finder
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (ai.state == AI_State.Attack)
        {
            if (check_Enemy(col.gameObject)) // 적
            {
                if (check_body(col.gameObject))
                {
  
                    Mob enemy = col.gameObject.GetComponent<Mob>();
                    if (enemy == null)
                    {
                        // player
                        Player player = col.gameObject.GetComponent<Player>();
                        player.AddDamage(ai.Get_Damage());
                    }
                    else
                    {
                        enemy.AddDamage(ai.Get_Damage());
                    }
                }
            }

            if (Check_Core(col.gameObject)) // 적 코어
            {
                Core core = col.gameObject.GetComponent<Core>();
                core.AddDamage(ai.Get_Damage());
            }
        }
    }
}
