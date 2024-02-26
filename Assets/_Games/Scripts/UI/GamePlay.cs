using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlay : UICanvas
{
    [SerializeField] private Text sumPlayer;


    private void Start()
    {

    }
    private void Update()
    {
        sumPlayer.text = LevelManager.Ins.sumPlayer.ToString();

    }
    public void WinButton()
    {
        UIManager.Ins.OpenUI<Win>().score.text = Random.Range(100, 200).ToString();
        Close(0);
    }

    public void LoseButton()
    {
        UIManager.Ins.OpenUI<Lose>().score.text = Random.Range(0, 100).ToString(); 
        Close(0);
    }

    public void SettingButton()
    {
        UIManager.Ins.OpenUI<Setting>();
    }
}
