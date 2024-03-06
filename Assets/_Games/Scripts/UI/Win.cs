using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Win : UICanvas
{
    public Text score;
    public int gold;
    public void MainMenuButton()
    {
        UIManager.Ins.OpenUI<MainMenu>();
        UIManager.Ins.CloseUI<GamePlay>();
        GameManager.Ins.ChangeState(GameState.Start);
        Close(0);
    }
    private void Start()
    {
        gold = Random.Range(50, 101);
        score.text = gold.ToString();
        LevelManager.Ins.UpdateGold(gold);
    }
}
