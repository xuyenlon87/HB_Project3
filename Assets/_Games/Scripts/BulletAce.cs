using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAce : BulletMain
{
    private float rotationSpeed = 30f;
    public override void Move()
    {

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        if (Vector3.Distance(startPos, transform.position) >= rangeSize || Vector3.Distance(transform.position, target) <= 0.1f)
        {
            OnDestroy();
        }
    }
}
