using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    private static AudioSource audioSource;
    private static SoundEffectLibrary soundEffectLibrary;
    [SerializeField] private  Slider sfxSlider;
    [SerializeField] private string sfxSliderName = "SfxSlider";

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            audioSource= GetComponent<AudioSource>();
            soundEffectLibrary = GetComponent<SoundEffectLibrary>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void Play(string soundName)
    {
        AudioClip audioClip = soundEffectLibrary.GetRandomClip(soundName);
        if(audioClip != null)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        BindSliders();
    }

    private void BindSliders()
    {

        Slider[] allSliders = FindObjectsByType<Slider>(FindObjectsInactive.Include);

        foreach (Slider slider in allSliders)
        {
            if (slider.gameObject.name == sfxSliderName)
            {
                sfxSlider = slider;
                sfxSlider.onValueChanged.RemoveAllListeners();
                sfxSlider.value = audioSource.volume;
                sfxSlider.onValueChanged.AddListener(SetVolume);
                break;
            }
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sfxSlider.onValueChanged.AddListener(delegate { OnValueChanged(); });
    }

    public static void SetVolume(float volume)
    {
        audioSource.volume= volume;
    }

    public  void OnValueChanged()
    {
        SetVolume(sfxSlider.value);
    }
}
