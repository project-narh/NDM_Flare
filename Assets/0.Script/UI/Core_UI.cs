using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Core_UI : MonoBehaviour
{
    Core team, enemy;
    Canvas canvas;
    [SerializeField] Text t, e;
    [SerializeField] Text wave;
    Vector3 v;

    private void Awake()
    {
        team = GameObject.FindWithTag("F_Core").GetComponent<Core>();
        enemy = GameObject.FindWithTag("E_Core").GetComponent<Core>();
        canvas = GetComponentInParent<Canvas>();
        v.Set(0, team.GetComponent<SpriteRenderer>().bounds.size.y/2 + 0.4f, 0);
    }


    private void LateUpdate()
    {
        HP_Text(ref t,ref team);
        HP_Text(ref e, ref enemy);
        wave.text = "Wave " + (enemy.num + 1);
    }

    private void HP_Text(ref Text text, ref Core core)
    {
        var pos = Camera.main.WorldToScreenPoint(core.transform.position + v);
        text.transform.position = pos;
        text.text = core.Get_Hp() + " / " + core.Get_Max_Hp();
    }

}
