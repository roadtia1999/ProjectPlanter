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

    [Header("# FloweSprite")]

    public Sprite Y_Freesia;
    public Sprite Freesia;


    private void Awake()
    {
        instance = this;

        /*InsertTimeData();*/
        PlantingAfterString = DateTime.Now.ToString();
        Debug.Log(PlantingAfterString + " ���� �ɰ� ���� �� �ٽ� ����");
        PlayerPrefs.SetString("PlantingAfterRestart", PlantingAfterString);


        for (int i = 0; i < 3; i++)
        {
            string PlantedTimeString = PlayerPrefs.GetString("PlantingAfterTime"+i);
            Debug.Log(PlantedTimeString + " ���Ѻ� ����� �ð�");
            if (PlantedTimeString == null)
            {
                continue;
            }

            if (!string.IsNullOrEmpty(PlantedTimeString))
            {
                // ������ ����� �ð��� �ִٸ� �ҷ��ͼ� DateTime���� ��ȯ
                seedlastTime = DateTime.Parse(PlantedTimeString);
                
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
        // ��: 10 ~ 29
        if (timeDiff.TotalSeconds >= 10 /*&& timeDiff.TotalSeconds < 30*/)
        {
            Debug.Log("10�� �̻� 30�� �̸� ��� -> ����");

            for (int i = 0; i < 3; i++)
            {

                GameObject seedObject = GameObject.Find("seed" + i);
                GameObject Pot = GameObject.Find("Pot" + i);
                GameObject Sprout = Pot.transform.Find("Sprout" + i).gameObject;
                

                // seed+i ã��.
                if (seedObject)
                {
                    Image seedImage = seedObject.GetComponent<Image>();
                    Debug.Log("seedImage.sprite Ÿ��: " + seedImage.sprite.GetType());
                    Debug.Log("SeedSpr Ÿ��: " + SeedSpr.GetType());
                    if (seedImage.sprite == SeedSpr)
                    {
                        Debug.Log(seedImage.sprite + " ã��@@@@@@!@");
                        seedObject.AddComponent<CanvasGroup>();
                        
                        CanvasGroup SeedAlpha = seedObject.GetComponent<CanvasGroup>();
                        SeedAlpha.alpha = 0;
                        Debug.Log(SeedAlpha + " 00000");
                        Sprout.SetActive(true);
                    }
                    else
                    {
                        Debug.Log(" ���� ã��");

                    }
                }
            }
        }
    }





   /* // �ѹ��� ����
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

    */
 
    // ���� ã��.
    // ���� �ɾ��� �� ����.
    void OnApplicationQuit()
    {
        // ���� ���� �� ���� �ð� ����
        
            PlantingAfter = DateTime.Now.ToString();
        for (int i = 0; i < 3; i++)
        {
            GameObject seedObject = GameObject.Find("seed" + i);

            // seed+i ã��.
            if (seedObject)
            {
                Image seedImage = seedObject.GetComponent<Image>();

                //�̹��� seed ���� Ȯ��
                if (seedImage.sprite == SeedSpr)
                {
                    // PlayerPrefs���� ���� �о��
                    string savedTime = PlayerPrefs.GetString("PlantingAfterTime" + i);

                    // ���� ��� �ִ��� Ȯ��
                    if (string.IsNullOrEmpty(savedTime))
                    {
                        // PlantingAfterTime0 1 2 �� �ٸ� ���� �ο�.
                        PlayerPrefs.SetString("PlantingAfterTime" + i, PlantingAfter);
                        Debug.Log("�� ���� ����");
                    }
                    else
                    {
                        Debug.Log("�̹� ����� ���� �ֽ��ϴ�: " + savedTime);
                    }
                }
            }
        }
        
   
    }







}