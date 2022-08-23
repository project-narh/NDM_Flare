using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team_Core : Core
{
    public override void Spawn_Gollem()
    {
        spawner.Spawn(3);
    }
}
