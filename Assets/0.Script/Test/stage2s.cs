using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class stage2s : MonoBehaviour
{
    [SerializeField] Mob_s[] mob;
    [SerializeField] UI_s[] wave;
    [SerializeField] UI_s core;
    [SerializeField] private Pooling[] pool;
    [SerializeField] Button[] button; // 0 : mob , 1 : stage , 2 : applay
    [SerializeField] Neutrality neutrality;
    [SerializeField] TMP_InputField ra;
    [SerializeField] System_cool o;
    [SerializeField] TMP_InputField system_1;
    [SerializeField] TMP_InputField system_2;


    Player player;
    Core C;
    Enemy_Core enemy;


    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        C = GameObject.FindWithTag("F_Core").GetComponent<Core>();
        enemy = GameObject.FindWithTag("E_Core").GetComponent<Enemy_Core>();
    }



    private void Start()
    {
        button_Active();
        core.Set(C.Get_Max_Hp(), enemy.Get_Max_Hp());
        for (int i = 0; i < mob.Length; i++)
        {
            mob[i].Setting();
            if (i < wave.Length)
            {
                Wave ws = enemy.Get(i);
                wave[i].Set(ws.Count, ws.delay);
            }
        }
        sett();
        GameObject[] o = GameObject.FindGameObjectsWithTag("Spawner");
        pool = new Pooling[o.Length];
        for (int i = 0; i < o.Length; i++)
        {
            pool[i] = o[i].GetComponent<Pooling>();
        }
    }


    private void button_Active()
    {
        button[0].onClick.AddListener(Mob_clear);
        button[1].onClick.AddListener(stage_clear);
        button[2].onClick.AddListener(Apply);
    }

    private void Mob_clear()
    {
        for (int i = 0; i < pool.Length; i++)
        {
            pool[i].Clear();
        }
    }
    private void stage_clear()
    {
        enemy.Stage_clear();
    }

    private void Apply()
    {
        for (int i = 0; i < mob.Length; i++)
        {
            mob[i].apply();
            C.Set_Hp(core.Get_num1());
            enemy.Set_Hp(core.Get_num2());
            if (i < wave.Length)
            {
                enemy.Set(i, (int)wave[i].Get_num1(), wave[i].Get_num2());
            }
            Set();
        }
    }

    private void relay(ref TMP_InputField f, float num)
    {
        f.text = num.ToString();
    }

    private float relay(TMP_InputField f)
    {
        return float.Parse(f.text);
    }

    private void Set()
    {
        neutrality.ratio = relay(ra)/100;

        o.Stop = relay(system_1);
        o.Slow = relay(system_2);
    }

    private void sett()
    {
        relay(ref ra, neutrality.ratio*100);
        relay(ref system_1,o.Stop);
        relay(ref system_2, o.Slow);
    }
}
