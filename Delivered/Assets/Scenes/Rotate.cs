using UnityEngine;
using UnityEngine.AI;

public class NavMeshMoveAndLook : MonoBehaviour
{
    public float rotationSpeed = 10f; // ��ת�ٶ�

    private NavMeshAgent navMeshAgent;

    void Start()
    {
        // ��ȡ NavMeshAgent ���
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // ��� NavMeshAgent �����ƶ�
        if (navMeshAgent.velocity.magnitude > 0.1f)
        {
            // ��ȡ��ǰ�н�����
            Vector3 direction = navMeshAgent.velocity.normalized;

            // ����Ŀ����ת
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            // ƽ����ת
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
}