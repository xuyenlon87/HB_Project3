using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float speed;
    public int amountBullet;
    public float hp;
    public float radiusSize;
    public Transform playerGun;
    public LayerMask targetLayer;
    public Transform target;
    public bool canAttack;
    public bool isDead;
    public int level;
    [SerializeField] GameObject expPotionPrefab;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public virtual void OnInit()
    {
        radiusSize = 5;
        hp = 1;
        amountBullet = 0;
        canAttack = true;
        isDead = false;
        level = 1;
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
            GameObject expPotion =  Instantiate(expPotionPrefab, transform.position, Quaternion.identity, LevelManager.Instance.transform);
            expPotion.GetComponent<Exp>().level = Random.Range(1, level + 1);
        }
    }
    public void Upgrade(int add)
    {
        speed += add * 0.05f;
        transform.localScale += new Vector3(add * 0.05f, add * 0.05f, add * 0.05f);
        radiusSize += add * 0.05f;
        level += add;
    } 
}
