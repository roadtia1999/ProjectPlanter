using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
  
    void Update()
    {

        // PlayerPrefs���� ��������
        int isSeedPlanted = PlayerPrefs.GetInt("IsSeedPlanted", 0); // �⺻���� 0

        if (isSeedPlanted == 1)
        {
            Debug.Log("���� �ɾ���.");
        }
        else
        {
            Debug.Log("���� ����.");
        }
    }
}