using UnityEngine;
using UnityEngine.UI;

public class SlideController : MonoBehaviour
{
    [SerializeField] private Slider mySlider;
    [SerializeField] private float maxValue = 0.2f;
    private static SlideController instance;

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
        mySlider.value = maxValue;
    }

    public static SlideController GetInstance()
    {
        return instance;
    }
    public void incSliderValue()
    {
        if (gameObject.activeSelf)
        {
            mySlider.value += 0.2f;
        }
    }

    public void setSliderFull()
    {
        mySlider.gameObject.SetActive(true);
        mySlider.value = 1f;
    }
}
