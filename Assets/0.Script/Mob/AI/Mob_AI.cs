using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public enum AI_State
{
    wait, move, Attack, dead, win, Attack_Wait
}

public class Mob_AI : MonoBehaviour
{
    private Mob mob;
    private Animator animator;
    private Rigidbody2D body;
    private Transform target = null;
    [HideInInspector] public GameObject A_Target { get; set; }
    public AI_State state = AI_State.wait;
    [SerializeField] float time;
    [SerializeField] float Dead_Timer;
    private Spine.Skeleton skeleton;
    [SerializeField] SkeletonMecanim SkeletonMecanim;
    IEnumerator coroutine;
    Vector3 dir;
    string ani;
    bool is_dead = false;

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
            if (state == AI_State.move)
            {
                Move();
            }
        }
        //Debug.Log(animator.GetCurrentAnimatorStateInfo(0).IsName(ani));
    }


    public void Set_State(AI_State state)
    {
        if (!is_dead)
        {
            switch (state)
            {
                case AI_State.wait:
                    animator.SetBool("Move", false);
                    animator.SetBool("Attack", false);
                    break;
                case AI_State.move:
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
                    animator.SetBool("Attack", true);
                    animator.SetBool("Move", false);
                    coroutine = Attack_End();
                    StartCoroutine(coroutine);
                    break;
                case AI_State.dead:
                    StartCoroutine(Dead_Ainmation());
                    break;
                case AI_State.win:
                    break;
            }
        }
        this.state = state;
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
        if (Vector3.Distance(target.position, transform.position) >= 1.0f)
        {
            body.MovePosition(transform.position + (dir * mob.Get_Speed()) * Time.deltaTime);
        }
    }


    private Vector3 Check_distance()
    {
        return target.position - transform.position;
    }

    IEnumerator Attack_End()
    {
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName(ani))
        {
            yield return null;
        }
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < animator.GetCurrentAnimatorStateInfo(0).length - time)
        {
            yield return null;
        }
        Set_State(AI_State.wait);
        mob.Attack();
    }

    IEnumerator Dead_Ainmation()
    {
        SpriteRenderer image = GetComponentInChildren<SpriteRenderer>();
        Color color = image.color;

        Set_State(AI_State.wait);
        is_dead = true;
        Debug.Log("코루틴 실행");
        yield return null;
        float t = Dead_Timer;
        while (skeleton.A > 0)
        {
            t -= Time.deltaTime;
            skeleton.A = t / Dead_Timer;
            color.a = t / Dead_Timer;
            image.color = color;
            yield return null;
        }
        mob.Pooling();
    }

}
