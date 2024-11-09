using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private Vector2 offset;

    // 定义 X 轴的移动范围
    public float minX = -100f; // 左侧最大位置
    public float maxX = 100f;  // 右侧最大位置

    public void OnPointerDown(PointerEventData eventData)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out offset);
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        Vector2 globalMousePos;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform.parent as RectTransform, eventData.position, eventData.pressEventCamera, out globalMousePos))
        {
            // 计算新的 X 轴位置
            float newX = globalMousePos.x - offset.x;

            // 限制 X 轴位置在指定范围内
            newX = Mathf.Clamp(newX, minX, maxX);

            // 更新位置，仅更改 X 轴，保持 Y 轴不变
            rectTransform.localPosition = new Vector2(newX, rectTransform.localPosition.y);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        offset = Vector2.zero;
    }

    // 暴露的方法，用于 EventTrigger 绑定
    public void StartDrag(BaseEventData data) => OnPointerDown((PointerEventData)data);
    public void Dragging(BaseEventData data) => OnDrag((PointerEventData)data);
    public void EndDrag(BaseEventData data) => OnPointerUp((PointerEventData)data);
}
