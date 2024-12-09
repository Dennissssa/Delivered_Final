using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeLimit = 60f; // ���ü�ʱ����ʱ�䣨�룩
    private float timer;
    public TMP_Text timerText; // ������ʾʣ��ʱ��� TextMesh Pro ���

    void Start()
    {
        timer = timeLimit; // ��ʼ����ʱ��
        UpdateTimerText(); // ��ʼ����ʾ��ʱ��
    }

    void Update()
    {
        // �ݼ���ʱ��
        timer -= Time.deltaTime;

        // ����ʱ���Ƿ����
        if (timer <= 0)
        {
            LoadNextLevel();
        }

        // ������ʾ��ʱ��
        UpdateTimerText();
    }

    void UpdateTimerText()
    {
        // ���� TMP �ı�������
        timerText.text = $"{Mathf.Ceil(timer)}s Left"; // ʹ�� Mathf.Ceil ��������ʾ
    }

    void LoadNextLevel()
    {
        // �л�����һ���ؿ�
        SceneManager.LoadScene("Fail");
    }
}