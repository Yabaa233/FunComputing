using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject OverPanel;
    public GameObject StartPanel;
    public GameObject ConfigPanel;
    public GameObject CollectionPanel;

    public enum GameState
    {
        Start,
        InGame,
        Collection,
        Config,
        Over
    }

    private GameState currentState;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        SetState(GameState.Start);  // 初始状态为 Start
    }

    public void SetState(GameState newState)
    {
        // 退出当前状态的 UI 设置
        ExitCurrentState();

        // 切换到新状态
        currentState = newState;

        // 进入新状态的 UI 设置
        EnterNewState();
    }

    private void ExitCurrentState()
    {
        // 关闭当前状态的面板
        switch (currentState)
        {
            case GameState.Start:
                StartPanel.SetActive(false);
                break;
            case GameState.InGame:
                // InGame 状态一般没有对应的 UI 面板
                break;
            case GameState.Collection:
                CollectionPanel.SetActive(false);
                break;
            case GameState.Config:
                ConfigPanel.SetActive(false);
                break;
            case GameState.Over:
                OverPanel.SetActive(false);
                break;
        }
    }

    private void EnterNewState()
    {
        // 打开新状态对应的面板
        switch (currentState)
        {
            case GameState.Start:
                StartPanel.SetActive(true);
                break;
            case GameState.InGame:
                // InGame 状态一般不需要 UI 面板
                break;
            case GameState.Collection:
                CollectionPanel.SetActive(true);
                break;
            case GameState.Config:
                ConfigPanel.SetActive(true);
                break;
            case GameState.Over:
                OverPanel.SetActive(true);
                break;
        }
    }

    public void StartGame()
    {
        SetState(GameState.InGame);
    }

    public void ShowCollection()
    {
        SetState(GameState.Collection);
        CollectionManager.instance.RefreshItemUI();
    }

    public void ShowConfig()
    {
        SetState(GameState.Config);
    }

    public void GameOver()
    {
        SetState(GameState.Over);
    }

    public void Retry()
    {
        SetState(GameState.InGame);
    }

    public void EndGame()
    {
        SetState(GameState.Start);
    }
}
