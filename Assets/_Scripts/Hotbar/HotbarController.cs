using UnityEngine;
using UnityEngine.InputSystem;

public class HotbarController : MonoBehaviour
{
    public GameObject hotbarPanel;
    public GameObject slotPrefab;
    public int slotCount = 10;
    private Key[] hotbarKeys;
    [SerializeField] private GameObject dialoguePanel;
    [Header("Prefabs")]
    [SerializeField] private GameObject carrierPrefab;
    [SerializeField] private GameObject treatPrefab;
    [SerializeField] private GameObject flashlightPrefab;
    private void Awake()
    {
        hotbarKeys = new Key[slotCount];
        for (int i = 0; i < slotCount; i++)
        {
            hotbarKeys[i] = i < 9 ? (Key)((int)Key.Digit1 + i) : Key.Digit0;
        }

        for (int i = 0; i < slotCount; i++)
        {
            Slot slot = Instantiate(slotPrefab, hotbarPanel.transform).GetComponent<Slot>();
            slot.slotIndex = i;
        }
    }

    void Update()
    {
        for (int i = 0; i < slotCount; i++)
        {
            if (Keyboard.current[hotbarKeys[i]].wasPressedThisFrame)
            {
                //Use Item
                UseItemInSlot(i);
            }
        }

    }

    void UseItemInSlot(int index)
    {
        Slot slot = hotbarPanel.transform.GetChild(index).GetComponent<Slot>();
        if (slot.currentItem != null)
        {
            Item item = slot.currentItem.GetComponent<Item>();
        }
    }

    public bool AddItem(GameObject itemPrefab)
    {
        Debug.Log("Adding item prefab: " + itemPrefab.name);
        foreach (Transform slot in hotbarPanel.transform)
        {
            Slot slotComponent = slot.GetComponent<Slot>();
            if (slotComponent != null && slotComponent.currentItem == null)
            {
                GameObject itemObject = Instantiate(itemPrefab, slot);
                Item item = itemObject.GetComponent<Item>();
                RectTransform rect = item.GetComponent<RectTransform>();
                item.GetComponent<ItemDragHandler>().setDialoguePanel(dialoguePanel);
                rect.anchoredPosition = Vector2.zero;
                rect.sizeDelta = new Vector2(80, 80);
                slotComponent.currentItem = item;
                return true;
            }
        }
        return false;
    }

    public void AddItem(string itemName)
    {
        switch (itemName)
        {
            case "carrier":
                AddItem(carrierPrefab);
                break;
            case "treat":
                AddItem(treatPrefab);
                break;
            case "flashlight":
                AddItem(flashlightPrefab);
                break;
            default:
                Debug.LogWarning("Unknown item name: " + itemName);
                break;
        }
    }

}
