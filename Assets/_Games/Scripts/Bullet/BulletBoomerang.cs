using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBoomerang : BulletMain
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
            target = startPos;
        }
        if( Vector3.Distance(target, startPos)<= 0.1f && Vector3.Distance(transform.position, startPos) <= 0.1f)
        {
            Destroy(gameObject);
        }
    }
}
