using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager :  Singleton<LevelManager>
{
    public Transform poolObj;
    public Transform poolBullet;
    public int sumPlayer;
    public List<Bot> listBot;
    public int sumGold;

    private void Start()
    {
        sumPlayer = 0;
    }
    public void SpawnBot(int count)
    {
        if(LevelManager.Ins.listBot.Count <= count)
        {
            for (int i = 0; i < count; i++)
            {
                float posX = Random.Range(-49, 50);
                float posZ = Random.Range(-49, 50);
                Bot bot = SimplePool.Spawn<Bot>(PoolType.Character_1, new Vector3(posX, 0f, posZ), Quaternion.identity);
                bot.OnInit();
                sumPlayer += 1;
                listBot.Add(bot);
            }
        }
    }

    public void UpdateGold( int gold)
    {
        sumGold += gold;
        SaveGold();
    }
    public void SaveGold()
    {
        PlayerPrefs.SetInt("Gold", sumGold);
        PlayerPrefs.Save();
    }
    public void LoadGold()
    {
        if (PlayerPrefs.HasKey("Gold"))
        {
            sumGold = PlayerPrefs.GetInt("Gold");
        }
    }
}
