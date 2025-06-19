using UnityEngine;
using UnityEngine.UI;
public class BackgroundMusicManager : MonoBehaviour
{
    private static BackgroundMusicManager Instance;
    private AudioSource audioSource;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private float fullVolume = 0.15f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            audioSource = GetComponent<AudioSource>();
            audioSource.volume = fullVolume;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        sfxSlider.onValueChanged.AddListener(delegate { OnValueChanged(); });
    }

    public static void SetVolume(float volume)
    {
        Instance.audioSource.volume = volume*Instance.fullVolume;
    }

    public static void OnValueChanged()
    {
        SetVolume(Instance.sfxSlider.value);
    }
}