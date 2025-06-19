using UnityEngine;
using UnityEngine.UI;

public class HappyCat : MonoBehaviour
{
    [Header("Emote Animator")]
    [SerializeField] private Animator emoteAnimator;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    [SerializeField] private GameObject thanksText;
    private void Start()
    {
        DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
        SlideController.GetInstance().setSliderFull();
    }
    private void Update()
    {
        if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            thanksText.SetActive(false);
            if (InputManager.GetInstance().GetInteractPressed())
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
            thanksText.SetActive(true);
        }

    }
}
