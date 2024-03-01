using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : UICanvas
{
    [SerializeField] GameObject off;
    [SerializeField] GameObject on;
    public static bool soundOn = true;
    public void OnOffSound()
    {
        soundOn = !soundOn;
        if (soundOn)
        {
            on.SetActive(true);
            off.SetActive(false);
            SoundManager.Ins.SetSFXVolume(1);
        }
        else if (!soundOn)
        {
            off.SetActive(true);
            on.SetActive(false);
            SoundManager.Ins.SetSFXVolume(0);
        }

    }

    public void ButtonBack()
    {
        UIManager.Ins.OpenUI<GamePlay>();
        Close(0);
    }
}
