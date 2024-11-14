using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Progress;

public class GamePlayManager : MonoBehaviour
{
    public static GamePlayManager instance;

    public DragItem curDragItem;
    public BinItem curBinItem;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void Update()
    {
        
    }

    public void StatDrag(DragItem item)
    {
        if (item.IfInDrag) return;
        item.IfInDrag = true;
        curDragItem = item;
        curBinItem = new BinItem();
    }

    public void EndDrag()
    {
        if (curDragItem is null)
        {
            Debug.Log("No DragItem!!!");
            return;
        }
        if (curBinItem is null)
        {
            ClickManager.instance.TriggerGameOver();
            curDragItem.ResetPos();
        }
        if(curBinItem.binName!=curDragItem.binName)
        {
            ClickManager.instance.TriggerGameOver();
            curDragItem.ResetPos();
        }
        else
        {
            GetRightGarbage();
        }

        //TODO直接设置为null有问题
        curDragItem.IfInDrag = false;
    }

    public void GetBin(BinItem item)
    {
        curBinItem = item;
    }

    public void DeleteBin()
    {
        curBinItem = null;
    }

    public void GetRightGarbage()
    {
        curBinItem.garbageItems.Add(curDragItem);
        curDragItem.gameObject.SetActive(false);
    }

    public void ChangeScene()
    {
        if (curDragItem is not null)
        { 
            curDragItem.IfInDrag = false;
            curDragItem.ResetPos();
    }
}
}
