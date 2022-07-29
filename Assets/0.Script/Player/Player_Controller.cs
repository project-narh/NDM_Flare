using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [SerializeField] private float Coin;
    [SerializeField] private float HP;
    [SerializeField] private float MAX_HP;
    [SerializeField] private float Attack_Damage;
    [SerializeField] private float Attack_Cooltime;
    [SerializeField] private float Speed;
    [SerializeField] private float limit_dir;
    [SerializeField] private SpriteRenderer sprite;
    private Animator animator;
    private Rigidbody2D body;
    float dir;
    float v;
    private bool is_check = false, is_wait = false;
    private int num = 0;
    float width;

    private void Start()
    {
        HP = MAX_HP;
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        dir = 0;
        v = 0;
        Init();
    }

    private void Init()
    {
        this.gameObject.tag = "Friend";
        transform.GetChild(0).tag = "Friend_Attack";
        width = sprite.bounds.size.x / 2;

    }

    public bool Is_Attack()
    {
        return animator.GetBool("Attack");
    }

    public void AddDamage(float Damage)
    {
        HP -= Damage;
        if (HP <= 0)
        {
            Dead();
        }
    }

    private void Update()
    {
        if (!animator.GetBool("Attack"))
        {

            v = Input.GetAxis("Horizontal");
            if (is_check)
            {
                if (v > 0) v = 0;
            }

            if (v < 0 && dir != -1) // ¿ÞÂÊ
            {
                if (!animator.GetBool("Move"))
                {
                    animator.SetBool("Move", true);
                }
                dir = -1;
                transform.localScale = new Vector3(-0.19f, 0.19f, 0.19f);
            }
            else if (v > 0 && dir != 1) // ¿À¸¥ÂÊ
            {
                if (!animator.GetBool("Move"))
                {
                    animator.SetBool("Move", true);
                }
                dir = 1;
                transform.localScale = new Vector3(0.19f, 0.19f, 0.19f);
            }
            else if (v == 0 && dir != 0) // Á¤Áö
            {
                dir = 0;
                animator.SetBool("Move", false);
            }
        }
        else
        {
            v = 0;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (!is_wait)
            {
                animator.SetBool("Attack", true);
                //StartCoroutine(Attack_Cool());
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetBool("Attack", false);
        }

    }

    private void FixedUpdate()
    {
        body.velocity = new Vector2(v * Speed, body.velocity.y);

    }

    public void Dead()
    {
        Debug.Log("À¸¾Ó Áê±Ý");
    }

    public float Get_Damage()
    {
        return Attack_Damage;
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            if (!is_check && num == 0)
                is_check = true;
            num++;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            if (--num <= 0)
                is_check = false;
        }
    }

    IEnumerator Attack_Cool()
    {
        is_wait = true;
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
}
