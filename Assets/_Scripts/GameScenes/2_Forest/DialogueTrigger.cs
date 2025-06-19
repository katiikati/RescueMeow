using UnityEngine;
using System.Collections;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private bool canTrigger = true;

    [Header("Emote Animator")]
    [SerializeField] private Animator emoteAnimator;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;
    [SerializeField] private string startNode;
    [SerializeField] private GameObject hole;
    [SerializeField] private Animator catAnimator;
    private int itemsUsed = 0;
    private bool playerInRange = true;

    private void Awake()
    {
        hole.SetActive(true);
        hole.GetComponent<Renderer>().enabled = true;
    }

    private void Update()
    {
        if (playerInRange && canTrigger)
        {
            if (!DialogueManager.GetInstance().dialogueIsPlaying)
            {
                if (InputManager.GetInstance().GetInteractPressed())
                {
                    DialogueManager.GetInstance().EnterDialogueMode(inkJSON, emoteAnimator, startNode);
                }
            }
            else if (InputManager.GetInstance().GetInteractPressed())
            {
                if (DialogueManager.GetInstance().canContinueToNextLine)
                {
                    DialogueManager.GetInstance().ContinueStory();
                }
                else
                {
                    DialogueManager.GetInstance().skipTyping = true;
                }
            }
        }
    }

    public bool checkCanDrop(Item item)
    {
        if (item == null)
        {
            Debug.LogWarning("Item is null, cannot check drop condition.");
            return false;
        }

        switch (item.itemName)
        {
            case "carrier":
                return itemsUsed == 2;
            case "treat":
                return itemsUsed == 1;
            case "flashlight":
                return itemsUsed == 0;
            default:
                Debug.LogWarning("Unknown item type: " + item.itemName);
                return false;
        }
    }

    public void dropItem(Item item)
    {
        Debug.Log("Dropping item: " + item.name);
        if (item != null)
        {
            string itemName = item.itemName;
            Destroy(item.gameObject);
            Debug.Log("Item dropped: " + item.name);
            if (DialogueManager.GetInstance().dialogueIsPlaying)
            {
                StartCoroutine(EndingDialogue(itemName));
            }
            else
            {
                dialogueSwitcher(itemName);
            }
        }
    }

    public void cantDropYet()
    {
        DialogueManager.GetInstance().EnterDialogueMode(inkJSON, emoteAnimator, "cantUseYet");
    }

    public bool getPlayerInRange()
    {
        return playerInRange;
    }

    public IEnumerator EndingDialogue(string itemName)
    {
        yield return StartCoroutine(DialogueManager.GetInstance().ExitDialogueMode());
        dialogueSwitcher(itemName);
    }

    private void dialogueSwitcher(string itemName)
    {
        switch (itemName)
        {
            case "carrier":
                if (itemsUsed == 2)
                {
                    DialogueManager.GetInstance().EnterDialogueMode(inkJSON, emoteAnimator, "carrierUsed");
                    itemsUsed = 3;
                }
                else
                {
                    DialogueManager.GetInstance().EnterDialogueMode(inkJSON, emoteAnimator, "cantUseYet");
                }
                break;
            case "treat":
                if (itemsUsed == 1)
                {
                    DialogueManager.GetInstance().EnterDialogueMode(inkJSON, emoteAnimator, "treatUsed");
                    itemsUsed = 2;
                    catAnimator.Play("CatOut");
                }
                else
                {
                    DialogueManager.GetInstance().EnterDialogueMode(inkJSON, emoteAnimator, "cantUseYet");
                }
                break;
            case "flashlight":
                itemsUsed = 1;
                hole.GetComponent<Renderer>().enabled = false;
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON, emoteAnimator, "flashlightUsed");
                break;
            default:
                Debug.LogWarning("Unknown item type: " + itemName);
                break;
        }
    }
}
