using UnityEngine;

public class FloatUpDown : MonoBehaviour
{
    public float floatHeight = 0.5f; // �����ĸ߶�
    public float floatSpeed = 2f; // �������ٶ�
    private Vector3 startPosition;

    void Start()
    {
        // ��¼����ĳ�ʼλ��
        startPosition = transform.position;
    }

    void Update()
    {
        // ���㸡����Y��λ��
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        // ���������λ��
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}