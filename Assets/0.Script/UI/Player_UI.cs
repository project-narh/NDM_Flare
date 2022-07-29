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
}

public class Player_UI : MonoBehaviour
{
    [SerializeField] UI_Bar HP;
    [SerializeField] UI_Bar Money;
    [SerializeField] UI_Bar Energy;
    [SerializeField] float Speed;
    [SerializeField] Button Sting;

    bool is_use = false;

    private Player p;

    private void Awake()
    {
        p = GameObject.FindWithTag("Player").GetComponent<Player>();
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
        if (Sting != null) Sting.onClick.AddListener(On_Sting);
    }


    private void Update()
    {
        HP.Set(p.Get_MaxHP(), p.Get_HP(),Speed);
        Money.Set(p.Get_MaxMoney(), p.Get_Money(), Speed);
        Energy.Set(p.Get_MaxEnergy(), p.Get_Energy(), Speed);
    }

    private void On_Sting()
    {
        if (!is_use)
        {
            if (p.Attack())
            {
                is_use = true;
                StartCoroutine(Start_Cool(Sting,p.Get_Attack_Cool()));
            }
        }
    }

    IEnumerator Start_Cool(Button b, float time)
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
}
