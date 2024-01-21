using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : BulletMain
{
    private void Update()
    {
        Move();
    }
    public override void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (Vector3.Distance(startPos, transform.position) >= rangeSize || Vector3.Distance(transform.position, target) <= 0.1f)
        {
            OnDestroy();
        }
    }
}
