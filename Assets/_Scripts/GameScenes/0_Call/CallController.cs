using UnityEngine;

public class CallController : MonoBehaviour
{
    [Header("Emote Animator")]
    [SerializeField] private Animator emoteAnimator;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    [Header("Dialogue Panel")]
    [SerializeField] private GameObject dialoguePanel;
    private bool startDialogue = false;
    [SerializeField] Animator callAnimator;

    private void Awake()
    {
        dialoguePanel.SetActive(false);
    }
    private void Update()
    {
        if (startDialogue)
        {
            if (!DialogueManager.GetInstance().dialogueIsPlaying)
            {
                if (InputManager.GetInstance().GetInteractPressed())
                {
                    dialoguePanel.SetActive(true);
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
        else
        {
            if (InputManager.GetInstance().GetInteractPressed())
            {
                startDialogue = true;
                callAnimator.Play("Call");
            }
        }
    }

}
