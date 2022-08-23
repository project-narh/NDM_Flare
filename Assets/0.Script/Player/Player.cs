using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Player_state
{
    None, skill1, skill2, skill3, ultimate, skill3_move
}

[System.Serializable]
public struct Skill
{
    [field: SerializeField] public Player_state state { get; set; }
    [field: SerializeField] public bool _iswait { get; set; }
    [field: SerializeField] public float Cooltime { get; private set; }
    [field: SerializeField] public float Damage { get; set; }

}

public class Player : MonoBehaviour
{
    [SerializeField] private float Money = 0;
    [SerializeField] private float Energy = 0;
    [SerializeField] private float HP;
    [field: SerializeField] public float MAX_HP { get; private set; }

    [SerializeField] public Skill[] skill;
    [field: SerializeField] public float Speed { get; private set; }
    [SerializeField] private float limit_dir;
    [Header("스킬 공격력")]
    [SerializeField] private SpriteRenderer sprite;
    private Animator animator;
    private Rigidbody2D body;
    float dir;
    float v;
    private bool is_check = false , is_wait = false, is_dead = false;
    private int _isAttack = 0;
    private Player_state state = Player_state.None;
    private int num = 0;
    float width;
    float Timer = 0;
    private List<GameObject> list;
    private Animator effect;
    public bool use = false;

