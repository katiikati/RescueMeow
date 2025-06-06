using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private string itemName;
    [SerializeField] private HotbarController hotbarController;
    [SerializeField] private AudreyController audreyController;
    private SpriteRenderer sr;
    private Color originalColor;

    [Tooltip("Color when hovered")]
    public Color highlightColor = Color.yellow;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }

    public void OnMouseEnter()
    {
        sr.color = highlightColor;
    }

    public void OnMouseExit()
    {
        sr.color = originalColor;
    }

    public void OnMouseDown()
    {
        hotbarController.AddItem(itemName);
        audreyController.addItemToHotbar();
        Destroy(gameObject);
    }
}
