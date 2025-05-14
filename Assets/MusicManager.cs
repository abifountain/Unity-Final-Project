using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager Instance; 
    private AudioSource audioSource; 
    public AudioClip backgroundMusic;

    private void Awake() 
    {
        if (Instance == null) 
        {
            Instance = this; 
            audioSource = GetComponent<AudioSource>(); 
            DontDestroyOnLoad(gameObject); 
        }
        else 
        {
            Destroy(gameObject); 
        }
    }

    void Start()
    {
        if(backgroundMusic != null) 
        {
            PlayBackgroundMusic(false, backgroundMusic); 
        }
    }

    public static void PlayBackgroundMusic(bool resetSong, AudioClip audioClip = null) 
    {
        if (audioClip != null && Instance.audioSource.clip != audioClip)
        {
            Instance.audioSource.clip = audioClip; 
            resetSong = true; 
        }
        if (Instance.audioSource.clip != null) 
        {
            if (resetSong) 
            {
                Instance.audioSource.Stop(); 
                Instance.audioSource.Play(); 
            }
            else if (!Instance.audioSource.isPlaying) 
            {
                Instance.audioSource.Play(); 
            }
        }
    }

    public static void PauseBackgroundMusic()
    {
        Instance.audioSource.Pause(); 
    }

    public static void StopBackgroundMusic()
    {
        if (Instance != null && Instance.backgroundMusic != null) 
        {
            Instance.audioSource.Stop();
            Destroy(Instance.gameObject);
        }
    }

    public static bool CheckBackgroundMusic()
    {
        if(Instance != null && Instance.backgroundMusic != null) 
        {
            return true; 
        }
        else 
        {
            return false; 
        }
    }
}
