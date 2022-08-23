using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Pooling : MonoBehaviour
{
    private GameObject obj;
    private Queue<GameObject> queue;
    private Transform path;

    private void Awake()
    {
        queue = new Queue<GameObject>();
    }

    public void Init(GameObject obj, Transform path)
    {
        this.path = path;
        this.obj = obj;

        for (int i = 0; i < 5; i++)
        {
            queue.Enqueue(Add_Pool());
        }
    }

    private GameObject Add_Pool()
    {
        GameObject o = Instantiate(obj);
        o.GetComponent<Mob>().Set_Pool(this);
        o.transform.SetParent(path);
        o.SetActive(false);
        return o;
    }

    public GameObject Active(Vector3 transform)
    {
        GameObject pool;
        if(queue.Count > 0)
            pool = queue.Dequeue();
        else 
            pool = Add_Pool();
        pool.transform.position = transform;
        pool.transform.SetParent(path);
        pool.GetComponent<SortingGroup>().sortingOrder = extraction(transform.y);
        pool.GetComponent<Mob>().Set_Pool(this);
        pool.SetActive(true);
        return pool;
    }

    public void Disabled(GameObject obj)
    {
        obj.transform.position = Vector3.zero;
        obj.SetActive(false);
        queue.Enqueue(obj);
    }

    public void Clear()
    {
       Mob[] mob = path.GetComponentsInChildren<Mob>();
       for(int i = 0; i < mob.Length; i++)
       {
            Disabled(mob[i].gameObject);
            //Debug.Log("삭제 ; " + i);
       }
    }

    private int extraction(float num)
    {
        //float m = (-num) * 10;
        float m = -((num + 10) * 10);
        //Debug.Log(m);

        //Debug.Log("원래 값 : " + num + "            변경 값 : " +(int)m );
        return (int)m;
    }

}
