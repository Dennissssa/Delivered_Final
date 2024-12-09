using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement; // ���������ռ���ʹ�ó�������
using System.Collections.Generic; // ���������ռ���ʹ��List

public class CarController : MonoBehaviour
{
    public float acceleration = 10f; // ���ٶ�
    public float maxSpeed = 100f;     // ����ٶ�
    public float turnSpeed = 100f;    // ת���ٶ�
    public float brakeForce = 50f;    // ɲ������
    public float deceleration = 5f;    // �ٶ�˥��ϵ��
    public Slider speedSlider;         // �ٶȻ���
    public List<NavMeshAgent> aiAgents; // �洢���AI Agent
    public Transform finishLine;       // �յ�����
    public string goldSceneName;       // �𳡾�����
    public string silverSceneName;     // ����������
    public string bronzeSceneName;     // ͭ��������
    public float countdownTime = 60f;  // ����ʱ����
    public AudioClip speedThresholdSound; // �ٶ���ֵ��Ч
    private AudioSource audioSource;     // ��ƵԴ
    public string failLevelSceneName;

    private float currentSpeed = 0f;   // ��ǰ�ٶ�
    private float timeRemaining;        // ʣ��ʱ��

    void Start()
    {
        // ��ȡAudioSource���
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = speedThresholdSound; // ������Ч
        audioSource.loop = true; // ��Ϊѭ������
        timeRemaining = countdownTime; // ��ʼ������ʱ
    }

    void Update()
    {
        HandleInput();
        UpdateSpeedSlider();
        CheckAIBehavior();
        CheckGameStatus();
        UpdateCountdown();
    }

    private void HandleInput()
    {
        // �����߼�
        if (Input.GetKey(KeyCode.W))
        {
            currentSpeed += acceleration * Time.deltaTime; // ʹ�ü��ٶ����ӵ�ǰ�ٶ�
        }

        // ɲ���߼�
        if (Input.GetKey(KeyCode.S))
        {
            currentSpeed -= brakeForce * Time.deltaTime; // ʹ��ɲ�����ȼ��ٵ�ǰ�ٶ�
        }

        // ת���߼�
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
        }

        // ��������ٶ�
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);

        // Ӧ���ٶ�˥��
        if (!Input.GetKey(KeyCode.UpArrow) && currentSpeed > 0)
        {
            currentSpeed -= deceleration * Time.deltaTime; // Ӧ��˥��
            currentSpeed = Mathf.Max(currentSpeed, 0); // ȷ���ٶȲ�Ϊ��
        }

        // �ƶ�����
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);

        // ������Ч
        if (currentSpeed > 15f && !audioSource.isPlaying)
        {
            audioSource.Play(); // ������Ч
        }
    }

    private void UpdateSpeedSlider()
    {
        speedSlider.value = currentSpeed / maxSpeed; // �����ٶȻ���
    }

    private void UpdateCountdown()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime; // ����ʣ��ʱ��
        }
    }

    private void CheckGameStatus()
    {
        // �������Ƿ񵽴��յ�����
        float distanceToFinish = Vector3.Distance(transform.position, finishLine.position);
        if (distanceToFinish < 5f) // ��������յ�С��5
        {
            LoadNextScene(distanceToFinish);
        }
    }

    private void LoadNextScene(float distanceToFinish)
    {
        if (timeRemaining > 40f) // 60�����40������
        {
            SceneManager.LoadScene(goldSceneName); // �л����𳡾�
        }
        else if (timeRemaining > 20f) // 20��40��
        {
            SceneManager.LoadScene(silverSceneName); // �л���������
        }
        else // 20������
        {
            SceneManager.LoadScene(bronzeSceneName); // �л���ͭ����
        }
    }

    private void CheckAIBehavior()
    {
        if (currentSpeed > 15f) // �ٶ���ֵ
        {
            foreach (var aiAgent in aiAgents)
            {
                aiAgent.SetDestination(transform.position); // ��������λ����ΪĿ��
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("NPC")) // ȷ��AI��"NPC"��ǩ
        {
            // �л���ʧ�ܹؿ�
            SceneManager.LoadScene(failLevelSceneName);
        }
    }
}