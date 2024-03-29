﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Events;

public enum GameState
{
    Start,
    Play,
    Pause,
    Win,
    Lose
}
public class GameManager : Singleton<GameManager>
{
    public GameState currentState;
    [SerializeField] Player player;
    public static GameManager instance { get; private set; }
    //[SerializeField] UserData userData;
    //[SerializeField] CSVData csv;
    //private static GameState gameState = GameState.MainMenu;

    // Start is called before the first frame update
    private void Awake()
    {
        //base.Awake();
        Input.multiTouchEnabled = false;
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        int maxScreenHeight = 1280;
        float ratio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        if (Screen.currentResolution.height > maxScreenHeight)
        {
            Screen.SetResolution(Mathf.RoundToInt(ratio * (float)maxScreenHeight), maxScreenHeight, true);
        }

        //csv.OnInit();
        //userData?.OnInitData();

        //ChangeState(GameState.MainMenu);

    }

    //public static void ChangeState(GameState state)
    //{
    //    gameState = state;
    //}

    //public static bool IsState(GameState state)
    //{
    //    return gameState == state;
    //}
    private void Start()
    {    
        ChangeState(GameState.Start);
    }
    public void ChangeState(GameState newState)
    {
        currentState = newState;
        switch (currentState)
        {
            case GameState.Start:
                StartGame();
                break;
            case GameState.Play:
                PlayGame();
                break;
            case GameState.Pause:
                PauseGame();
                break;
            case GameState.Win:
                WinGame();
                break;
            case GameState.Lose:
                LoseGame();
                break;
        }
    }

    private void WinGame()
    {
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI<Win>();
    }

    private void LoseGame()
    {
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI<Lose>();
    }

    private void PauseGame()
    {
        throw new NotImplementedException();
    }

    private void PlayGame()
    {
        player.OnInit();
        LevelManager.Ins.SpawnBot(49);
    }

    private void StartGame()
    {
        SimplePool.CollectAll();
        player.OnInit();
        LevelManager.Ins.sumPlayer = 1;
        LevelManager.Ins.LoadGold();
        SoundManager.Ins.LoadSound();
        UIManager.Ins.OpenUI<MainMenu>();
        LevelManager.Ins.listBot.Clear();
    }
}
