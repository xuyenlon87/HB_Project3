using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed;
    private Vector3 target;
    private float dame;
    private float distanceMove;
    public float rangeSize;
    public void OnInit()
    {
        dame = 1;
        speed = 5f;
    }
    public void SetTarget(Vector3 target)
    {
        this.target = target;
    }
    public void OnDestroy()
    {
        Destroy(gameObject);
    }

    private void Start()
    {
        OnInit();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") || other.CompareTag("Bot"))
        {
            other.GetComponent<Character>().OnHit(dame);
            OnDestroy();
        }
    }

    private void Update()
    {
        Move();   
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        distanceMove += speed * Time.fixedDeltaTime;
        if (distanceMove >= rangeSize)
        {
            OnDestroy();
        }
        Debug.Log(rangeSize);
    }
}
