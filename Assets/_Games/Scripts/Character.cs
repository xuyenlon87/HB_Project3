using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
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
    public GameObject playerGun;
    public Transform target;
    public bool canAttack;
    public bool isDead;
    public int level;
    public float timeResetAttack;
    [SerializeField] GameObject expPotionPrefab;
    public Vector3 lookTarget;
    public Transform charaterImg;
    private MiniPool<Bullet> miniPoolBullet;
    public BulletType currentBullet;
    public Transform bulletStart;
    public BulletMain bullet;
    public Animator anim;
    public string currentAnimName;


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
        currentBullet = BulletType.Bullet;
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
                OnDeath();
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
            GameObject expPotion =  Instantiate(expPotionPrefab, new Vector3(transform.position.x, 0.25f, transform.position.z), Quaternion.identity, LevelManager.Instance.transform);
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
    public virtual void OnShoot()
    {
        if(target != null)
        {
            RotateTarget();
            if (canAttack && amountBullet > 0)
            {
                if(currentBullet == BulletType.Bullet)
                {
                      bullet = SimplePool.Spawn<Bullet>(PoolType.Bullet_1, new Vector3(bulletStart.position.x, 0f, bulletStart.position.z), Quaternion.identity, LevelManager.Instance.poolBullet);
                }
                else if(currentBullet == BulletType.Axe)
                {
                     bullet = SimplePool.Spawn<BulletAxe>(PoolType.Bullet_2, new Vector3(bulletStart.position.x, 0f, bulletStart.position.z), Quaternion.identity, LevelManager.Instance.poolBullet);

                }
                else if(currentBullet == BulletType.Boomerang)
                {
                     bullet = SimplePool.Spawn<BulletBoomerang>(PoolType.Bullet_3, new Vector3(bulletStart.position.x, 0f, bulletStart.position.z), Quaternion.identity, LevelManager.Instance.poolBullet);

                }
                //bullet.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                bullet.SetTarget(target.position);
                bullet.SetRangeSize(radiusSize);
                canAttack = false;
                amountBullet = 0;
                Invoke(nameof(ResetAttack), timeResetAttack);
            }
        }      
    }
    public void ResetAttack()
    {
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
    protected void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.SetBool(currentAnimName,false);
            currentAnimName = animName;
            anim.SetBool(currentAnimName, true);
        }
    }
}
