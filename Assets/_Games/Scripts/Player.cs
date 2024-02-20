using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private FixedJoystick fixedJoystick;
    private Rigidbody rb;
    public Transform weaponPlayer;

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
        if (rb.velocity.sqrMagnitude <= 0.1f)
        {
            OnShoot();
        }
    }

    private void Move()
    {
        Vector3 direction = Vector3.forward * fixedJoystick.Vertical + Vector3.right * fixedJoystick.Horizontal;
        rb.velocity = direction * speed;
        if(GameManager.Ins.currentState == GameState.Start)
        {
            charaterImg.transform.LookAt(Camera.main.transform);
        }
        if (rb.velocity.sqrMagnitude > 1f)
        {
            charaterImg.forward = direction;
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
