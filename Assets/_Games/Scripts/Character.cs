using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private GameObject sight;
    public float speed;
    public int amountBullet;
    public float hp;
    public float radiusSize;
    public Transform playerGun;
    public Transform target;
    public bool canAttack;
    public bool isDead;
    public int level;
    [SerializeField] GameObject expPotionPrefab;
    public Bullet bulletPrefab;
    public Vector3 lookTarget;
    private MiniPool<Bullet> miniPoolBullet;

    public virtual void OnInit()
    {
        radiusSize = 5;
        hp = 1;
        amountBullet = 0;
        canAttack = true;
        isDead = false;
        level = 1;
        miniPoolBullet = new MiniPool<Bullet>();
        miniPoolBullet.OnInit(bulletPrefab, 5, LevelManager.Instance.poolObj.transform);
        speed = 5f;
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

    public void OnDeath()
    {
        Destroy(gameObject);
        int random = Random.Range(0, 100);
        if(random < 30)
        {
            GameObject expPotion =  Instantiate(expPotionPrefab, new Vector3(transform.position.x, -1.2f, transform.position.z), Quaternion.identity, LevelManager.Instance.transform);
            expPotion.GetComponent<Exp>().level = Random.Range(1, level + 1);
        }
    }
    public void Upgrade(int add)
    {
        speed += add * 0.05f;
        transform.localScale += new Vector3(add * 0.05f, add * 0.05f, add * 0.05f);
        radiusSize += add * 0.05f;
        level += add;
        sight.transform.localScale += new Vector3(add * 0.05f, add * 0.05f, add * 0.05f);
    }
    public virtual void OnShoot()
    {
        if (canAttack && amountBullet > 0)
        {
            canAttack = false;
            amountBullet = 0;
            Bullet bullet = miniPoolBullet.Spawn(playerGun.transform.position, Quaternion.identity, LevelManager.Instance.poolObj.transform);
            if(target != null)
            {
                bullet.SetTarget(target.position);
                bullet.SetRangeSize(radiusSize);
                Invoke(nameof(ResetAttack), 1.5f);
            }
        }
    }
    public void ResetAttack()
    {
        canAttack = true;
    }
}
