using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : GameUnit
{
    [SerializeField] private GameObject sight;
    public float speed;
    public int amountBullet;
    public float hp;
    public float radiusSize;
    public Transform target;
    public bool canAttack;
    public bool isDead;
    public int level;
    public Transform charaterImg;
    public Animator anim;
    public string currentAnimName = null;
    public PoolControler pool;
    public BulletMain bullet;
    public WeaponType currentWeapon;
    public GameObject knifePrefab;
    public GameObject axePrefab;
    public GameObject BoomerangPrefab;
    public GameObject weapon;


    public virtual void OnInit()
    {
        radiusSize = 5;
        hp = 1;
        amountBullet = 0;
        canAttack = true;
        isDead = false;
        level = 1;
        speed = 5f;
        ChangeAnim("IsIdle");
        GetWeapon();
    }

    public void OnHit(float damage)
    {
        if (!isDead)
        {
            hp -= damage;
            if (hp <=0)
            {
                hp = 0;
                isDead = true;
                ChangeAnim("IsDead");
                LevelManager.Ins.sumPlayer -= 1;
                Invoke(nameof(OnDeath), 1.5f);
                if(LevelManager.Ins.listBot.Count <= 0)
                {
                    GameManager.Ins.ChangeState(GameState.Win);
                }
            }
        }
    }
    public void ChangeWeapon(WeaponType newWeapon)
    {
        if(currentWeapon != newWeapon)
        {
            currentWeapon = newWeapon;
        }
    }
    public void OnDeath()
    {
        SoundManager.Ins.PlaySoundAt(SoundManager.Ins.die, gameObject.transform.position);
        Destroy(gameObject);
    }
    //public void Upgrade(int add, Bot bot = null)
    //{
    //    speed += add * 0.05f;
    //    charaterImg.localScale += new Vector3(add * 0.05f, add * 0.05f, add * 0.05f);
    //    radiusSize += add * 0.05f;
    //    level += add;
    //    sight.transform.localScale += new Vector3(add * 0.05f, add * 0.05f, add * 0.05f);
    //    bot.SetSpeed(speed);
    //}

    public void GetWeapon()
    {
        if (!isDead)
        {
            if (currentWeapon == WeaponType.KnifeWeapon)
            {
                knifePrefab.SetActive(true);
                axePrefab.SetActive(false);
                BoomerangPrefab.SetActive(false);
            }
            else if (currentWeapon == WeaponType.AxeWeapon)
            {
                knifePrefab.SetActive(false);
                axePrefab.SetActive(true);
                BoomerangPrefab.SetActive(false);
            }
            else if (currentWeapon == WeaponType.BoomerangWeapon)
            {
                knifePrefab.SetActive(false);
                axePrefab.SetActive(false);
                BoomerangPrefab.SetActive(true);
            }
        }
    }
    public virtual void OnShoot()
    {
        RotateTarget();
        ChangeAnim("IsAttack");
        SoundManager.Ins.PlaySoundAt(SoundManager.Ins.attack, gameObject.transform.position);
        StartCoroutine(Shoot());
        
    }

    public IEnumerator Shoot()
    {
        yield return new WaitForSeconds(0.26f);
        DeActiveWeapon();
        if (target != null && canAttack && amountBullet > 0 && !isDead)
        {
            switch (currentWeapon)
            {
                case WeaponType.KnifeWeapon:
                    bullet = SimplePool.Spawn<BulletKnife>(PoolType.Bullet_1, weapon.transform.position, Quaternion.identity);
                    bullet.transform.LookAt(target);
                    break;
                case WeaponType.AxeWeapon:
                    bullet = SimplePool.Spawn<BulletAxe>(PoolType.Bullet_2, weapon.transform.position, weapon.transform.rotation);
                    break;
                case WeaponType.BoomerangWeapon:
                    bullet = SimplePool.Spawn<BulletBoomerang>(PoolType.Bullet_3, weapon.transform.position, weapon.transform.rotation);
                    break;
            }
            bullet.SetTarget(target.transform.position);
            canAttack = false;
            amountBullet = 0;
        }
        StartCoroutine(ResetAttack());
    }

    public IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(1f);
        weapon.SetActive(true);
        canAttack = true;
        if (!isDead)
        {
            ChangeAnim("IsIdle");
        }
    }
    
    public void DeActiveWeapon()
    {
        weapon.SetActive(false);
    }
    public void RotateTarget()
    {
        if (target != null)
        {
            charaterImg.transform.LookAt(new Vector3(target.position.x, 0f, target.position.z));
        }
    }
    public void ChangeAnim(string newAnimName)
    {

        if (currentAnimName != newAnimName)
        {
            anim.ResetTrigger(currentAnimName);
            currentAnimName = newAnimName;
            anim.SetTrigger(currentAnimName);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bot") || other.CompareTag("Player"))
        {
            target = other.transform;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Bot") || other.CompareTag("Player"))
        {
            target = null;
        }
    }
}
