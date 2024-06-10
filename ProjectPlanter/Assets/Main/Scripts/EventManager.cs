using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    private void Awake()
    {
        instance = this;

    }

    void Start()
    {

        TimeManager timeManager = TimeManager.instance;

            // �ð� ���� ��������
            TimeSpan timeDif = timeManager.timeDifference;

            Debug.Log(timeDif + "   Ÿ�ӸŴ������� ������ ����");

            // �ð� ���̰� 1�ð� �̻��� �� ���� �̺�Ʈ �߻�
            if (timeDif.TotalSeconds >= 30 )
            {
                TriggerRandomEvent();
            }
        
      
    }

    public void TriggerRandomEvent()
    {
        // ���� ���� ����
        int randomEvent = UnityEngine.Random.Range(0, 3);

        // ���� �̺�Ʈ �߻�
        switch (randomEvent)
        {
            case 0: Debug.Log("���� �̺�Ʈ �߻�"); break;

            default: break;
        }
    }
}
