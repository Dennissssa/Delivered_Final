using UnityEngine;

public class FloatUpDown : MonoBehaviour
{
    public float floatHeight = 0.5f; // 浮动的高度
    public float floatSpeed = 2f; // 浮动的速度
    private Vector3 startPosition;

    void Start()
    {
        // 记录物体的初始位置
        startPosition = transform.position;
    }

    void Update()
    {
        // 计算浮动的Y轴位置
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        // 更新物体的位置
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}