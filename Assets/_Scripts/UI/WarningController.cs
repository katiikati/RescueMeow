using UnityEngine;
using System;
using System.Collections;
using TMPro;

public class WarningController : MonoBehaviour
{
    [SerializeField] private GameObject warningPrefab;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private float waitDuration = 10f;
    public void addWarning(String warningText)
    {
        Debug.Log("Warning: " + warningText);
        GameObject newWarningPrefab = Instantiate(warningPrefab, gameObject.transform);
        if(newWarningPrefab == null)
        {
            Debug.LogError("Warning prefab is null.");
            return;
        }
        if(newWarningPrefab.GetComponentInChildren<TextMeshProUGUI>() == null)
        {
            Debug.LogError("Warning prefab does not contain a TextMeshProUGUI component.");
            return;
        }
        newWarningPrefab.GetComponentInChildren<TextMeshProUGUI>().text = warningText;

        StartCoroutine(FadeInThenFadeOut(newWarningPrefab.GetComponent<CanvasGroup>()));
    }

    IEnumerator FadeInThenFadeOut(CanvasGroup cg)
    {
        Debug.Log("Fading in and out");
        StartCoroutine(FadeCanvasGroup(cg, true));
        yield return new WaitForSeconds(waitDuration);
        StartCoroutine(FadeCanvasGroup(cg, false));
    }

    IEnumerator FadeCanvasGroup(CanvasGroup cg, bool fadeIn, System.Action onComplete = null)
    {
        float start = fadeIn ? 0f : cg.alpha;
        float end = fadeIn ? cg.alpha : 0f;
        float t = 0f;

        cg.interactable = fadeIn;
        cg.blocksRaycasts = fadeIn;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            cg.alpha = Mathf.Lerp(start, end, t / fadeDuration);
            yield return null;
        }

        cg.alpha = end;

        if (!fadeIn)
        {
            Destroy(cg.gameObject);
        }

        onComplete?.Invoke();
    }
}
