using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public int ID;
    public string itemName;

    private void Awake()
    {
    }
    public virtual void UseItem(GameObject NPC, int index)
    {
        Debug.Log("Using item: " + itemName);

        switch (itemName)
        {
            case "waterBucket":
                Debug.Log("Using waterBucket");
                Destroy(gameObject);
                break;
            default:
                Debug.Log("Using default item");
                break;
        }
    }
    public virtual void PickUp()
    {
        Sprite itemIcon = GetComponent<Image>().sprite;
    }
}
