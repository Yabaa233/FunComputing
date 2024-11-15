using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickManager : MonoBehaviour
{
    public static ClickManager instance;

    public Material highlightMaterial;
    public Material defaultMaterial;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }

    // 切换到高亮材质
    public void HighlightItem(GameObject item)
    {
          item.GetComponent<Image>().material = highlightMaterial;

    }

    // 切换回默认材质
    public void ResetItemMaterial(GameObject item)
    {
        item.GetComponent<Image>().material = defaultMaterial;

    }

    // 销毁被点击的物体
    public void DestroyItem(GameObject item)
    {
        Destroy(item);
    }

    // 收集被点击的物体
    public void CollectItem(CollectionItem item)
    {
        if(item.ifCollect == true) return;
        // Debug.Log("Item collected: " + item.itemData.itemName);
        // 使用 ScriptableObject.CreateInstance 来创建一个新的 CollectionItemData 实例
        CollectionItemData cloneItemData = ScriptableObject.CreateInstance<CollectionItemData>();

        // 复制属性
        cloneItemData.itemName = item.itemData.itemName;
        cloneItemData.itemSprite = item.itemData.itemSprite;
        cloneItemData.itemDetail = item.itemData.itemDetail;

        CollectionManager.instance.AddItem(cloneItemData);

        GameOverItem overItem = null;
        DragItem dragItem = null;

        if (!item.gameObject.TryGetComponent<GameOverItem>(out overItem)
            &&(!item.gameObject.TryGetComponent<DragItem>(out dragItem))){
            SoundManager.instance.RightAction();
        }

        item.ifCollect = true;
        //Destroy(item.gameObject);
    }

    public void GetStick(StickItem item)
    {
        // Debug.Log("Item collected: " + item.itemData.itemName);
        // 使用 ScriptableObject.CreateInstance 来创建一个新的 CollectionItemData 实例
        CollectionItemData cloneItemData = ScriptableObject.CreateInstance<CollectionItemData>();

        // 复制属性
        cloneItemData.itemName = item.itemData.itemName;
        cloneItemData.itemSprite = item.itemData.itemSprite;
        cloneItemData.itemDetail = item.itemData.itemDetail;

        CollectionManager.instance.AddStick(cloneItemData);
        SoundManager.instance.GetSticker();
        Destroy(item.gameObject);
    }

    // 调用 GameOver
    public void TriggerGameOver()
    {
        UIManager.instance.GameOver();
    }

    public void ShowItemMessage(CollectionItem item)
    {
        UIManager.instance.CollectionDetailPanel.SetActive(true);
        CollectionManager.instance.RefreshDetailUI(item);
    }

    public void OpenExternalLink()
    {
        Application.OpenURL("https://www.youtube.com/watch?v=XpBL_barOLo");
    }
}
