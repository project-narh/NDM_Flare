using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : P_Finder
{
    Player player;
    private void Start()
    {
        Init();
        player = GetComponentInParent<Player>();
        //Debug.Log("Player Attack 활성화 \n 비활성화 타겟 : " + my_tag);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (player.Is_Attack())
        {
            if (check_Enemy(col.gameObject))
            {
                if (check_body(col.gameObject))
                {
                    if (!col.gameObject.name.Equals("Range"))
                    {
                        Mob enemy = col.GetComponent<Mob>();
                        enemy.AddDamage(player.Get_Damage());
                        //Debug.Log(player.Get_Damage() + "데미지");
                    }

                }
            }
            if (Check_Core(col.gameObject)) // 적 코어
            {
                Core core = col.gameObject.GetComponent<Core>();
                core.AddDamage(player.Get_Damage());
            }
        }

    }
}
