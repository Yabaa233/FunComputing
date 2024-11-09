using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectionManager : MonoBehaviour
{
    public static CollectionManager instance;

    private int MaxColCount;
    private int CurColCount;

    public TMP_Text CollectionText;
    public TMP_Text DetailText;

    public List<CollectionItemData> collectedItemList;

    public GameObject UIParent;
    public GameObject itemPrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        collectedItemList = new List<CollectionItemData>();
        MaxColCount = CountCollectionItemsInScene();
    }

    public void AddItem(CollectionItemData itemData)
    {
        //CollectionItem item = new CollectionItem();
        //item.itemData = itemData;
        collectedItemList.Add(itemData);
    }

    public void RefreshItemUI()
    {
        // 清空 UIParent 下的所有子物体
        foreach (Transform child in UIParent.transform)
        {
            Destroy(child.gameObject);
        }

        // 清空 DetailText 的内容
        DetailText.text = string.Empty;

        CollectionText.text = collectedItemList.Count.ToString()+"/" + MaxColCount.ToString();

        if (collectedItemList.Count > 0)
        {
            foreach (CollectionItemData item in collectedItemList)
            {
                // 实例化预制体
                GameObject newUIElement = Instantiate(itemPrefab);

                // 设置生成物体的父对象为指定的 UI 父对象
                newUIElement.transform.SetParent(UIParent.transform, false);

                // 获取并设置 CollectionItem 组件的数据
                var preItem = newUIElement.GetComponent<CollectionItem>();
                if (preItem != null)
                {
                    preItem.itemData = item;
                }

                // 获取并设置 Image 组件的图片
                var preImage = newUIElement.GetComponent<Image>();
                if (preImage != null)
                {
                    preImage.sprite = item.itemSprite;
                }
            }
        }
    }



    public void RefreshDetailUI(string detail)
    {
        DetailText.text = detail;
    }

    public int CountCollectionItemsInScene()
    {
        int count = 0;

        // 获取所有包含 CollectionItem 的实例，无论是否 active
        CollectionItem[] allCollectionItems = Resources.FindObjectsOfTypeAll<CollectionItem>();

        // 遍历每个 CollectionItem 实例
        foreach (CollectionItem item in allCollectionItems)
        {
            // 检查 ifInScene 是否为 true
            if (item.ifInScene)
            {
                count++;
            }
        }

        return count;
    }
}
