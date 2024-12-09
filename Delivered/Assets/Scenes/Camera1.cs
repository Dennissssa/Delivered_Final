using UnityEngine;

public class SimpleCameraFollow : MonoBehaviour
{
    public Transform target; // Ҫ���������
    public Vector3 offset; // ���������֮���ƫ��
    public float followSpeed = 5f; // �����ٶ�

    private void LateUpdate()
    {
        if (target != null)
        {
            // ����Ŀ��λ��
            Vector3 targetPosition = target.position + offset;

            // ƽ�����ƶ������Ŀ��λ��
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

            // ʼ�տ�������
            transform.LookAt(target);
        }
    }
}