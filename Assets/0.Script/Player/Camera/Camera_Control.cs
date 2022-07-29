using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Control : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private Transform player;
    [SerializeField] private SpriteRenderer map;
    [SerializeField] private float p;
    Vector3 pos;
    float min, max;


    private void Start()
    {
        //pos = new Vector3(0, 3.5f,-10);
        pos = new Vector3(0, p, -10f);
        transform.position = pos + player.position;
        Check_map();

    }


    private void FixedUpdate()
    {
        Move_Camera();
    }

    private void Check_map()
    {
        Camera camera = Camera.main;
        float c_width = camera.aspect * camera.orthographicSize;
        float c_height = camera.orthographicSize*2;
        float m_width = map.bounds.size.x/2;

        min = -m_width + c_width;
        max = m_width - c_width;
    }

    private void Move_Camera()
    {
        transform.position = Vector3.Lerp(transform.position, player.position + pos, Speed * Time.deltaTime);
        float limit = Mathf.Clamp(transform.position.x, min, max);
        transform.position = new Vector3(limit, p, -10);
    }
}
