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

    public TMP_Text CountCollectionText;

    public Text Detail_DetailText;
    public Text Detail_NameText;
    public Image Detail_Image;



    public List<CollectionItemData> collectedItemDataList;
    public List<CollectionItemData> collectedStickDataList;


    public GameObject UIParent;
    public GameObject itemPrefab;

    public GameObject StickUIParent;
    public GameObject StickItemPrefab;

    private int curStickShow;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        curStickShow = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        collectedItemDataList = new List<CollectionItemData>();
        collectedStickDataList = new List<CollectionItemData>();
        MaxColCount = CountCollectionItemsInScene();
    }

    public void AddItem(CollectionItemData itemData)
    {
        //CollectionItem item = new CollectionItem();
        //item.itemData = itemData;
        collectedItemDataList.Add(itemData);
    }

    public void AddStick(CollectionItemData itemData)
    {
        //CollectionItem item = new CollectionItem();
        //item.itemData = itemData;
        collectedStickDataList.Add(itemData);
    }

    public void RefreshCollectionItemUI()
    {
        // 清空 UIParent 下的所有子物体
        foreach (Transform child in UIParent.transform)
        {
            Destroy(child.gameObject);
        }

        CountCollectionText.text = collectedItemDataList.Count.ToString()+"/" + MaxColCount.ToString();

        if (collectedItemDataList.Count > 0)
        {
            foreach (CollectionItemData itemData in collectedItemDataList)
            {
                // 实例化预制体
                GameObject newUIElement = Instantiate(itemPrefab);

                // 设置生成物体的父对象为指定的 UI 父对象
                newUIElement.transform.SetParent(UIParent.transform, false);

                // 获取并设置 CollectionItem 组件的数据
                var newItem = newUIElement.GetComponent<CollectionItem>();
                if (newItem != null)
                {
                    newItem.itemData = itemData;
                    newItem.NameText.text = itemData.itemName;
                    newItem.Image.sprite = itemData.itemSprite;
                }


            }
        }
    }

    public void ShowFirstStickItemUI()
    {
        // 清空 UIParent 下的所有子物体
        foreach (Transform child in StickUIParent.transform)
        {
            Destroy(child.gameObject);
        }

        if (collectedStickDataList.Count > 0)
        {
            var itemData= collectedStickDataList[0];
            curStickShow = 0;
                // 实例化预制体
                GameObject newUIElement = Instantiate(StickItemPrefab);

                // 设置生成物体的父对象为指定的 UI 父对象
                newUIElement.transform.SetParent(StickUIParent.transform, false);

                // 获取并设置 CollectionItem 组件的数据
                var newItem = newUIElement.GetComponent<StickItem>();
                if (newItem != null)
                {
                    newItem.itemData = itemData;
                    newItem.NameText.text = itemData.itemName;
                    newItem.DetailText.text = itemData.itemDetail;
                    newItem.Image.sprite = itemData.itemSprite;
                }          
        }
    }
    public void ShowNextStickItemUI()
    {
        // 清空 StickUIParent 下的所有子物体
        foreach (Transform child in StickUIParent.transform)
        {
            Destroy(child.gameObject);
        }

        // 确保当前索引在范围内
        if (curStickShow < collectedStickDataList.Count - 1)
        {
            curStickShow++;
            var itemData = collectedStickDataList[curStickShow];

            // 实例化预制体
            GameObject newUIElement = Instantiate(StickItemPrefab);
            newUIElement.transform.SetParent(StickUIParent.transform, false);

            var newItem = newUIElement.GetComponent<StickItem>();
            if (newItem != null)
            {
                newItem.itemData = itemData;
                newItem.NameText.text = itemData.itemName;
                newItem.DetailText.text = itemData.itemDetail;
                newItem.Image.sprite = itemData.itemSprite;
            }
        }
    }

    public void ShowPreStickItemUI()
    {
        // 清空 StickUIParent 下的所有子物体
        foreach (Transform child in StickUIParent.transform)
        {
            Destroy(child.gameObject);
        }

        // 确保当前索引在范围内
        if (curStickShow > 0)
        {
            curStickShow--;
            var itemData = collectedStickDataList[curStickShow];

            // 实例化预制体
            GameObject newUIElement = Instantiate(StickItemPrefab);
            newUIElement.transform.SetParent(StickUIParent.transform, false);

            var newItem = newUIElement.GetComponent<StickItem>();
            if (newItem != null)
            {
                newItem.itemData = itemData;
                newItem.NameText.text = itemData.itemName;
                newItem.DetailText.text = itemData.itemDetail;
                newItem.Image.sprite = itemData.itemSprite;
            }
        }
    }


    public void RefreshDetailUI(CollectionItem detail)
    {
        Detail_DetailText.text = detail.itemData.itemDetail;
        Detail_NameText.text = detail.itemData.itemName;
        Detail_Image.sprite = detail.itemData.itemSprite;

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
