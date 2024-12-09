using UnityEngine;

public class FloatObject : MonoBehaviour
{
    public enum FloatDirection { Horizontal, Depth }
    public FloatDirection floatDirection; // ��������
    public float floatDistance = 0.5f; // �����ľ���
    public float floatSpeed = 2f; // �������ٶ�

    private Vector3 startPosition;

    void Start()
    {
        // ��¼����ĳ�ʼλ��
        startPosition = transform.position;
    }

    void Update()
    {
        float newPosition = 0f;

        // ���ݸ�����������µ�λ��
        switch (floatDirection)
        {
            case FloatDirection.Horizontal: // X�ḡ��
                newPosition = startPosition.x + Mathf.Sin(Time.time * floatSpeed) * floatDistance;
                transform.position = new Vector3(newPosition, startPosition.y, startPosition.z);
                break;

            case FloatDirection.Depth: // Z�ḡ��
                newPosition = startPosition.z + Mathf.Sin(Time.time * floatSpeed) * floatDistance;
                transform.position = new Vector3(startPosition.x, startPosition.y, newPosition);
                break;
        }
    }
}