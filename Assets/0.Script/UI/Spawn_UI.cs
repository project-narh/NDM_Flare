 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Button_s
{
    [SerializeField] private Button button;
    private Text text;
    bool is_use;

    public void Init()
    {
        text = button.GetComponentInChildren<Text>();
        Active_Text(false);
    }

    public void Set_Text(float t)
    {
        text.text = string.Format("{0:f1}", t);
    }

    public Button Get_Button()
    {
        return button;
    }
    
    public void Set_use(bool use)
    {
        is_use = use;
    }

    public void Active_Text(bool use)
    {
        text.gameObject.SetActive(use);
    }

    public bool Get_use()
    {
        return is_use;
    }
}

public class Spawn_UI : MonoBehaviour
{
    [SerializeField] private Button_s[] button;
    [SerializeField] private bool is_Lock = false;
    Player player;
    Spawner core;


    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        core = GameObject.FindWithTag("F_Core").GetComponent<Spawner>();
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < button.Length; i++)
        {
            button[i].Init();
        }

        button[0].Get_Button().onClick.AddListener(On_Mason);
        button[1].Get_Button().onClick.AddListener(On_Shield);
        button[2].Get_Button().onClick.AddListener(On_Cannon);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            On_Mason();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            On_Shield();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            On_Cannon();
        }
    }

    private void On_Mason()
    {
        SoundManager.Instance.play_sfx("UI");
        if (!button[0].Get_use())
        {
            if (player.Check_Money(10))
            {
                button[0].Set_use(true);
                Mob_Storage.Instance.Add_Mob(core.Spawn(0).GetComponent<Mob>(), true);
                StartCoroutine(Start_Cool(button[0], core.Spawn_limit(0), Spawner =>
                {
                    button[0].Set_use(false);
                }));
            }
        }
    }
    private void On_Shield()
    {
        SoundManager.Instance.play_sfx("UI");
        if (!button[1].Get_use())
        {
            if (player.Check_Money(20))
            {
                button[1].Set_use(true);
                Mob_Storage.Instance.Add_Mob(core.Spawn(1).GetComponent<Mob>(), true);
                StartCoroutine(Start_Cool(button[1], core.Spawn_limit(1), Spawner =>
                {
                    button[1].Set_use(false);
                }));
            }
        }
    }

    private void On_Cannon()
    {
        SoundManager.Instance.play_sfx("UI");
        if (!is_Lock)
        {
            if (!button[2].Get_use())
            {
                if (player.Check_Money(30))
                {
                    button[2].Set_use(true);
                    Mob_Storage.Instance.Add_Mob(core.Spawn(2).GetComponent<Mob>(), true);
                    StartCoroutine(Start_Cool(button[2], core.Spawn_limit(2), Spawner =>
                    {
                        button[2].Set_use(false);
                    }));
                }
            }
        }
    }

    IEnumerator Start_Cool(Button_s b, float time, System.Action<bool> spawn)
    {
        Image i = b.Get_Button().GetComponent<Image>();
        float t = 0;
        b.Active_Text(true);
        i.fillAmount = t;
        while (t < time)
        {
            yield return new WaitForEndOfFrame();
            //t += 0.01f;
            t += Time.deltaTime;
            b.Set_Text(time - t);
            i.fillAmount = t;
        }
        spawn(true);
        b.Active_Text(false);
    }

}
