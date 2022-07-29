using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public struct Mob_s
{
    /*[SerializeField] private Mob_Stat file;
    [SerializeField] private InputField Max;
    [SerializeField] private InputField spawn;
    [SerializeField] private InputField A_cool;
    [SerializeField] private InputField A_damage;
    [SerializeField] private InputField speed;*/
    [SerializeField] private Mob_Stat file;
    [SerializeField] private TMP_InputField Max;
    [SerializeField] private TMP_InputField spawn;
    [SerializeField] private TMP_InputField A_cool;
    [SerializeField] private TMP_InputField A_damage;
    [SerializeField] private TMP_InputField speed;

    private void relay(ref TMP_InputField f, float num)
    {
        f.text = num.ToString();
    }

    private float relay(TMP_InputField f)
    {
        return float.Parse(f.text);
    }

    public void Setting()
    {
        relay(ref Max, file.Max_HP);
        relay(ref spawn, file.Spawn_Cooltime);
        relay(ref A_cool, file.Attack_Cooltime);
        relay(ref A_damage, file.Attack_Damage);
        relay(ref speed, file.Speed);
    }

    public void apply()
    {
        file.Set(relay(Max), relay(spawn), relay(A_cool), relay(A_damage), relay(speed));
    }
}

[System.Serializable]
public struct Player_s
{
    /*    [SerializeField] private InputField Max;
        [SerializeField] private InputField A_cool;
        [SerializeField] private InputField A_damage;
        [SerializeField] private InputField speed;*/
    [SerializeField] private TMP_InputField Max;
    [SerializeField] private TMP_InputField A_cool;
    [SerializeField] private TMP_InputField A_damage;
    [SerializeField] private TMP_InputField speed;

    private void relay(ref TMP_InputField f, float num)
    {
        f.text = num.ToString();
    }

    private float relay(TMP_InputField f)
    {
        return float.Parse(f.text);
    }

    public void Setting(Player p)
    {
        relay(ref Max, p.MAX_HP);
        relay(ref A_cool, p.Attack_Cooltime);
        relay(ref A_damage, p.Attack_Damage);
        relay(ref speed, p.Speed);
    }

    public void apply(ref Player p)
    {
        p.Set(relay(Max), relay(A_cool), relay(A_damage), relay(speed));
    }
}

[System.Serializable]
public struct UI_s
{
/*    [SerializeField] private InputField num1;
    [SerializeField] private InputField num2;*/
    [SerializeField] private TMP_InputField num1;
    [SerializeField] private TMP_InputField num2;

    public void Set(float num1, float num2)
    {
        this.num1.text = num1.ToString();
        this.num2.text = num2.ToString();
    }

    public float Get_num1()
    {
        return float.Parse(num1.text);
    }
    public float Get_num2()
    {
        return float.Parse(num2.text);
    }

}

public class Mob_Test : MonoBehaviour
{
    [SerializeField] Mob_s[] mob;
    [SerializeField] UI_s[] wave;
    [SerializeField] UI_s core;
    [SerializeField] Player_s p;
    [SerializeField] private Pooling[] pool;
    [SerializeField] Button[] button; // 0 : mob , 1 : stage , 2 : applay

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
        p.Setting(player);
        core.Set(C.Get_Max_Hp(), enemy.Get_Max_Hp());
        for(int i = 0; i < mob.Length; i++)
        {
            mob[i].Setting();
            if(i < wave.Length)
            {
                Wave ws = enemy.Get(i);
                wave[i].Set(ws.Count, ws.delay);
            }
        }
        GameObject[] o = GameObject.FindGameObjectsWithTag("Spawner");
        for(int i = 0; i < o.Length; i++)
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
        for(int i = 0; i < pool.Length; i++)
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
            p.apply(ref player);
            C.Set_Hp(core.Get_num1());
            enemy.Set_Hp(core.Get_num2());
            if (i < wave.Length)
            {
                enemy.Set(i, (int) wave[i].Get_num1(), wave[i].Get_num2());
            }
        }
    }
}
