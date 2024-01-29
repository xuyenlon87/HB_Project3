using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : GameUnit
{
    public enum BulletType
    {
        Bullet,
        Axe,
        Boomerang,
    }
    [SerializeField] private GameObject sight;
    public float speed;
    public int amountBullet;
    public float hp;
    public float radiusSize;
    public Transform target;
    public bool canAttack;
    public bool isDead;
    public int level;
    public float timeResetAttack;
    [SerializeField] GameObject expPotionPrefab;
    public Transform charaterImg;
    public BulletType currentBullet;
    public Transform bulletStart;
    public Animator anim;
    public string currentAnimName = null;
    public Transform hand;
    public PoolControler pool;
    public BulletMain bullet;
    public GameObject bomerangPrefab;
    public virtual void OnInit()
    {
        radiusSize = 5;
        hp = 1;
        amountBullet = 0;
        canAttack = true;
        isDead = false;
        level = 1;
        speed = 5f;
        timeResetAttack = 2f;
        ChangeAnim("IsIdle");
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
                Invoke(nameof(OnDeath), 1.5f);
            }
        }
    }
    public void ChangeBullet(BulletType newBullet)
    {
        if(currentBullet != newBullet)
        {
            currentBullet = newBullet;
        }
    }
    public void OnDeath()
    {
        Destroy(gameObject);
        int random = Random.Range(0, 100);
        if(random < 30)
        {
            GameObject expPotion =  Instantiate(expPotionPrefab, new Vector3(transform.position.x, 0.25f, transform.position.z), Quaternion.identity, LevelManager.Ins.transform);
            expPotion.GetComponent<Exp>().level = Random.Range(1, level + 1);
            Debug.Log(random);
        }
    }
    public void Upgrade(int add, Bot bot = null)
    {
        speed += add * 0.05f;
        charaterImg.localScale += new Vector3(add * 0.05f, add * 0.05f, add * 0.05f);
        radiusSize += add * 0.05f;
        level += add;
        sight.transform.localScale += new Vector3(add * 0.05f, add * 0.05f, add * 0.05f);
        bot.SetSpeed(speed);

    }

    public virtual void GetBullet()
    {
        if (currentBullet == BulletType.Bullet)
        {
            bullet = SimplePool.Spawn<Bullet>(PoolType.Bullet_1, new Vector3(-0.2f, 0.1f, 0.1f), Quaternion.identity, hand);
            bullet.transform.localRotation = Quaternion.Euler(0f, 0f, -90f);
            bullet.transform.localScale = new Vector3(30f, 30f, 30f);
        }
        else if (currentBullet == BulletType.Axe)
        {
            bullet = SimplePool.Spawn<BulletAxe>(PoolType.Bullet_2, new Vector3(-0.2f, 0.1f, 0.1f), Quaternion.identity, hand);
            bullet.transform.localRotation = Quaternion.Euler(0f, 0f, -90f);
            bullet.transform.localScale = new Vector3(30f, 30f, 30f);
        }
        else if (currentBullet == BulletType.Boomerang)
        {
            bullet = SimplePool.Spawn<BulletBoomerang>(PoolType.Bullet_3, new Vector3(-0.3f, 0.1f, 0.1f), Quaternion.identity, hand);
            bullet.transform.localRotation = Quaternion.Euler(0f, 0f, -90f);
            bullet.transform.localScale = new Vector3(8f, 8f, 8f);
        }
        Debug.Log("here");
    }
    public virtual void OnShoot()
    {
        RotateTarget();
        if (target != null && canAttack && amountBullet > 0 && !isDead)
        {
            ChangeAnim("IsAttack");
            Debug.Log("attack");
            bullet.transform.SetParent(LevelManager.Ins.transform);
            bullet.SetTarget(target.position);
            bullet.SetRangeSize(radiusSize);
            canAttack = false;
            amountBullet = 0;
            Invoke(nameof(ResetAttack), timeResetAttack);
        }
    }
    public void ResetAttack()
    {
        GetBullet();
        canAttack = true;
    }
    public void RotateTarget()
    {
        if (target != null)
        {
            Vector3 directionToTarget = target.position - transform.position;
            directionToTarget.y = 0f;
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1f);
        }
    }
    protected void ChangeAnim(string newAnimName)
    {

        if (currentAnimName != newAnimName)
        {
            anim.ResetTrigger(currentAnimName);
            currentAnimName = newAnimName;
            anim.SetTrigger(currentAnimName);
        }
    }
}
