using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance;
    private int currentSceneIndex = 0;
    [SerializeField] private GameObject[] scenes;
    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject pinkSlide;
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        scenes[0].SetActive(true);
        pinkSlide.SetActive(false);
        for (int i = 1; i < scenes.Length; i++)
        {
            scenes[i].SetActive(false);
        }
        inventory.SetActive(false);
        dialoguePanel.GetComponent<RectTransform>().position = new Vector3(960, 40, 0);
    }

    public static GameSceneManager GetInstance()
    {
        return Instance;
    }
    public void ChangeGameScene()
    {
        currentSceneIndex++;
        Debug.Log("Changing game scene to index: " + currentSceneIndex);
        for (int i = 0; i < scenes.Length; i++)
        {
            scenes[i].SetActive(currentSceneIndex == i);
        }

        if (currentSceneIndex == 1 || currentSceneIndex == 2)
        {
            inventory.SetActive(true);
            dialoguePanel.GetComponent<RectTransform>().position = new Vector3(960, 240, 0);
        }
        else
        {
            inventory.SetActive(false);
            dialoguePanel.GetComponent<RectTransform>().position = new Vector3(960, 40, 0);
        }

        if (currentSceneIndex < 3 && currentSceneIndex != 5)
        {
            pinkSlide.SetActive(false);
        }
        else
        {
            pinkSlide.SetActive(true);
        }
    }

    public int GetCurrentSceneIndex()
    {
        return currentSceneIndex;
    }
}
