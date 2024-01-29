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
        currentBullet = BulletType.Boomerang;
        rb = gameObject.GetComponent<Rigidbody>();
        OnInit();
        anim.GetComponent<Animator>();
        GetBullet();
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
                    OnShoot();
                }
            }
        }
    }

    private void Move()
    {
        Vector3 direction = Vector3.forward * fixedJoystick.Vertical + Vector3.right * fixedJoystick.Horizontal;
        rb.velocity = direction * speed;
        if (rb.velocity.sqrMagnitude > 1f)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);
            amountBullet = 1;
            ChangeAnim("IsRun");
        }
        else if (rb.velocity.sqrMagnitude <= 1f)
        {
            ChangeAnim("IsIdle");
        }
    }
    
    public override void OnInit()
    {
        base.OnInit();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bot"))
        {
            target = other.transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Bot"))
        {
            target = null;
        }
    }
}
