using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Core_UI : MonoBehaviour
{
    Core team, enemy;
    
    Canvas canvas;
    //[SerializeField] Text t, e;
    [SerializeField] TextMeshProUGUI t, e;
    [SerializeField] Text wave;
    Text t_line, e_line;
    [SerializeField] float column;
    [SerializeField] float row;
    [SerializeField] float column2;
    [SerializeField] float row2;
    [SerializeField] SpriteRenderer spriteRenderer_T;
    [SerializeField] SpriteRenderer spriteRenderer_E;
    [SerializeField] bool is_ = true;


    Vector3 v,v2;

    private void Awake()
    {
        team = GameObject.FindWithTag("F_Core").GetComponent<Core>();
        enemy = GameObject.FindWithTag("E_Core").GetComponent<Core>();
        canvas = GetComponentInParent<Canvas>();
        if (!is_)
        {
            v.Set(row, spriteRenderer_T.bounds.size.y / 2 + column, 0);
            v2.Set(row2, spriteRenderer_E.bounds.size.y / 2 + column2, 0);
        }
        else
        {
            v.Set(row, team.GetComponent<SpriteRenderer>().bounds.size.y / 2 + column, 0);
            v2.Set(row2, enemy.GetComponent<SpriteRenderer>().bounds.size.y / 2 + column2, 0);
        }
    }


    private void FixedUpdate()
    {
        HP_Text(ref t,ref team, true);
        HP_Text(ref e, ref enemy, false);
        wave.text = "Wave " + (enemy.num + 1);
    }

    private void HP_Text(ref TextMeshProUGUI text, ref Core core, bool left)
    {
        Vector3 pos;
        if (left)
        {
            pos = Camera.main.WorldToScreenPoint(core.transform.position + v);
        }
        else
        {
            pos = Camera.main.WorldToScreenPoint(core.transform.position + v2);
        }

        text.transform.position = pos;
        text.text = core.Get_Hp() + " / " + core.Get_Max_Hp();
    }

}
