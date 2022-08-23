using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct UI_Bar
{
    [SerializeField] private Image image;
    [HideInInspector] private Text text;
    [HideInInspector] private float cur;

    public void Init(float Max, float Now)
    {
        text = image.GetComponentInChildren<Text>();
        cur = Now;
        image.fillAmount = Now / Max;
        text.text = Now + " / " + Max;
    }

    public void Set(float Max, float Now, float speed)
    {
        image.fillAmount = Mathf.Lerp(cur, Now/Max, Time.deltaTime * speed);
        cur = image.fillAmount;
        text.text = Now + " / " + Max;
    }

    public Image Get_Image()
    {
        return image;
    }
}

public class Player_UI : MonoBehaviour
{
    [SerializeField] UI_Bar HP;
    [SerializeField] UI_Bar Money;
    [SerializeField] UI_Bar Energy;
    [SerializeField] float Speed;
    [SerializeField] Button Sting;
    [SerializeField] Button_s[] button;

    [SerializeField] float dir;
    [SerializeField] Image hp_back;

    private bool is_mouseOver = false;
    private Camera camera;

    bool is_use = false;

    private Player p;
    Vector3 v;

    private void Awake()
    {
        camera = Camera.main;
        p = GameObject.FindWithTag("Player").GetComponent<Player>();
        v.Set(0,dir,0);
    }
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        HP.Init(p.Get_MaxHP(), p.Get_HP());
        Money.Init(p.Get_MaxMoney(), p.Get_Money());
        Energy.Init(p.Get_MaxEnergy(), p.Get_Energy());
        for (int i = 0; i < button.Length; i++)
        {
            button[i].Init();
        }
        button[0].Get_Button().onClick.AddListener(On_Sting);
        button[1].Get_Button().onClick.AddListener(On_Throw);
        button[2].Get_Button().onClick.AddListener(On_shield);
        button[3].Get_Button().onClick.AddListener(On_ultimate);
    }


    private void Update()
    {
        HP.Set(p.Get_MaxHP(), p.Get_HP(),Speed);
        Money.Set(p.Get_MaxMoney(), p.Get_Money(), Speed);
        Energy.Set(p.Get_MaxEnergy(), p.Get_Energy(), Speed);
        
        if(Input.GetKeyDown(KeyCode.Q))
        {
            On_Sting();
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            On_Throw();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            On_shield();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            On_ultimate();
        }

    }


    private void FixedUpdate()
    {
        var pos = Camera.main.WorldToScreenPoint(p.transform.position + v);
        hp_back.transform.position = pos;
        HP.Get_Image().transform.position = pos;
    }



    private void On_Sting()
    {
        SoundManager.Instance.play_sfx("UI");
        if (!p.use)
        {
            if (!button[0].Get_use())
            {
                if (p.Attack(Player_state.skill1))
                {
                    button[0].Set_use(true);
                    StartCoroutine(Start_Cool(button[0], p.Get_Attack_Cool(Player_state.skill1), skill =>
                    {
                        button[0].Set_use(false);
                    }));
                }
            }
        }
    }

    private void On_Throw()
    {
        SoundManager.Instance.play_sfx("UI");
        if (!p.use)
        {
            if (!button[1].Get_use())
            {
                if (p.Attack(Player_state.skill2))
                {
                    button[1].Set_use(true);
                    StartCoroutine(Start_Cool(button[1], p.Get_Attack_Cool(Player_state.skill2), skill =>
                    {
                        button[1].Set_use(false);
                    }));
                }
            }
        }
    }

    private void On_shield()
    {
        SoundManager.Instance.play_sfx("UI");
        if (!p.use)
        {
            if (!button[2].Get_use())
            {
                if (p.Attack(Player_state.skill3))
                {
                    button[2].Set_use(true);
                    StartCoroutine(Start_Cool(button[2], p.Get_Attack_Cool(Player_state.skill3), skill =>
                    {
                        button[2].Set_use(false);
                    }));
                }
            }
        }
    }

    private void On_ultimate()
    {
        SoundManager.Instance.play_sfx("UI");
        if (!p.use)
        {
            if (!button[3].Get_use())
            {
                if (p.Attack(Player_state.ultimate))
                {
                    StartCoroutine(Shake(1, 0.08f));
                    button[3].Set_use(true);
                    StartCoroutine(Start_Cool(button[3], p.Get_Attack_Cool(Player_state.ultimate), skill =>
                    {
                        button[3].Set_use(false);
                    }));
                }
            }
        }
    }

    /*    IEnumerator Start_Cool(Button b, float time)
        {
            Image i = b.GetComponent<Image>();
            float t = 0;
            i.fillAmount = t;
            while(t < time)
            {
                yield return new WaitForEndOfFrame();
                t += 0.01f;
                i.fillAmount = t;
            }
            is_use = false;
        }
    */
    IEnumerator Start_Cool(Button_s b, float time, System.Action<bool> skill)
    {
        Image i = b.Get_Button().GetComponent<Image>();
        float t = 0;
        b.Active_Text(true);
        i.fillAmount = t;
        while (t < time)
        {
            yield return new WaitForEndOfFrame();
            t += Time.deltaTime;
            b.Set_Text(time - t);
            i.fillAmount = t / time;
        }
        skill(true);
        b.Active_Text(false);
    }

    IEnumerator Shake(float timer,float range)
    {
        Vector3 vector = Camera.main.transform.position;
        float t = 0;
        while(t < timer)
        {
            Camera.main.transform.localPosition = Random.insideUnitSphere * range + vector;
            t+=Time.deltaTime;
            Mob_Storage.Instance.Enemy_kill();
            yield return null;
        }
        Camera.main.transform.localPosition = vector;
    }
}
