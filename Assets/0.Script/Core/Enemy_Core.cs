using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Wave
{
    [SerializeField] int min;
    [SerializeField] int max;
    [field: SerializeField] public int Count { get; private set; }
    [field: SerializeField] public float delay { get; private set; }

    public int Spawn_number()
    {
        int n = Random.Range(min, max + 1);
        return n;
    }

    public int Spawn_Count()
    {
        return Count;
    }

    public float Wave_delay()
    {
        return delay;
    }

    public void Set(int count,float delay)
    {
        this.Count = count;
        this.delay = delay;
    }

}

public class Enemy_Core : Core
{
    [SerializeField] private Wave[] wave;
    Coroutine co;
    int number = 0;

    protected override void Dead()
    {
        GameManager.Instance.Win();
    }

    protected override void Init()
    {
        SoundManager.Instance.play_BGM(GameManager.Instance.Get_Scene());
        UIManager.Instance.Check();
        co = StartCoroutine(wave_spawn(wave));
        //SoundManager.Instance.play_BGM(GameManager.Instance.Scene_Name());
    }

    public void Spawn(string name)
    {
        spawner.Spawn(name);
    }

    IEnumerator wave_spawn(Wave[] wave)
    {
        for (int i = 0; i < wave.Length; i++)
        {
            number++;
            num = i;
/*            Debug.Log("===========[Stage]===========");
            Debug.Log("Monster : " + wave[i].Count);
            Debug.Log("delay : " + wave[i].delay);
            Debug.Log("=============================");*/
            for (int j = 0; j < wave[i].Spawn_Count(); j++)
            {
                Spawn_e(wave[i].Spawn_number());
                yield return new WaitUntil(() => spawner.Get_spawn());
            }
            yield return new WaitForSeconds(wave[i].Wave_delay());
        }
        co = StartCoroutine(wave_spawn(wave));
    }
    public void Spawn_boss()
    {
        Spawn_e(4);
    }
    public Wave Get(int i)
    {
        return wave[i];
    }

    public float Get_wave()
    {
        return number;
    }

    public void Set(int i, int count, float delay)
    {
        wave[i].Set(count,delay);
    }

    public void Stage_clear()
    {
        Debug.Log("스테이지 초기화");
        StopCoroutine(co);
        co = StartCoroutine(wave_spawn(wave));
    }

    public override void Spawn_Gollem()
    {
        Spawn_e(3);
    }
}
