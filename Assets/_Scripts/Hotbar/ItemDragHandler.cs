using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform originalParent;
    CanvasGroup canvasGroup;
    [SerializeField] private GameObject dialoguePanel;
    private WarningController warningController;

    void Awake()
    {
        if (warningController == null)
        {
            warningController = FindFirstObjectByType<WarningController>();
        }
    }

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void setDialoguePanel(GameObject panel)
    {
        dialoguePanel = panel;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (dialoguePanel != null && dialoguePanel.activeInHierarchy)
        {
            warningController.addWarning("You can't use items during dialogue.");
            return;
        }
        originalParent = transform.parent;
        transform.SetParent(transform.root);
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (dialoguePanel != null && dialoguePanel.activeInHierarchy)
        {
            warningController.addWarning("You can't use items during dialogue.");
            return;
        }

        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (dialoguePanel != null && dialoguePanel.activeInHierarchy)
        {
            warningController.addWarning("You can't use items during dialogue.");
            return;
        }

        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;

        Slot dropSlot = eventData.pointerEnter?.GetComponent<Slot>();
        Slot originalSlot = originalParent.GetComponent<Slot>();

        if (dropSlot == null)
        {
            GameObject dropItem = eventData.pointerEnter;
            if (dropItem != null)
            {
                dropSlot = dropItem.GetComponentInParent<Slot>();
            }
        }

        if (dropSlot != null) 
        {
            if (dropSlot.currentItem != null) 
            {
                Item tempItem = dropSlot.currentItem;
                dropSlot.currentItem = gameObject.GetComponent<Item>();
                transform.SetParent(dropSlot.transform);
                dropSlot.currentItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

                originalSlot.currentItem = tempItem;
                tempItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            }
            else if (originalSlot.currentItem != null)
            {
                dropSlot.currentItem.transform.SetParent(originalSlot.transform);
                originalSlot.currentItem = dropSlot.currentItem;
                dropSlot.currentItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            }
            else
            {
                originalSlot.currentItem = null;
            }

            transform.SetParent(dropSlot.transform);
            dropSlot.currentItem = gameObject.GetComponent<Item>();
        }
        else // if not dropped in a slot
        {
            Debug.Log("Item not dropped in a slot, checking for drop targets...");
            if (!IsWithinInventory(eventData.position))
            {
                Debug.Log("Item not dropped within inventory, checking for dialogue triggers...");
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos, Vector2.zero);

                DialogueTrigger targetTrigger = null;
                foreach (RaycastHit2D hit in hits)
                {
                    if (hit.collider != null && hit.collider.CompareTag("DropTarget") && hit.collider.isTrigger)
                    {
                        Debug.Log("Found a drop target: " + hit.collider.name);
                        DialogueTrigger trigger = hit.collider.GetComponent<DialogueTrigger>();
                        if (trigger != null)
                        {
                            targetTrigger = trigger;
                            break;
                        }
                    }
                }

                if (targetTrigger != null && targetTrigger.checkCanDrop(originalSlot.currentItem))
                {
                    targetTrigger.dropItem(originalSlot.currentItem);
                }
                else if (targetTrigger != null)
                {
                    targetTrigger.cantDropYet();
                    transform.SetParent(originalParent);
                }
                else
                {
                    Debug.Log("No valid drop target found, returning item to original parent.");
                    transform.SetParent(originalParent);
                }
            }
            else
            {
                transform.SetParent(originalParent);
            }
        }

        GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }
    bool IsWithinInventory(Vector2 mousePosition)
    {
        RectTransform inventoryRect = originalParent.parent.GetComponent<RectTransform>();
        return RectTransformUtility.RectangleContainsScreenPoint(inventoryRect, mousePosition);
    }

}
