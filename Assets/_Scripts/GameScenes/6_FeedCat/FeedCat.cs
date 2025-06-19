using UnityEngine;
using System.Collections;
public class FeedCat : MonoBehaviour
{
    [Header("Emote Animator")]
    [SerializeField] private Animator emoteAnimator;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    [SerializeField] private GameObject foodButton;
    [SerializeField] private GameObject waterButton;

    [SerializeField] private GameObject food;
    [SerializeField] private GameObject water;
    [SerializeField] private GameObject cat;

    private void Start()
    {
        foodButton.SetActive(true);
        waterButton.SetActive(true);
        DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
    }

    private void Awake()
    {
        foodButton.SetActive(false);
        waterButton.SetActive(false);
        food.SetActive(false);
        water.SetActive(false);
        cat.SetActive(false);
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

    public void OnFoodButtonPressed()
    {
        foodButton.SetActive(false);
        food.SetActive(true);

        if (food.activeSelf && water.activeSelf)
        {
            cat.SetActive(true);
            if (DialogueManager.GetInstance().dialogueIsPlaying)
            {
                StartCoroutine(ExitDialogue());
            }
            else
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON, emoteAnimator, "fedCat");
            }
            SlideController.GetInstance().incSliderValue();
        }
    }

    public void OnWaterButtonPressed()
    {
        waterButton.SetActive(false);
        water.SetActive(true);

        if (food.activeSelf && water.activeSelf)
        {
            cat.SetActive(true);
            if (DialogueManager.GetInstance().dialogueIsPlaying)
            {
                StartCoroutine(ExitDialogue());
            }
            else
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON, emoteAnimator, "fedCat");
            }
            SlideController.GetInstance().incSliderValue();
        }
    }
    private IEnumerator ExitDialogue()
    {
        yield return StartCoroutine(DialogueManager.GetInstance().ExitDialogueMode());
        DialogueManager.GetInstance().EnterDialogueMode(inkJSON, emoteAnimator, "fedCat");
    }

}
