using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public static SceneManager instance;

    // 链表用于存储多个场景面板
    public List<GameObject> scenes;

    //private int currentSceneIndex = 0; // 当前场景的索引

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
    }

    // 显示指定索引的场景，并隐藏其他场景
    public void ShowScene(int index)
    {
        if (index < 0 || index >= scenes.Count) return;  // 检查索引合法性

        for (int i = 0; i < scenes.Count; i++)
        {
            scenes[i].SetActive(i == index);  // 仅激活当前索引对应的场景
        }

        GamePlayManager.instance.ChangeScene();

        //currentSceneIndex = index;  // 更新当前场景索引
    }

    //// 显示下一个场景
    //public void ShowNextScene()
    //{
    //    int nextIndex = (currentSceneIndex + 1) % scenes.Count; // 循环到下一个场景
    //    ShowScene(nextIndex);
    //}

    //// 显示上一个场景
    //public void ShowPreviousScene()
    //{
    //    int previousIndex = (currentSceneIndex - 1 + scenes.Count) % scenes.Count; // 循环到上一个场景
    //    ShowScene(previousIndex);
    //}
}
