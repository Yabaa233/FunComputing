using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu(menuName = "SOs/DragItemData")]
public class DragItemData : ScriptableObject
{
    [SerializeField]
    public string itemName = string.Empty;
    //[SerializeField]
    //public Sprite itemSprite;
    //[SerializeField]
    //public string itemDetail = string.Empty;

    [SerializeField]
    public string binName = string.Empty;

}
