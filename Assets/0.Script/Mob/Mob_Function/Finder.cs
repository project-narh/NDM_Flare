using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finder : P_Finder
{
    protected Mob_AI ai;

    protected override void Init()
    {
        ai = GetComponentInParent<Mob_AI>();
        my_tag = ai.tag;
    }
}
