using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class PlayerFollowMultipleAI : MonoBehaviour
{
    public List<GameObject> aiCharacters; // AI ��ɫ���б�
    public float stoppingDistance = 2.0f; // ֹͣ����ľ���

    void Update()
    {
        // �������� AI ��ɫ
        foreach (GameObject aiCharacter in aiCharacters)
        {
            NavMeshAgent aiNavMeshAgent = aiCharacter.GetComponent<NavMeshAgent>();
            if (aiNavMeshAgent != null)
            {
                float distance = Vector3.Distance(aiCharacter.transform.position, transform.position);

                // ����������ֹͣ���룬�ƶ� AI
                if (distance > stoppingDistance)
                {
                    aiNavMeshAgent.SetDestination(transform.position);
                }
                else
                {
                    // ֹͣ�ƶ�
                    aiNavMeshAgent.ResetPath();
                }
            }
        }
    }
}