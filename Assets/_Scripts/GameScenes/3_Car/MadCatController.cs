using TMPro;
using UnityEngine;

public class MadCatController : MonoBehaviour
{
    [Header("Emote Animator")]
    [SerializeField] private Animator emoteAnimator;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private GameObject continueArrow;
    private bool showingTextField = false;
    private static MadCatController instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        nameInputField.gameObject.SetActive(false);
    }
    public static MadCatController GetInstance()
    {
        return instance;
    }
    private void Start()
    {
        DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
    }
    private void Update()
    {
        if (!showingTextField)
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
        else
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            {
                if (!string.IsNullOrEmpty(nameInputField.text))
                {
                    SaveName();
                }
            }
        }
    }

    public void EnterDialogue()
    {
        DialogueManager.GetInstance().EnterDialogueMode(inkJSON, emoteAnimator);
    }

    public void SaveName()
    {
        string catName = nameInputField.text;
        if (!string.IsNullOrEmpty(catName))
        {
            DialogueManager.GetInstance().saveCatName(catName);
        }
        nameInputField.gameObject.SetActive(false);
        showingTextField = false;
        InputManager.GetInstance().EnableInteract();
        continueArrow.SetActive(true);
        DialogueManager.GetInstance().ContinueStory();
    }

    public void ShowNameInputField()
    {
        showingTextField = true;
        nameInputField.gameObject.SetActive(true);
        continueArrow.SetActive(false);
    }

}
