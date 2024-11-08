using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "SOs/CollectionItemData")]
public class CollectionItemData : ScriptableObject
{
    [SerializeField]
    public string itemName = string.Empty;
    [SerializeField]
    public Sprite itemSprite ;  
    [SerializeField]
    public string itemDetail = string.Empty;
    
}
public class CollectionItem : MonoBehaviour
{
    public CollectionItemData itemData;

    public bool ifNeedDynamicEvent = false;

    private void Awake()
    {
        if (ifNeedDynamicEvent)
        {
            // 获取或添加 EventTrigger 组件
            EventTrigger eventTrigger = gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = gameObject.AddComponent<EventTrigger>();
            }

            // 动态添加 PointerClick 事件
            AddEventTrigger(eventTrigger, () => ClickManager.instance.ShowItemMessage(this), EventTriggerType.PointerClick);

        }

    }

    private void AddEventTrigger(EventTrigger trigger, System.Action action, EventTriggerType eventType)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = eventType };
        entry.callback.AddListener((eventData) => action()); // 添加回调
        trigger.triggers.Add(entry);
    }
}
