using UnityEngine;

public class ExternalFunctionHelper : MonoBehaviour
{
    private static ExternalFunctionHelper instance;

    public void Awake()
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
    }
    public static ExternalFunctionHelper GetInstance()
    {
        return instance;
    }
    public void nextScene()
    {
        SceneFader.Instance.FadeToScene("ForestScene");
    }

    public void blackOut()
    {
        Debug.Log("Blackout");
        InputManager.Instance.DisableInteract();
        StartCoroutine(SceneFader.Instance.BlackOut());
    }

    public void nameCat()
    {
        InputManager.Instance.DisableInteract();
        MadCatController.GetInstance().ShowNameInputField();
    }
}