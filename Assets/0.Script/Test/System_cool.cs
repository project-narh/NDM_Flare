using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "system_cool", menuName = "Mob/cool", order = int.MaxValue)]
public class System_cool : ScriptableObject
{
    [field: SerializeField] public float Stop { get; set; }
    [field: SerializeField] public float Slow { get; set; }
}
