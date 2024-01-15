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
    private float rangeSize;
    public void OnInit(Vector3 target)
    {
        targetPosition = target;
        Debug.Log(targetPosition);
        dame = 1;
        speed = 5f;
        rangeSize = 5f;
    }

    public void OnDespawn()
    {
        Destroy(gameObject);
    }

    private void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") || other.CompareTag("Bot"))
        {
            other.GetComponent<Character>().OnHit(dame);
            OnDespawn();

        }
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.fixedDeltaTime);
        distanceMove += speed * Time.fixedDeltaTime;
        if(distanceMove >= rangeSize)
        {
            OnDespawn();
        }
    }

}
