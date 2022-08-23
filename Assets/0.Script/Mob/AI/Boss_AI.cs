using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_AI : Mob_AI
{
    [SerializeField] public float probability = 50f;
    [SerializeField] public float skill_timer = 0;
    float timer = 0;
    public Boss_AI(string tag):base(tag)
    {

    }

    public virtual void Debuffer()
    {
        use_skill = true;
        animator.SetBool("Attack", false);
        animator.SetBool("Move", false);
        animator.SetTrigger("Debuff");
        StartCoroutine(buff());
        

    }
    protected override void End()
    {
        GameManager.Instance.GameOver();
    }

    private void Update()
    {
        if (state == AI_State.move && !use_skill)
        {
            timer += Time.deltaTime;
            if (timer >= skill_timer)
            {
                Debug.Log("확률 시작");
                timer = 0;
                if(Random.Range(0,101) <= probability)
                {
                    Debug.Log("버프");
                    Debuffer();
                }
            }
            else
            {
                Debug.Log("버프아님");
            }
        }
    }

    IEnumerator buff()
    {
        Debug.Log("버프 시작");
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("ani_Nightmare_basic_debuff"))
        {
            yield return null;
        }
        bool use = false;
        SoundManager.Instance.play_sfx("Nightmare_Debuff", 0.1f);
        while (animator.GetCurrentAnimatorStateInfo(0).IsName("ani_Nightmare_basic_debuff") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f - 0.1f)
        {
            if(!use && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.5f)
            {
                use = true;
                Mob_Storage.Instance.Start_Debuff(DeBuff.Slow);
            }
            yield return null;
        }

        if(is_find)
        {
            Set_State(AI_State.Attack);
        }
        else
        {
            Set_State(AI_State.wait);
        }
        use_skill = false;
    }



}
