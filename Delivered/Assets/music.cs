using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public AudioSource audioSource;
    public AudioClip[] tracks; // ���������Ŀ

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
        PlayMusic(0); // Ĭ�ϲ��ŵ�һ������
    }

    public void PlayMusic(int trackIndex)
    {
        if (trackIndex >= 0 && trackIndex < tracks.Length)
        {
            audioSource.clip = tracks[trackIndex];
            audioSource.loop = true; // ѭ������
            audioSource.Play();
        }
    }
}