    private void Awake()
    {
        list = new List<GameObject>();
    }

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        effect = transform.GetChild(2).GetComponent<Animator>();
        Init();
    }

    private void Init()
    {
        this.gameObject.tag = "Friend";
        transform.GetChild(0).tag = "Friend_Attack";
        width = sprite.bounds.size.x / 2;
        dir = 0;
        v = 0;
        HP = MAX_HP;
        effect.gameObject.SetActive(false);
    }

    public bool Is_Attack()
    {
        //return animator.GetBool("Attack");
        if(!(state == Player_state.None || state == Player_state.skill3_move)) return true;
        return false;

    }

    public void AddDamage(float Damage)
    {
        if (state != Player_state.skill3_move)
        {
            HP -= Damage;
            if (HP <= 0)
            {
                Dead();
            }
        }
    }

    private void Update()
    {
        if(GameManager.Instance == null)
        {
            Resource_Update();
            Move_Update();
            Attack_Update();
        }
        else if(GameManager.Instance.is_Game)
        {
            Resource_Update();
            Move_Update();
            Attack_Update();
        }
        //Debug.Log(state);
    }

    public void Set(float MAX_HP, float Speed, float skill1, float skill2)
    {
        this.MAX_HP = MAX_HP;
        this.HP = MAX_HP;
        this.Speed = Speed;
        this.skill[0].Damage = skill1;
        this.skill[1].Damage = skill2;
    }

    private void FixedUpdate()
    {
        if (!Is_Attack())
        {
            body.velocity = new Vector2(v * Speed, body.velocity.y);
        }
        else
        {
            body.velocity = new Vector2(0, body.velocity.y);
        }

        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < 0f) pos.x = 0f;
        else if (pos.x > 1f) pos.x = 1f;
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    private bool check_mob()
    {
        if (list.Count > 0)
        {
            for(int i = list.Count - 1; i >=0; i--)
            {
                Vector3 t = transform.position - list[i].transform.position;
                BoxCollider2D col;
                bool is_ = list[i].TryGetComponent<BoxCollider2D>(out col);
                if (is_)
                {
                    /*float pos = col.size.x + col.offset.x;
                    if (Mathf.Abs(t.x) > pos)
                    {
                        Remove(list[i]);
                    }
                    else*/
                    {
                        if (!list[i].active)
                        {
                            Remove(list[i]);
                        }
                    }
                }
                else
                {
                    Remove(list[i]);
                }
            }

            if (list.Count > 0)
            {
                return true;
            }
        }
        return false;
    }
    private void Remove(GameObject n)
    {
        list.Remove(n);
    }
    private void Move_Update()
    {

        is_check = check_mob();

        if (!Is_Attack())
        {
            v = Input.GetAxis("Horizontal");
            if (v < 0 && dir != -1) // 왼쪽
            {
                if (!animator.GetBool("Move"))
                {
                    animator.SetBool("Move", true);
                }
                dir = -1;
                transform.localScale = new Vector3(-0.19f, 0.19f, 0.19f);
            }
            else if (v > 0 && dir != 1) // 오른쪽
            {
                if (!animator.GetBool("Move"))
                {
                    animator.SetBool("Move", true);
                }
                dir = 1;
                transform.localScale = new Vector3(0.19f, 0.19f, 0.19f);
            }
            else if (v == 0 && dir != 0) // 정지
            {
                dir = 0;
                animator.SetBool("Move", false);
            }

            if (is_check)
            {
                if (v > 0) v = 0;
            }
        }
        else
        {
            v = 0;
        }
    }

    private void Attack_Update()
    {
    }

    public bool Attack(Player_state ps)
    {
        if (!Is_Attack())
        {
            v = 0;
            switch (ps)
            {
                case Player_state.skill1:
                    if (Resource_Check(ref Energy, 10))
                    {
                        //animator.SetTrigger("skill1");
                        state = ps;
                        animator.SetBool("skill1", true);
                        animator.SetBool("Move", false);
                        SoundManager.Instance.play_sfx("Player_Skill1",0.2f);
                        StartCoroutine(Attack_check());
                        return true;
                    }
                    break;

                case Player_state.skill2:
                    if (Resource_Check(ref Energy, 20))
                    {
                        state = ps;
                        //animator.SetTrigger("skill2");
                        animator.SetBool("skill2", true);
                        animator.SetBool("Move", false);
                        SoundManager.Instance.play_sfx("Player_Skill2", 0.35f);
                        StartCoroutine(Attack_check());
                        return true;
                    }
                    break;

                case Player_state.skill3:
                    if (Resource_Check(ref Energy, 30))
                    {
                        state = ps;
                        animator.SetTrigger("skill3");
                        //animator.SetBool("skill3", true);

                        animator.SetBool("Move", false);
                        SoundManager.Instance.play_sfx("Player_Skill3");
                        StartCoroutine(Attack_check());
                        return true;
                    }
                    break;

                case Player_state.ultimate:
                    if (Resource_Check(ref Energy, 50))
                    {
                        state = ps;
                        //animator.SetTrigger("ultimate");
                        animator.SetBool("ultimate", true);
                        animator.SetBool("Move", false);
                        SoundManager.Instance.play_sfx("Player_Skill4");
                        StartCoroutine(Attack_check());
                        return true;
                    }
                    break;
            }
        }
        return false;
    }

    public void Dead()
    {
        SoundManager.Instance.play_sfx("Player_Death");
        animator.SetTrigger("Death");
        GameManager.Instance.is_Game = false;
        GameManager.Instance.GameOver();

    }

    public float Get_Damage()
    {
        for(int i = 0; i < skill.Length;i++)
        {
            if (skill[i].state == state)
            {
                return skill[i].Damage;
            }
        }
        return 0;
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Enemy") || col.CompareTag("E_Core"))
        {
            if(list.Count <= 0)
                is_check = true;
            list.Add(col.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Enemy") || col.CompareTag("E_Core"))
        {
            list.Remove(col.gameObject);
            if (list.Count == 0)
                is_check = false;
        }
    }

    IEnumerator Attack_check()
    {
        use = true;
        is_wait = true;
        string str = "ani_meogun_basic_" + state.ToString();
        Debug.Log(str);
        if (state != Player_state.skill3)
        {
            while (!animator.GetCurrentAnimatorStateInfo(0).IsName(str))
            {
                yield return null;
            }
            while (animator.GetCurrentAnimatorStateInfo(0).IsName(str) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            {
                yield return null;
            }
        }
        else
        {
            while (!animator.GetCurrentAnimatorStateInfo(0).IsName(str))
            {
                yield return null;
            }
            while (animator.GetCurrentAnimatorStateInfo(0).IsName(str) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            {
                if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f)
                {
                    effect.gameObject.SetActive(true);
                    effect.Play("PowerOverwhelming", -1, 0f);
                    state = Player_state.skill3_move;
                    break;
                }
                yield return null;
            }
            string str2 = "PowerOverwhelming";
            while (!effect.GetCurrentAnimatorStateInfo(0).IsName(str2))
            {
                yield return null;
            }
            while (effect.GetCurrentAnimatorStateInfo(0).IsName(str2) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            {
                yield return null;
            }
            effect.gameObject.SetActive(false);
        }
        Player_state ps = state;
        use = false;

            state = Player_state.None;
            if (Input.GetAxis("Horizontal") != 0)
            {
                animator.SetBool("Move", true);
            }
            animator.SetBool(ps.ToString(), false);

        float cool = 0;
        for(int i = 0; i < skill.Length;  i++)
        {
            if (skill[i].state == state)
            {
                cool = skill[i].Damage;
            }
        }
        yield return new WaitForSeconds(cool);
        is_wait = false;
    }

    public float Get_MaxHP()
    {
        return MAX_HP;
    }
    public float Get_HP()
    {
        return HP;
    }

    public float Get_MaxMoney()
    {
        return 60;
    }
    public float Get_Money()
    {
        return Money;
    }

    public float Get_MaxEnergy()
    {
        return 60;
    }
    public float Get_Energy()
    {
        return Energy;
    }

    public float Get_Attack_Cool(Player_state ps)
    {
        for(int i=0; i < skill.Length; i++)
        {
            if (skill[i].state == ps)
            {
                return skill[i].Cooltime;
            }
        }
        return 0;
    }

    private void Resource_UP(ref float Resources)
    {
        if(Resources < 60)
        {
            Resources += 4;
            if (Resources > 60) Resources = 60;
        }
    }

    private bool Resource_Check(ref float Resources, float num)
    {
        if (Resources >= num)
        {
            Resources -= num;
            return true;
        }
        return false;
    }

    public bool Check_Money(float num)
    {
        return Resource_Check(ref Money, num);
    }

    private void Resource_Update()
    {
        Timer += Time.deltaTime;
        if(Timer >= 1f)
        {
            Timer = 0;
            Resource_UP(ref Money);
            Resource_UP(ref Energy);
        }
    }


}
