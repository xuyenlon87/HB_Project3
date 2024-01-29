using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager :  Singleton<LevelManager>
{
    public Transform poolObj;
    public Transform poolBullet;
    private void Start()
    {
        //SpawnBot(50);
    }
    
    private static LevelManager instance;

    //public void SpawnBot(int count)
    //{
    //    for(int i=0; i<count; i++)
    //    {
    //        float posX = Random.Range(-49, 50);
    //        float posZ = Random.Range(-49, 50);
    //        SimplePool.Spawn<Bot>(PoolType.Character_1, new Vector3(posX, 0f, posZ), Quaternion.identity);
    //    }
    //}
}
