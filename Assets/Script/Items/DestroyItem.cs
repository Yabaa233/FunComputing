using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DestrotyItem : MonoBehaviour
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
        AddEventTrigger(eventTrigger, () => ClickManager.instance.DestroyItem(this.gameObject), EventTriggerType.PointerClick);
        AddEventTrigger(eventTrigger, () => ClickManager.instance.ResetItemMaterial(this.gameObject), EventTriggerType.PointerExit);
        AddEventTrigger(eventTrigger, () => ClickManager.instance.HighlightItem(this.gameObject), EventTriggerType.PointerEnter);

    }

    private void AddEventTrigger(EventTrigger trigger, System.Action action, EventTriggerType eventType)
    {
        // 查找是否已存在相同类型的事件
        EventTrigger.Entry existingEntry = trigger.triggers.Find(entry => entry.eventID == eventType);

        if (existingEntry != null)
        {
            // 如果存在，直接在现有事件的回调中添加新事件
            existingEntry.callback.AddListener((eventData) => action());
        }
        else
        {
            // 如果不存在，创建一个新的事件条目
            EventTrigger.Entry newEntry = new EventTrigger.Entry { eventID = eventType };
            newEntry.callback.AddListener((eventData) => action());
            trigger.triggers.Add(newEntry);
        }
    }

    private void AddEventTrigger(EventTrigger trigger, System.Action[] actions, EventTriggerType eventType)
    {
        // 查找是否已存在相同类型的事件
        EventTrigger.Entry existingEntry = trigger.triggers.Find(entry => entry.eventID == eventType);

        if (existingEntry != null)
        {
            // 如果事件类型已存在，直接将每个动作添加到现有的回调
            foreach (var action in actions)
            {
                existingEntry.callback.AddListener((eventData) => action());
            }
        }
        else
        {
            // 如果事件类型不存在，创建一个新的事件条目
            EventTrigger.Entry newEntry = new EventTrigger.Entry { eventID = eventType };
            newEntry.callback.AddListener((eventData) =>
            {
                foreach (var action in actions)
                {
                    action();
                }
            });
            trigger.triggers.Add(newEntry);
        }
    }

}
