using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Seed : MonoBehaviour
{
    public static Seed instance;
    public Sprite SeedSpr;
    public string PlantingAfter;
    public string PlantingAfterString;

    public TimeSpan timeDif;
    public TimeSpan x;

    // ���� ���� �ð�
    public DateTime seedlastTime;

    public TimeSpan timeDifference;

    [Header("# ItemData")]
    //��ũ���ͺ� ������Ʈ -- growthtime
    public ItemData itemData;
    public float growthTime;

    private void Awake()
    {
        instance = this;

        InsertTimeData();
        PlantingAfterString = DateTime.Now.ToString();
        Debug.Log(PlantingAfterString + " ���� �ɰ� ���� �� �ٽ� ����");
        PlayerPrefs.SetString("PlantingAfterRestart", PlantingAfterString);


        for (int i = 0; i < 3; i++)
        {
            string savedTimeString = PlayerPrefs.GetString("PlantingAfterTime"+i);
            Debug.Log(savedTimeString + "   ����ð� �ҷ�����");

            if (!string.IsNullOrEmpty(savedTimeString))
            {
                // ������ ����� �ð��� �ִٸ� �ҷ��ͼ� DateTime���� ��ȯ
                seedlastTime = DateTime.Parse(savedTimeString);
                Debug.Log(seedlastTime);
                // ���� ���� �ð��� DateTime���� ��ȯ
                DateTime startTime = DateTime.Parse(PlantingAfterString);

                // �ð� ���� ��� �� Ŭ���� ���� ������ ����
                timeDifference = startTime - seedlastTime;

                // �ð� ���� ���
                Debug.Log("���� ���� �� : " + timeDifference);

                // �ð� ���̸� x�� ����
                x = timeDifference;

                // x�� ���� Ȯ���Ͽ� �̺�Ʈ�� ����Ŵ
                CheckTimeDifference(x);
            }

        }





    }

    void CheckTimeDifference(TimeSpan timeDiff)
    {
        // ��: �ð� ���̰� 1�ð� �̻��� �� �̺�Ʈ�� �߻���Ŵ
        if (timeDiff.TotalHours >= 1)
        {
            Debug.Log("1�ð� �̻� ���");
            // �̺�Ʈ �߻� �ڵ� �߰�
        }
    }





    // �ѹ��� ����
    // == �̹����� �����϶��� .����. 
    public void InsertTimeData()
    {

        // �̹����� seed ���� üũ. 
        // �´ٸ� growthtime �ο� 
        for (int i = 0; i < 3; i++)
        {
            GameObject seedObject = GameObject.Find("seed" + i);

            if (seedObject)
            {
                Image seedImage = seedObject.GetComponent<Image>();

                if (seedImage.sprite == SeedSpr)
                {

                    // �ε尡 �ִٸ� ������������
                    if (PlantingAfterString == null)
                    {

                        if (itemData != null)
                        {
                            growthTime = itemData.GrowthTime;
                        }

                    }

                }

            }

        }
      
    }
 
    // ���� ã��.
    // ���� �ɾ��� �� ����.
    void OnApplicationQuit()
    {
        // ���� ���� �� ���� �ð� ����
        
        PlantingAfter = DateTime.Now.ToString();
        for (int i = 0; i < 3; i++)
        {
            GameObject seedObject = GameObject.Find("seed" + i);

            if (seedObject)
            {
                Image seedImage = seedObject.GetComponent<Image>();

                if (seedImage.sprite == SeedSpr)
                {
                    PlayerPrefs.SetString("PlantingAfterTime" + i, PlantingAfter + i);
                    Debug.Log(PlantingAfter + i + " ���� ���� �� ����");
                }
            }
        }
        
   
    }







}