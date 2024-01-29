using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : BulletMain
{
    private void Start()
    {
        OnInit();
    }
    private void Update()
    {
        Move();
    }
    public override void Move()
    {
        if(target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            if (Vector3.Distance(startPos, transform.position) >= rangeSize || Vector3.Distance(transform.position, target) <= 0.1f)
            {
                Destroy(gameObject);
            }
        }

    }
}
