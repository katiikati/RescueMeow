using UnityEngine;

public class CatCleaner : MonoBehaviour
{
    [Header("Emote Animator")]
    [SerializeField] private Animator emoteAnimator;
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;
    [SerializeField] private float cleanThreshold = 100f;
    [SerializeField] private GameObject dirtyOverlay;
    [SerializeField] private float cleanRate = 30f;

    private float currentClean = 0f;
    private bool isClean = false;

    public void Start()
    {
        DialogueManager.GetInstance().EnterDialogueMode(inkJSON, emoteAnimator);
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
        
        if (isClean) return;

        if (Input.GetMouseButton(0)) // mouse is held
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(mousePos);
            if (hit != null && hit.gameObject == gameObject)
            {
                currentClean += Time.deltaTime * cleanRate;
                UpdateVisuals();

                if (currentClean >= cleanThreshold)
                {
                    FinishCleaning();
                }
            }
        }
    }

    private void UpdateVisuals()
    {
        if (dirtyOverlay != null)
        {
            float alpha = Mathf.Lerp(1f, 0f, currentClean / cleanThreshold);
            Color c = dirtyOverlay.GetComponent<SpriteRenderer>().color;
            c.a = alpha;
            dirtyOverlay.GetComponent<SpriteRenderer>().color = c;
        }
    }

    private void FinishCleaning()
    {
        isClean = true;
        Debug.Log("Cat is clean!");
        DialogueManager.GetInstance().EnterDialogueMode(inkJSON, emoteAnimator, "catCleaned");
    }
}
