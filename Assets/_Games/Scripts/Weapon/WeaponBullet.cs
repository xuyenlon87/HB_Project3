using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBullet : Weapon
{
    public void Shoot(Character character)
    {
        Bullet bullet = SimplePool.Spawn<Bullet>(PoolType.Bullet_3, character.playerGun.transform.position, Quaternion.identity);
        bullet.SetTarget(character.target.position);
        bullet.SetRangeSize(character.radiusSize);
    }
}
