using UnityEngine.EventSystems;
using UnityEngine;

public class GameOverItem : MonoBehaviour
{

    private void Awake()
    {
        // 获取或添加 EventTrigger 组件
        EventTrigger eventTrigger = gameObject.GetComponent<EventTrigger>();
        if (eventTrigger == null)
        {
            eventTrigger = gameObject.AddComponent<EventTrigger>();
        }

        // 动态添加 Pointer事件
        AddEventTrigger(eventTrigger, () => ClickManager.instance.TriggerGameOver(), EventTriggerType.PointerClick);
        AddEventTrigger(eventTrigger, () => ClickManager.instance.ResetItemMaterial(this.gameObject), EventTriggerType.PointerExit);
        AddEventTrigger(eventTrigger, () => ClickManager.instance.HighlightItem(this.gameObject), EventTriggerType.PointerEnter);

    }

    private void AddEventTrigger(EventTrigger trigger, System.Action action, EventTriggerType eventType)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = eventType };
        entry.callback.AddListener((eventData) => action()); // 添加回调
        trigger.triggers.Add(entry);
    }
}