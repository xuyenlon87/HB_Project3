using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlay : UICanvas
{
    [SerializeField] private Text sumPlayer;
    [SerializeField] private Camera mainCam;
    private Vector3 arrowIndicateBotPos;
    public Image arrowBotImg;

    private void Start()
    {
        mainCam = Camera.main;
    }
    private void Update()
    {
        sumPlayer.text = LevelManager.Ins.sumPlayer.ToString();

        //arrowBot
        for(int i = 0; i < LevelManager.Ins.listBot.Count; i++)
        {
            arrowIndicateBotPos = mainCam.WorldToViewportPoint(LevelManager.Ins.listBot[i].transform.position);
            if (arrowIndicateBotPos.x < 0 || arrowIndicateBotPos.x > 1 || arrowIndicateBotPos.y < 0 || arrowIndicateBotPos.y > 1)
            {
                // Hiển thị prefab chỉ định trên UI và cập nhật vị trí của nó
                RectTransform canvasRectTransform = GetComponent<RectTransform>();
                Vector2 indicatorPos = new Vector2(arrowIndicateBotPos.x * canvasRectTransform.rect.width, arrowIndicateBotPos.y * canvasRectTransform.rect.height);
                arrowBotImg.rectTransform.anchoredPosition = indicatorPos;
                arrowBotImg.enabled = true;
            }
            else
            {
                // Ẩn indicator nếu con bot không nằm ngoài màn hình
                arrowBotImg.enabled = false;
            }
        }
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
