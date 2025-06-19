using System.Collections;
using UnityEngine;

public class FindCatController : MonoBehaviour
{
    [Header("Emote Animator")]
    [SerializeField] private Animator emoteAnimator;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    private SpriteRenderer sr;
    private Color originalColor;

    [Tooltip("Color when hovered")]
    [SerializeField] private Color highlightColor;
    private Animator catAnimator;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
        catAnimator = GetComponent<Animator>();
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
        catAnimator.Play("CatJump");
        if(DialogueManager.GetInstance().dialogueIsPlaying)
        {
            StartCoroutine(ExitDialogue());
        }
        else
        {
            DialogueManager.GetInstance().EnterDialogueMode(inkJSON, emoteAnimator, "foundCat");
        }
    }
    private IEnumerator ExitDialogue()
    {
        yield return StartCoroutine(DialogueManager.GetInstance().ExitDialogueMode());
        DialogueManager.GetInstance().EnterDialogueMode(inkJSON, emoteAnimator, "foundCat");
    }
}
