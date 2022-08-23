using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Debuff
{

}

public class Timer : MonoBehaviour
{
    [Header("제한시간 타이머")]
    [SerializeField] private float limit_timer;
    [SerializeField] private bool is_Use = true;
    [SerializeField] private Image image;

    private float n_timer = 0; // 제한시간 타이머

    private void Awake()
    {
        n_timer = 0;
        is_Use = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (is_Use)
        {
            if(is_check(ref n_timer,limit_timer))
            {
                is_Use = false;
            }
        }
    }

    private bool is_check(ref float t, float ti)
    {
        t += Time.deltaTime;
        if (t >= ti)
        {
            StartCoroutine(Stop());
            return true;
        }
        return false;
    }

    private IEnumerator Stop()
    {
        Color color = image.color;
        color.a = 0;
        image.color = color;
        image.gameObject.SetActive(true);
        float timer = 0;
        while(timer < 1f)
        {
            Debug.Log(timer);
            color = image.color;
            timer += Time.deltaTime;
            color.a = timer;
            image.color= color;
            yield return null;
        }
        Mob_Storage.Instance.Start_Debuff(DeBuff.stiffness);
        yield return new WaitForSeconds(1f);
        while (0f < timer)
        {
            color = image.color;
            timer -= Time.deltaTime;
            color.a = timer;
            image.color = color;
            yield return null;
        }

        color = image.color;
        color.a = 0;
        image.color = color;

        image.gameObject.SetActive(false);
    }

}
