using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private FixedJoystick fixedJoystick;
    private Rigidbody rb;
    public void FixedUpdate()
    {
        Move();
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            if (rb.velocity.sqrMagnitude <= 0.1f)
            {
                if (target != null)
                {
                    transform.rotation = Quaternion.LookRotation(lookTarget);
                    if (Quaternion.Angle(transform.rotation, Quaternion.LookRotation(lookTarget)) < 0.1f)
                    {
                        OnShoot();
                    }
                }
            }
        }
    }

    private void Move()
    {
        Vector3 direction = Vector3.forward * fixedJoystick.Vertical + Vector3.right * fixedJoystick.Horizontal;
        rb.velocity = direction * speed;
        if (rb.velocity.sqrMagnitude > 0)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }
        if (rb.velocity.sqrMagnitude > 1f)
        {
            amountBullet = 1;
        }
    }
    
    public override void OnInit()
    {
        base.OnInit();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bot"))
        {
            target = other.transform;
            lookTarget = new Vector3(target.position.x, 0, target.position.z);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Bot"))
        {
            target = null;
        }
    }
}
