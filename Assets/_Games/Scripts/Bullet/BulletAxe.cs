using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAxe : BulletMain
{
    private float rotationSpeed = 360f;

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
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
            if (Vector3.Distance(startPos, transform.position) >= rangeSize || Vector3.Distance(transform.position, target) <= 0.1f)
            {
                gameObject.SetActive(false);
            }
        }

    }
}
