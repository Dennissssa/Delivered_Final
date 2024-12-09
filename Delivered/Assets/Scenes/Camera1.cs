using UnityEngine;

public class SimpleCameraFollow : MonoBehaviour
{
    public Transform target; // 要跟随的主角
    public Vector3 offset; // 相机与主角之间的偏移
    public float followSpeed = 5f; // 跟随速度

    private void LateUpdate()
    {
        if (target != null)
        {
            // 计算目标位置
            Vector3 targetPosition = target.position + offset;

            // 平滑地移动相机到目标位置
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

            // 始终看向主角
            transform.LookAt(target);
        }
    }
}