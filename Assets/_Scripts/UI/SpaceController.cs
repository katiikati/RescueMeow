using UnityEngine;
using TMPro;

public class SpaceController : MonoBehaviour
{
    private TextMeshProUGUI pressSpaceText;
    void Start()
    {
        pressSpaceText = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
            float alpha = Mathf.Abs(Mathf.Sin(Time.time * 2f));
            Color color = pressSpaceText.color;
            color.a = alpha;
            pressSpaceText.color = color;
        }
    }
}
