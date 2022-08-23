using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public enum AI_State
{
    wait, move, Attack, dead, win, System_wait, debuff
}

public enum DeBuff
{
    stiffness, Slow, None
}

public class Mob_AI : MonoBehaviour
{
    private Mob mob;
    protected Animator animator;
    private Rigidbody2D body;
    private Transform target = null;
    [HideInInspector] public GameObject A_Target { get; set; }
    public AI_State state = AI_State.wait;
    [SerializeField] float time;
    [SerializeField] float Dead_Timer;
    [SerializeField] System_cool cool;
    private Spine.Skeleton skeleton;
    [SerializeField] SkeletonMecanim SkeletonMecanim;
    IEnumerator coroutine;
    Vector3 dir;
    string ani;
    [SerializeField] float sfx_timer;
    protected bool use_skill = false;
    bool is_dead = false;
    bool is_System = false;
    bool is_Attack = false;
    bool is_Slow = false;
    [HideInInspector] public bool is_find { get; set; }
    Coroutine slow, stop;
    float timer1 = 0, timer2 = 0;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        skeleton = SkeletonMecanim.Skeleton;
    }


    public Mob_AI(string tag)
    {
        this.target = GameObject.FindWithTag(tag).transform;

    }
    public void Init(Mob mob, string tag)
    {
        this.mob = mob;
        this.target = GameObject.FindWithTag(tag).transform;
        dir = Check_distance().normalized;
        dir.y = 0;
        if (target != null)
            Set_State(AI_State.move);
    }

    private void OnEnable()
    {
        is_Attack = false;
        is_dead = false;
        if (target != null)
            Set_State(AI_State.move);
        skeleton.A = 1;
    }

    private void Start()
    {
        string s = animator.runtimeAnimatorController.name;
        ani = "ani_" + s.Substring(0, s.IndexOf('_')) + "_basic_attack";
    }


    private void FixedUpdate()
    {
        if(!is_dead)
        {
            if (state == AI_State.move && !use_skill)
            {
                Move();
            }
        }
    }

    public void Set_Debuff(DeBuff buff)
    {
        switch (buff)
        {
            case DeBuff.stiffness:
                Debug.Log("경직 적용");
                if (slow != null)
                {
                    timer2 = 0;
                }
                else
                {
                    stop = StartCoroutine(Stop_Cool());
                }
                break;
            case DeBuff.Slow:
                Debug.Log("느림 적용");
                if (slow != null)
                {
                    timer1 = 0;
                }
                else
                {
                    timer1 = 0;
                    slow = StartCoroutine(Slow_Cool());
                }
                break;
        }

    }


    public void Set_State(AI_State state)
    {
        if (!is_dead)
        {
            if (!is_System)
            {
                switch (state)
                {
                    case AI_State.System_wait:
                        is_System = true;
                        animator.SetBool("Move", false);
                        animator.SetBool("Attack", false);
                        break;

                    case AI_State.wait:
                        animator.SetBool("Move", false);
                        animator.SetBool("Attack", false);
                        break;
                    case AI_State.move:
                        is_Attack = false;
                        animator.SetBool("Move", true);
                        animator.SetBool("Attack", false);
                        if (coroutine != null)
                        {
                            StopCoroutine(coroutine);
                            coroutine = null;
                            mob.Attack_cancel();
                        }
                        break;
                    case AI_State.Attack:
                        is_Attack = true;
                        animator.SetBool("Attack", true);
                        animator.SetBool("Move", false);
                        coroutine = Attack_End();
                        StartCoroutine(coroutine);
                        break;
                    case AI_State.dead:
                        SoundManager.Instance.play_sfx(mob.Get_name() + "_Death");
                        End();
                        StartCoroutine(Dead_Ainmation());
                        break;
                    case AI_State.win:
                        break;
                }
            }
        }
        this.state = state;
    }

    protected virtual void End()
    {

    }

    public void Set_State(bool system)
    {
        if (system)
        {
            Set_State(AI_State.System_wait);
            return;
        }
        is_System = false;
        if (is_Attack)
            Set_State(AI_State.Attack);
        else
        Set_State(AI_State.move);
    }

    public void Set_Target(Transform t)
    {
        target = t;
        Set_State(AI_State.move);
    }
    
    public float Get_Damage()
    {
        return mob.Get_Attack_Damage();
    }

    private void Move()
    {
        //if (Vector3.Distance(target.position, transform.position) >= 1.0f)
        {
            body.MovePosition(transform.position + (dir * Get_Speed()) * Time.deltaTime);
        }
    }

    private float Get_Speed()
    {

        if(is_Slow)
        {
            return (mob.Get_Speed() / 2);
        }
        else if(is_System)
        {
            return 0;
        }
        return mob.Get_Speed();
    }

    private Vector3 Check_distance()
    {
        return target.position - transform.position;
    }

    IEnumerator Attack_End()
    {
        SoundManager.Instance.play_sfx(mob.Get_name() + "_Attack", sfx_timer);
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName(ani))
        {
            yield return null;
        }
        //while (animator.GetCurrentAnimatorStateInfo(0).IsName(ani) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < animator.GetCurrentAnimatorStateInfo(0).length - time)
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f - time)
        {
            yield return null;
        }
        Set_State(AI_State.wait);
        mob.Attack();
    }

    IEnumerator Dead_Ainmation()
    {
        SpriteRenderer image = GetComponentInChildren<SpriteRenderer>();
       // Color color = image.color;

        Set_State(AI_State.wait);
        is_dead = true;
        yield return null;
        float t = Dead_Timer;
        while (skeleton.A > 0)
        {
            t -= Time.deltaTime;
            skeleton.A = t / Dead_Timer;
            //color.a = t / Dead_Timer;
            //image.color = color;
            yield return null;
        }
        skeleton.A = 0;
        mob.Pooling();
    }

    IEnumerator Slow_Cool()
    {
        is_Slow = true;
        while(timer1 < cool.Slow)
        {
            timer1 += Time.deltaTime;
            yield return null;
        }
        is_Slow = false;
        slow = null;
    }

    IEnumerator Stop_Cool()
    {
        Set_State(true);
        while (timer2 < cool.Stop)
        {
            timer2 += Time.deltaTime;
            yield return null;
        }
        Set_State(false);
        stop = null;
    }


}
