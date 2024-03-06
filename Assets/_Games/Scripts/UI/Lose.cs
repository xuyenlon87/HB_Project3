using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lose : UICanvas
{
    public Text score;

    public void MainMenuButton()
    {
        UIManager.Ins.OpenUI<MainMenu>();
        UIManager.Ins.CloseUI<GamePlay>();
        GameManager.Ins.ChangeState(GameState.Start);
        Close(0);
    }
}
