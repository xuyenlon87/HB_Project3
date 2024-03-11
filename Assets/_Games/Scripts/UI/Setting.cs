using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : UICanvas
{
    [SerializeField] GameObject off;
    [SerializeField] GameObject on;

    public void OnOffSound()
    {
        SoundManager.Ins.soundOn = !SoundManager.Ins.soundOn;
        if (SoundManager.Ins.soundOn)
        {
            on.SetActive(true);
            off.SetActive(false);
            SoundManager.Ins.SetSFXVolume(1);
        }
        else if (!SoundManager.Ins.soundOn)
        {
            off.SetActive(true);
            on.SetActive(false);
            SoundManager.Ins.SetSFXVolume(0);
        }
        PlayerPrefs.SetInt("SoundOn", SoundManager.Ins.soundOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void ButtonBack()
    {
        UIManager.Ins.OpenUI<GamePlay>();
        Close(0);
    }
}
