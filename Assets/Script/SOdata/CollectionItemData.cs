using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SOs/CollectionItemData")]
public class CollectionItemData : ScriptableObject
{
    [SerializeField]
    public string itemName = string.Empty;
    [SerializeField]
    public Sprite itemSprite;
    [SerializeField]
    public string itemDetail = string.Empty;

}
