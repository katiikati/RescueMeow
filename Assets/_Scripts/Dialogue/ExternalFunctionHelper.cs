using UnityEngine;

public class ExternalFunctionHelper : MonoBehaviour
{
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
}