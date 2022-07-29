using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Mob_Stat", menuName = "Mob/stat",order = int.MaxValue)]
public class Mob_Stat : ScriptableObject
{
    [field: SerializeField] public float Max_HP { get; private set; }
    [field: SerializeField] public float HP { get; private set; }
    [field: SerializeField] public float Spawn_Cooltime { get; private set; }
    [field: SerializeField] public float Attack_Cooltime { get; private set; }
    [field: SerializeField] public float Attack_Damage { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }

    public void Set(float Max_HP, float Spawn_Cooltime,float Attack_Cooltime, float Attack_Damage,float Speed)
    {
        this.Max_HP = Max_HP;
        this.Max_HP = Max_HP;
        this.Spawn_Cooltime = Spawn_Cooltime;
        this.Attack_Cooltime = Attack_Cooltime;
        this.Attack_Damage = Attack_Damage;
        this.Speed = Speed;
    }
}
