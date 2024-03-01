using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Win : UICanvas
{
    public Text score;
    private int goldAmount;
    public void MainMenuButton()
    {
        UIManager.Ins.OpenUI<MainMenu>();
        score.text = goldAmount.ToString();
        GameManager.Ins.ChangeGold(goldAmount);
        Close(0);
    }
}
