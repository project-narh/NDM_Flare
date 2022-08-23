using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public struct S_Mob
{
    public string name;
    public GameObject mob;

}

public class Spawner : MonoBehaviour
{
    [SerializeField] private S_Mob[] s_mob;
    [SerializeField] private float Spawn_Cooltime;
    Transform path;
    Pooling[] pool;
    private bool is_spawn = true;
    Vector2 limit;

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        
    }
    private void Init()
    {
        path = transform.GetChild(0);
        pool = new Pooling[s_mob.Length];
        DistanceJoint2D dis = GameObject.FindWithTag("Distance").GetComponent<DistanceJoint2D>();
        limit.x = dis.transform.position.y;
        limit.y = limit.x + dis.distance;
        pool_in();
    }

    public void Spawn(string name)
    {
        if (is_spawn)
        {
            for (int i = 0; i < s_mob.Length; i++)
            {
                if (s_mob[i].name.Equals(name))
                {
                    Instantiate(s_mob[i].mob,transform.position,Quaternion.identity);
                    is_spawn = false;
                    StartCoroutine(Spawn_Cool());
                    break;
                }
            }
        }
    }

/*    public void Spawn(int n)
    {
        if (is_spawn)
        {
            if (s_mob[n].mob != null)
            {
                GameObject mob;
                Vector3 pos = transform.position;
                pos.y = Random.Range(limit.x, limit.y + 1);
                mob = pool[n].Active(pos);
                is_spawn = false;
                StartCoroutine(Spawn_Cool());
            }
        }
    }*/

    public GameObject Spawn(int n)
    {
        if (is_spawn)
        {
            if (s_mob[n].mob != null)
            {
                GameObject mob;
                Vector3 pos = transform.position;
                pos.y = Random.Range(limit.x, limit.y + 1);
                mob = pool[n].Active(pos);
                is_spawn = false;
                StartCoroutine(Spawn_Cool());
                return mob;
            }
        }
        return null;
    }

    public float Spawn_limit(int n)
    {
        if (s_mob[n].mob != null)
        {
           return s_mob[n].mob.GetComponent<Mob>().Get_spawn_c();
        }
        return 0;
    }


    public bool Get_spawn()
    {
        return is_spawn;
    }
    public void Spawn_cancel()
    {
        StopCoroutine(Spawn_Cool());
        is_spawn = true;
    }

    IEnumerator Spawn_Cool()
    {
        yield return new WaitForSeconds(Spawn_Cooltime);
        is_spawn=true;
    }

    private Transform Check_file(string name)
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).name.Equals(name))
            {
                return transform.GetChild(i);
            }
        }
        
        GameObject empty = new GameObject(name);
        return empty.transform;
    }

    private void pool_in()
    {
        for(int i = 0; i < s_mob.Length; i++ )
        {
            GameObject empty = new GameObject(s_mob[i].name);
            empty.transform.SetParent(path);
            pool[i] = empty.AddComponent<Pooling>();
            empty.tag = "Spawner";
            pool[i].Init(s_mob[i].mob, empty.transform); 
        }
    }
}