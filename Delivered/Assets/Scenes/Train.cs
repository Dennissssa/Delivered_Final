using UnityEngine;

public class FloatObject : MonoBehaviour
{
    public enum FloatDirection { Horizontal, Depth }
    public FloatDirection floatDirection; // 浮动方向
    public float floatDistance = 0.5f; // 浮动的距离
    public float floatSpeed = 2f; // 浮动的速度

    private Vector3 startPosition;

    void Start()
    {
        // 记录物体的初始位置
        startPosition = transform.position;
    }

    void Update()
    {
        float newPosition = 0f;

        // 根据浮动方向计算新的位置
        switch (floatDirection)
        {
            case FloatDirection.Horizontal: // X轴浮动
                newPosition = startPosition.x + Mathf.Sin(Time.time * floatSpeed) * floatDistance;
                transform.position = new Vector3(newPosition, startPosition.y, startPosition.z);
                break;

            case FloatDirection.Depth: // Z轴浮动
                newPosition = startPosition.z + Mathf.Sin(Time.time * floatSpeed) * floatDistance;
                transform.position = new Vector3(startPosition.x, startPosition.y, newPosition);
                break;
        }
    }
}