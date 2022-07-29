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
        //Debug.Log("Player Attack Ȱ��ȭ \n ��Ȱ��ȭ Ÿ�� : " + my_tag);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (player.Is_Attack())
        {
            if (check_Enemy(col.gameObject))
            {
                if (check_body(col.gameObject))
                {
                    Mob enemy = col.GetComponent<Mob>();
                    enemy.AddDamage(player.Get_Damage());

                }
            }
            if (Check_Core(col.gameObject)) // �� �ھ�
            {
                Core core = col.gameObject.GetComponent<Core>();
                core.AddDamage(player.Get_Damage());
            }
        }

    }
}
