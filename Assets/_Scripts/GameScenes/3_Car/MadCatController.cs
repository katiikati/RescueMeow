using UnityEngine;
using System.Collections;

public class MadCatController : MonoBehaviour
{
    [Header("Emote Animator")]
    [SerializeField] private Animator emoteAnimator;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

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

    public void EnterDialogue()
    {
        DialogueManager.GetInstance().EnterDialogueMode(inkJSON, emoteAnimator);
    }

}
