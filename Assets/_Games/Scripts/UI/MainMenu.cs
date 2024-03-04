using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : UICanvas
{
    public Text goldText;
    public void PlayButton()
    {
        UIManager.Ins.OpenUI<GamePlay>();
        Close(0);
        GameManager.Ins.ChangeState(GameState.Play);
    }

    public void ButtonWeapon()
    {
        UIManager.Ins.OpenUI<ShopWeapon>();
        Close(0);
    }

    public void ButtonShop()
    {
        UIManager.Ins.OpenUI<Shop>();
        Close(0);
    }
    public void ButtonSetting()
    {
        UIManager.Ins.OpenUI<Setting>();
        Close(0);
    }
    private void Start()
    {
        LevelManager.Ins.LoadGold();
        goldText.text = LevelManager.Ins.sumGold.ToString();
    }
}
