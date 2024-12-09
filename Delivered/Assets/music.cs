using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public AudioSource audioSource;
    public AudioClip[] tracks; // 你的音乐曲目

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        PlayMusic(0); // 默认播放第一首音乐
    }

    public void PlayMusic(int trackIndex)
    {
        if (trackIndex >= 0 && trackIndex < tracks.Length)
        {
            audioSource.clip = tracks[trackIndex];
            audioSource.loop = true; // 循环播放
            audioSource.Play();
        }
    }
}