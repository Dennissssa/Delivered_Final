using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement; // 引入命名空间以使用场景管理
using System.Collections.Generic; // 引入命名空间以使用List

public class CarController : MonoBehaviour
{
    public float acceleration = 10f; // 加速度
    public float maxSpeed = 100f;     // 最大速度
    public float turnSpeed = 100f;    // 转向速度
    public float brakeForce = 50f;    // 刹车力度
    public float deceleration = 5f;    // 速度衰减系数
    public Slider speedSlider;         // 速度滑块
    public List<NavMeshAgent> aiAgents; // 存储多个AI Agent
    public Transform finishLine;       // 终点区域
    public string goldSceneName;       // 金场景名称
    public string silverSceneName;     // 银场景名称
    public string bronzeSceneName;     // 铜场景名称
    public float countdownTime = 60f;  // 倒计时秒数
    public AudioClip speedThresholdSound; // 速度阈值音效
    private AudioSource audioSource;     // 音频源
    public string failLevelSceneName;

    private float currentSpeed = 0f;   // 当前速度
    private float timeRemaining;        // 剩余时间

    void Start()
    {
        // 获取AudioSource组件
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = speedThresholdSound; // 设置音效
        audioSource.loop = true; // 设为循环播放
        timeRemaining = countdownTime; // 初始化倒计时
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
        // 加速逻辑
        if (Input.GetKey(KeyCode.W))
        {
            currentSpeed += acceleration * Time.deltaTime; // 使用加速度增加当前速度
        }

        // 刹车逻辑
        if (Input.GetKey(KeyCode.S))
        {
            currentSpeed -= brakeForce * Time.deltaTime; // 使用刹车力度减少当前速度
        }

        // 转向逻辑
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
        }

        // 限制最大速度
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);

        // 应用速度衰减
        if (!Input.GetKey(KeyCode.UpArrow) && currentSpeed > 0)
        {
            currentSpeed -= deceleration * Time.deltaTime; // 应用衰减
            currentSpeed = Mathf.Max(currentSpeed, 0); // 确保速度不为负
        }

        // 移动汽车
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);

        // 播放音效
        if (currentSpeed > 15f && !audioSource.isPlaying)
        {
            audioSource.Play(); // 播放音效
        }
    }

    private void UpdateSpeedSlider()
    {
        speedSlider.value = currentSpeed / maxSpeed; // 更新速度滑块
    }

    private void UpdateCountdown()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime; // 减少剩余时间
        }
    }

    private void CheckGameStatus()
    {
        // 检查玩家是否到达终点区域
        float distanceToFinish = Vector3.Distance(transform.position, finishLine.position);
        if (distanceToFinish < 5f) // 如果距离终点小于5
        {
            LoadNextScene(distanceToFinish);
        }
    }

    private void LoadNextScene(float distanceToFinish)
    {
        if (timeRemaining > 40f) // 60秒规则，40秒以上
        {
            SceneManager.LoadScene(goldSceneName); // 切换到金场景
        }
        else if (timeRemaining > 20f) // 20到40秒
        {
            SceneManager.LoadScene(silverSceneName); // 切换到银场景
        }
        else // 20秒以下
        {
            SceneManager.LoadScene(bronzeSceneName); // 切换到铜场景
        }
    }

    private void CheckAIBehavior()
    {
        if (currentSpeed > 15f) // 速度阈值
        {
            foreach (var aiAgent in aiAgents)
            {
                aiAgent.SetDestination(transform.position); // 用汽车的位置作为目标
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("NPC")) // 确保AI有"NPC"标签
        {
            // 切换到失败关卡
            SceneManager.LoadScene(failLevelSceneName);
        }
    }
}