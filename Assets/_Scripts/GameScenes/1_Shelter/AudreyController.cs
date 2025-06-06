using System.Collections;
using UnityEngine;

public class AudreyController : MonoBehaviour
{
    [Header("Emote Animator")]
    [SerializeField] private Animator emoteAnimator;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;
    private int itemsAddedToHotbar = 0;

    private void Start()
    {
        DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
    }
    private void Update()
    {
        if (!DialogueManager.GetInstance().dialogueIsPlaying)
        {
            if (InputManager.GetInstance().GetInteractPressed())
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
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
    public void addItemToHotbar()
    {
        itemsAddedToHotbar++;
        if (itemsAddedToHotbar == 3)
        {
            if (DialogueManager.GetInstance().dialogueIsPlaying)
            {
                StartCoroutine(EndingDialogue());
            }
            else
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON, emoteAnimator, "gotAllItems");
            }
        }
    }

    public IEnumerator EndingDialogue() 
    {
        yield return StartCoroutine(DialogueManager.GetInstance().ExitDialogueMode());
        DialogueManager.GetInstance().EnterDialogueMode(inkJSON, emoteAnimator, "gotAllItems");
    }
    
}
