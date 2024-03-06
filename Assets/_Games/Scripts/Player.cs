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
        fixedJoystick = UIManager.Ins.GetUI<GamePlay>().GetComponentInChildren<FixedJoystick>();
        currentWeapon = WeaponType.KnifeWeapon;
        rb = gameObject.GetComponent<Rigidbody>();
        OnInit();
        anim.GetComponent<Animator>();
        GetWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDead && rb.velocity.sqrMagnitude <= 1f && amountBullet >=1 && target != null && canAttack)
        {
            OnShoot();
        }
    }

    private void Move()
    {
        if (!isDead)
        {
            Vector3 direction = Vector3.forward * fixedJoystick.Vertical + Vector3.right * fixedJoystick.Horizontal;
            rb.velocity = direction * speed;
            if (GameManager.Ins.currentState == GameState.Start)
            {
                charaterImg.transform.LookAt(Camera.main.transform);
            }
            if (rb.velocity.sqrMagnitude > 1f)
            {
                charaterImg.forward = direction;
                amountBullet = 1;
                ChangeAnim("IsRun");
            }
            else if (target ==null)
            {
                ChangeAnim("IsIdle");
            }
        }
    }
    
    public override void OnInit()
    {
        base.OnInit();
        rb.velocity = Vector3.zero;
        LevelManager.Ins.sumPlayer += 1;
        gameObject.SetActive(true);
        gameObject.transform.position = new Vector3(0, 1, 0);
        target = null;
        ChangeAnim("null");
    }
}
