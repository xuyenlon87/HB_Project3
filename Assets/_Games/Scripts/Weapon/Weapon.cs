using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    KnifeWeapon,
    AxeWeapon,
    BoomerangWeapon,
}
public class Weapon : MonoBehaviour
{

    //public void Shoot()
    //{
    //    if (currentBullet == BulletType.Bullet)
    //    {
    //        bullet = SimplePool.Spawn<BulletKnife>(PoolType.Bullet_1, new Vector3(-0.2f, 0.1f, 0.1f), Quaternion.identity, hand);
    //        bullet.transform.localRotation = Quaternion.Euler(0f, 0f, -90f);
    //        bullet.transform.localScale = new Vector3(30f, 30f, 30f);
    //    }
    //    else if (currentBullet == BulletType.Axe)
    //    {
    //        bullet = SimplePool.Spawn<BulletAxe>(PoolType.Bullet_2, new Vector3(-0.2f, 0.1f, 0.1f), Quaternion.identity, hand);
    //        bullet.transform.localRotation = Quaternion.Euler(0f, 0f, -90f);
    //        bullet.transform.localScale = new Vector3(30f, 30f, 30f);
    //    }
    //    else if (currentBullet == BulletType.Boomerang)
    //    {
    //        bullet = SimplePool.Spawn<BulletBoomerang>(PoolType.Bullet_3, new Vector3(-0.3f, 0.1f, 0.1f), Quaternion.identity, hand);
    //        bullet.transform.localRotation = Quaternion.Euler(0f, 0f, -90f);
    //        bullet.transform.localScale = new Vector3(8f, 8f, 8f);
    //    }
    //}
}
