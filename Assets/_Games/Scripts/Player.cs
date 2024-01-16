using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private FixedJoystick fixedJoystick;
    [SerializeField] private Bullet bulletPrefab;
    private Rigidbody rb;
    private MiniPool<Bullet> miniPoolBullet;

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
        Target();
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
        if (rb.velocity.sqrMagnitude > 0)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }
        if (rb.velocity.sqrMagnitude > 1f)
        {
            amountBullet = 1;
            Target();
        }
    }
    private void OnShoot()
    {
        transform.rotation = Quaternion.LookRotation(target.position);
        if (canAttack && amountBullet > 0)
        {
            canAttack = false;
            amountBullet = 0;
            Bullet bullet = miniPoolBullet.Spawn(playerGun.transform.position, Quaternion.identity, LevelManager.Instance.poolObj.transform);
            bullet.SetTarget(target.position);
            bullet.rangeSize = radiusSize;
            Invoke(nameof(ResetAttack), 1.5f);
        }
    }

    private void ResetAttack()
    {
        canAttack = true;
    }
    public override void OnInit()
    {
        base.OnInit();
        miniPoolBullet = new MiniPool<Bullet>();
        miniPoolBullet.OnInit(bulletPrefab, 5, LevelManager.Instance.poolObj.transform);
    }

    public Transform Target()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radiusSize, targetLayer);
        if(hitColliders.Length > 0)
        {
            target = hitColliders[0].transform;
        }
        if(target != null && Vector3.Distance(transform.position, target.position) > radiusSize)
        {
            target = null;
        }
        return target;
    }
}
