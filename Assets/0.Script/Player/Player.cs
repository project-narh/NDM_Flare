using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float Money = 0;
    [SerializeField] private float Energy = 0;
    [SerializeField] private float HP;
    [field: SerializeField] public float MAX_HP { get; private set; }
    [field: SerializeField] public float Attack_Damage { get; private set; }
    [field: SerializeField] public float Attack_Cooltime { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }
    [SerializeField] private float limit_dir;
    [SerializeField] private SpriteRenderer sprite;
    private Animator animator;
    private Rigidbody2D body;
    float dir;
    float v;
    private bool is_check = false , is_wait = false, is_dead = false;
    private int num = 0;
    float width;
    float Timer = 0;
    private List<GameObject> list;

    private void Awake()
    {
        list = new List<GameObject>();
    }

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
    }

    public bool Is_Attack()
    {
        return animator.GetBool("Attack");
    }

    public void AddDamage(float Damage)
    {
        HP -= Damage;
        if(HP <= 0)
        {
            Dead();
        }
    }

    private void Update()
    {
        if(GameManager.Instance.is_Game)
        {
            Resource_Update();
            Move_Update();
            Attack_Update();
        }
    }

    public void Set(float MAX_HP, float Attack_Damage, float Attack_Cooltime, float Speed)
    {
        this.MAX_HP = MAX_HP;
        this.HP = MAX_HP;
        this.Attack_Damage = Attack_Damage;
        this.Attack_Cooltime = Attack_Cooltime;
        this.Speed = Speed;
    }

    private void FixedUpdate()
    {
         body.velocity = new Vector2(v * Speed, body.velocity.y);
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

        if (!animator.GetBool("Attack"))
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    public bool Attack()
    {
        if (!is_wait)
        {
            if (Resource_Check(ref Energy, 15))
            {
                animator.SetBool("Attack",true);
                animator.SetBool("Move", false);
                StartCoroutine(Attack_check());
                return true;
            }
        }
        return false;
    }

    public void Dead()
    {
        animator.SetTrigger("Death");
        GameManager.Instance.is_Game = false;
        GameManager.Instance.GameOver();

    }

    public float Get_Damage()
    {
        return Attack_Damage;
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
        is_wait = true;
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("ani_meogun_basic_skill1")){ yield return null; }
        while(animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            yield return null;
        }
        if (Input.GetAxis("Horizontal") != 0)
        {
            animator.SetBool("Move", true);
        }
        animator.SetBool("Attack", false);
        yield return new WaitForSeconds(Attack_Cooltime);
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

    public float Get_Attack_Cool()
    {
        return Attack_Cooltime;
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
