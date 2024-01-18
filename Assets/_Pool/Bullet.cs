using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed;
    private Vector3 target;
    private float dame;
    private float rangeSize;
    private Vector3 startPos;
    public void OnInit()
    {
        dame = 1;
        speed = 5f;
        startPos = transform.position;
        rangeSize = 5f;
    }
    public void SetTarget(Vector3 target)
    {
        this.target = target;
    }
    public void SetRangeSize(float rangeSize)
    {
        this.rangeSize = rangeSize;
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
        if (Vector3.Distance(startPos, transform.position) >= rangeSize || Vector3.Distance(transform.position, target) <=0.1f)
        {
            OnDestroy();
        }
    }
}
