using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PRchk : MonoBehaviour
{
    public Seed seedScript; // Seed ��ũ��Ʈ�� ������ ���� public ����

    void Start()
    {
        // ���� GameObject�� �پ� �ִ� Seed ��ũ��Ʈ�� ������ ������
        seedScript = GetComponent<Seed>();
    }
}
