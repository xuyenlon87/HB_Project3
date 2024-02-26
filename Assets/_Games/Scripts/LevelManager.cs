using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager :  Singleton<LevelManager>
{
    public Transform poolObj;
    public Transform poolBullet;
    public int sumPlayer;
    public List<Bot> listBot;

    private void Start()
    {
        sumPlayer = 1;
    }
    public void SpawnBot(int count)
    {
        for (int i = 0; i < count; i++)
        {
            float posX = Random.Range(-49, 50);
            float posZ = Random.Range(-49, 50);
            Bot bot = SimplePool.Spawn<Bot>(PoolType.Character_1, new Vector3(posX, 0f, posZ), Quaternion.identity);
            sumPlayer += 1;
            listBot.Add(bot);
        }
    }
}
