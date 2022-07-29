 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawn_UI : MonoBehaviour
{
    GameObject obj;
    public Button mason;
    public Button Shield;

    Player player;
    Spawner core;

    bool is_ma = false, is_sh = false;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        core = GameObject.FindWithTag("F_Core").GetComponent<Spawner>();
    }

    // Start is called before the first frame update
    void Start()
    {
        mason.onClick.AddListener(On_Mason);
        Shield.onClick.AddListener(On_Shield);
    }
    private void On_Mason()
    {
        if (!is_ma)
        {
            if (player.Check_Money(10))
            {
                is_ma = true;
                core.Spawn(0);
                StartCoroutine(Start_Cool(mason, core.Spawn_limit(0), Spawner =>
                {
                    is_ma = false;
                }));
            }
        }
    }
    private void On_Shield()
    {
        if (!is_sh)
        {
            if (player.Check_Money(20))
            {
                is_sh = true;
                core.Spawn(1);
                StartCoroutine(Start_Cool(Shield, core.Spawn_limit(1), Spawner =>
                {
                    is_sh = false;
                }));
            }
        }
    }

    IEnumerator Start_Cool(Button b, float time, System.Action<bool> spawn)
    {
        Image i = b.GetComponent<Image>();
        float t = 0;
        i.fillAmount = t;
        while (t < time)
        {
            yield return new WaitForEndOfFrame();
            t += 0.01f;
            i.fillAmount = t;
        }
        spawn(true);
    }

}
