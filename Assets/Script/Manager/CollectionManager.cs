using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectionManager : MonoBehaviour
{
    public static CollectionManager instance;

    public int MaxColCount;
    public int CurColCount;

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

}
