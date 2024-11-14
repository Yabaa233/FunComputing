using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour
{
    [SerializeField]
    public string binName = string.Empty;

    public Vector3 initPos;
    private Canvas dragCanvas;
    private int originalSortingOrder;
    private Camera uiCamera;

    public bool IfInDrag = false;

    private void Awake()
    {
        IfInDrag = false;
        // Record initial position as local coordinates
        initPos = transform.localPosition;

        // Get the main camera or assign a specific UI camera
        uiCamera = Camera.main;

        EventTrigger eventTrigger = gameObject.GetComponent<EventTrigger>();
        if (eventTrigger == null)
        {
            eventTrigger = gameObject.AddComponent<EventTrigger>();
        }

        // Dynamically add PointerDown event
        AddEventTrigger(eventTrigger,  EventTriggerType.PointerClick ,(data) => OnPointerClick((PointerEventData)data));


    }

    public void ResetPos()
    {
        // Reset to the initial local position
        IfInDrag = false; // Exit dragging state
        RectTransform rect = new RectTransform();
        if (gameObject.TryGetComponent<RectTransform>(out rect))
        {
            rect.anchoredPosition3D = initPos;
        }


        if (dragCanvas != null)
        {
            DestroyImmediate(dragCanvas);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(GamePlayManager.instance.curDragItem!=null)
        {
            if ((GamePlayManager.instance.curDragItem.
                gameObject.GetComponent<CollectionItem>().itemData.itemName !=
                gameObject.GetComponent<CollectionItem>().itemData.itemName)
                && GamePlayManager.instance.curDragItem.isActiveAndEnabled
                && GamePlayManager.instance.curDragItem!= GamePlayManager.instance.EmptyDragItem)
                return;
        }
        // If left-clicked, enter dragging state
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            GamePlayManager.instance.StatDrag(this);  // Start dragging
            IfInDrag = true; // Set to dragging state

            // Bring to top by adjusting the sorting order
            dragCanvas = gameObject.GetComponent<Canvas>();
            if (dragCanvas == null)
            {
                dragCanvas = gameObject.AddComponent<Canvas>();
                dragCanvas.overrideSorting = true;
            }
            originalSortingOrder = dragCanvas.sortingOrder;
            dragCanvas.sortingOrder = 100; // Set a high sorting order to bring it to the top
        }
    }

    private void Update()
    {
        // Follow the mouse while in dragging state
        if (IfInDrag)
        {
            RectTransform rectTransform = GetComponent<RectTransform>();

            Vector2 localPointerPosition;
            Vector2 screenPoint = Input.mousePosition;

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    rectTransform.parent as RectTransform, screenPoint, uiCamera, out localPointerPosition))
            {
                // Set UI element position to follow the mouse
                rectTransform.anchoredPosition = localPointerPosition;
            }

            // If right-clicked, reset position and exit dragging state
            if (Input.GetMouseButtonDown(1))
            {
                ResetPos();
            }
        }
    }

    private void AddEventTrigger(EventTrigger trigger, EventTriggerType eventType, UnityEngine.Events.UnityAction<UnityEngine.EventSystems.BaseEventData> action)
    {
        EventTrigger.Entry existingEntry = trigger.triggers.Find(entry => entry.eventID == eventType);

        if (existingEntry != null)
        {
            existingEntry.callback.AddListener(action);
        }
        else
        {
            EventTrigger.Entry newEntry = new EventTrigger.Entry { eventID = eventType };
            newEntry.callback.AddListener(action);
            trigger.triggers.Add(newEntry);
        }
    }

}
