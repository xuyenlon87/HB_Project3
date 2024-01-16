using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rb;
    private float speed;
    private Vector3 targetPosition;
    private float dame;
    private float distanceMove;
    public float rangeSize;
    public void OnInit()
    {
        Debug.Log(targetPosition);
        dame = 1;
        speed = 5f;
    }
    public void SetTarget(Vector3 target)
    {
        targetPosition = target;
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

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.fixedDeltaTime);
        distanceMove += speed * Time.fixedDeltaTime;
        if (distanceMove >= rangeSize)
        {
            OnDestroy();
        }
        Debug.Log(rangeSize);
    }

}
