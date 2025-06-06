using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    public void ChangeScene(string sceneName)
    {
        Debug.Log($"Changing scene to: {sceneName}");
        SceneManager.LoadScene(sceneName);
    }
}

