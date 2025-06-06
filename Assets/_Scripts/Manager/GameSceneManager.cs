using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance;
    private int currentSceneIndex = 0;
    [SerializeField] private GameObject[] scenes;
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
        for (int i = 1; i < scenes.Length; i++)
        {
            scenes[i].SetActive(false);
        }
    }
    public void ChangeGameScene()
    {
        currentSceneIndex++;
        Debug.Log("Changing game scene to index: " + currentSceneIndex);
        for (int i = 0; i < scenes.Length; i++)
        {
            scenes[i].SetActive(currentSceneIndex == i);
        }
    }
}
