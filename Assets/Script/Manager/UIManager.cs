using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject OverPanel;
    public GameObject StartPanel;
    public GameObject ConfigPanel;
    public GameObject CollectionPanel;
    public GameObject StickPanel;

    public GameObject CollectionDetailPanel;


    public enum UIState
    {
        Start,
        InGame,
        Collection,
        Stick,
        Config,
        Over
    }

    private UIState currentState;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        SetState(UIState.Start);  // 初始状态为 Start
    }

    private void Update()
    {
        if (Input.GetKeyDown("c"))
        {
            ShowCollection();
        }
    }
    public void SetState(UIState newState)
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
            case UIState.Start:
                StartPanel.SetActive(false);
                break;
            case UIState.InGame:
                // InGame 状态一般没有对应的 UI 面板
                break;
            case UIState.Collection:
                CollectionPanel.SetActive(false);
                break;
            case UIState.Config:
                ConfigPanel.SetActive(false);
                break;
            case UIState.Over:
                OverPanel.SetActive(false);
                break;
        }
    }

    private void EnterNewState()
    {
        // 打开新状态对应的面板
        switch (currentState)
        {
            case UIState.Start:
                StartPanel.SetActive(true);
                break;
            case UIState.InGame:
                // InGame 状态一般不需要 UI 面板
                break;
            case UIState.Collection:
                CollectionPanel.SetActive(true);
                break;
            case UIState.Stick:
                StickPanel.SetActive(true);
                break;
            case UIState.Config:
                ConfigPanel.SetActive(true);
                break;
            case UIState.Over:
                OverPanel.SetActive(true);
                break;
        }
    }

    public void StartGame()
    {
        SetState(UIState.InGame);
        SoundManager.instance.GameStart();
    }

    public void ShowCollection()
    {
        SetState(UIState.Collection);
        CollectionManager.instance.RefreshCollectionItemUI();
    }

    public void ShowStick()
    {
        SetState(UIState.Stick);
        CollectionManager.instance.ShowFirstStickItemUI();
    }

    public void ShowConfig()
    {
        SetState(UIState.Config);
    }

    public void GameOver()
    {
        SetState(UIState.Over);
        SoundManager.instance.GameOver();

    }

    public void Retry()
    {
        SetState(UIState.InGame);
        SoundManager.instance.GameStart();

    }

    public void EndGame()
    {
        SetState(UIState.Start);
        SoundManager.instance.GameStart();

    }

    public void ColseInUI(GameObject uiOb)
    {
        uiOb.SetActive(false);
    }
}
