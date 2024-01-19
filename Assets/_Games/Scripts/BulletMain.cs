using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMain : MonoBehaviour
{
    public float speed;
    public Vector3 target;
    public float dame;
    public float rangeSize;
    public Vector3 startPos;
    [SerializeField] GameObject fireballPinkVFX;
    private void Update()
    {
    }


    private void Start()
    {
        OnInit();
    }
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

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Bot"))
        {
            Instantiate(fireballPinkVFX, other.transform.position, Quaternion.identity);
            other.GetComponent<Character>().OnHit(dame);
            OnDestroy();
        }
    }
}
