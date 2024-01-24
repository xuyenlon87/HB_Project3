using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMain : GameUnit
{

    public float speed;
    public Vector3 target;
    public float dame;
    public float rangeSize;
    public Vector3 startPos;

    private void Start()
    {
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

    public virtual void Move()
    {

    }
    public void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Player") || other.CompareTag("Bot"))
        //{
        //    other.GetComponent<Character>().OnHit(dame);
        //    SimplePool.Despawn(LevelManager.Instance.bullet);
        //}
    }
}
