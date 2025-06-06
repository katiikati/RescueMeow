using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFader : MonoBehaviour
{
    public static SceneFader Instance;
    [SerializeField] private CanvasGroup blackOutPanel;
    [SerializeField] private CanvasGroup fadeCanvasGroup;
    [SerializeField] private float fadeDuration = 1f;

    private void Awake()
    {
        blackOutPanel.alpha = 0;
        blackOutPanel.gameObject.SetActive(false);
        fadeCanvasGroup.alpha = 1; 
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator BlackOut()
    {
        blackOutPanel.gameObject.SetActive(true);
        yield return StartCoroutine(Fade(0, 1, blackOutPanel));
        yield return new WaitForSeconds(1);
        GameSceneManager.Instance.ChangeGameScene();
        StartCoroutine(Fade(1, 0, blackOutPanel));
        InputManager.Instance.EnableInteract();
    }

    public void StartBlackOut()
    {
        StartCoroutine(Fade(0, 1, blackOutPanel));
    }

    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeScene(sceneName));
    }

    private IEnumerator FadeScene(string sceneName)
    {
        yield return StartCoroutine(Fade(1, 0, fadeCanvasGroup));
        yield return null;
        yield return SceneManager.LoadSceneAsync(sceneName);
        yield return StartCoroutine(Fade(0, 1, fadeCanvasGroup));
        gameObject.SetActive(false);
    }

    private IEnumerator Fade(float from, float to, CanvasGroup canvasGroup)
    {
        float time = 0f;
        while (time < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(from, to, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = to;
        if(to == 0)
        {
            canvasGroup.gameObject.SetActive(false);
        }
        else
        {
            canvasGroup.gameObject.SetActive(true);
        }
    }

    public void StartFade(string sceneName)
    {
        Instance.FadeToScene(sceneName);
    }
}
