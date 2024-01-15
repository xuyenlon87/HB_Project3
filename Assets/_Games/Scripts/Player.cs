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
        Move();
        if (canAttack) 
        {
            if(amountBullet > 0)
            {
                if(rb.velocity.sqrMagnitude < 0.1f)
                {
                    Debug.Log("here");

                    if (targetPosition != null)
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
            Target();
        }
    }
    private void OnShoot()
    {
        canAttack = false;
        amountBullet = 0;
        Bullet bullet = miniPoolBullet.Spawn(playerGun.transform.position, Quaternion.identity, LevelManager.Instance.poolObj.transform);
        bullet.OnInit(targetPosition.position);
        Invoke("ResetAttack", 1.5f);
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
        targetPosition =  hitColliders[0].transform;
        return targetPosition;
        
    }
}
