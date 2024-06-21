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
    public TimeSpan y;
    public TimeSpan z;

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

        /*InsertTimeData();*/
        PlantingAfterString = DateTime.Now.ToString();
        PlayerPrefs.SetString("PlantingAfterRestart", PlantingAfterString);


        for (int i = 0; i < 3; i++)
        {
            string PlantedTimeString = PlayerPrefs.GetString("PlantingAfterTime"+i);
            Debug.Log(PlantedTimeString + " ���Ѻ� ����� �ð�" + i);
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

                TimeDifChk(i);

            }
        }
    }


    void TimeDifGrow(TimeSpan timeDiff)
    {
        // ��: 10 ~ 29
        if (timeDiff.TotalSeconds >= 10 && timeDiff.TotalSeconds < 30)
        {
            if (x.TotalSeconds >= 10 && x.TotalSeconds < 30)
            {
                Debug.Log(x + "x == ���� ��");
            }

            if (y.TotalSeconds >= 10 && y.TotalSeconds < 30)
            {
                Debug.Log(y + "y == ���� ��");
            }

            if (z.TotalSeconds >= 10 && z.TotalSeconds < 30)
            {
                Debug.Log(z + "z == ���� ��");
            }

            for (int i = 0; i < 3; i++)
            {
                CheckMethod10sec(i);
            }
        }

        else if (timeDiff.TotalSeconds >= 30 && timeDiff.TotalSeconds < 60)
        {
            if (x.TotalSeconds >= 30 && x.TotalSeconds < 60)
            {
                Debug.Log(x + " �߰� x == ��");
            }

            if (y.TotalSeconds >= 30 && y.TotalSeconds < 60)
            {
                Debug.Log(y + " �߰� y == ��");
            }

            if (z.TotalSeconds >= 30 && z.TotalSeconds < 60)
            {
                Debug.Log(z + " �߰� z == ��");
            }



            for (int i = 0; i < 3; i++)
            {
                CheckMethod30sec(i);
            }

        }

        else if (timeDiff.TotalSeconds >= 60 )
        {
            if (x.TotalSeconds >= 60)
            {
                Debug.Log(x + "x == ��");
            }

            if (y.TotalSeconds >= 60)
            {
                Debug.Log(y + "y == ��");
            }

            if (z.TotalSeconds >= 60)
            {
                Debug.Log(z + "z == ��");
            }

            for (int i = 0; i < 3; i++)
            {
                CheckMethod60sec(i);
            }
        }

    }

    // x y z �迭�� ����  ��� �̾ƿ��� .
    void TimeDifChk(int i)
    {
        if (i == 0)
        {
            // �ð� ���̸� x�� ����
            x += timeDifference;
            // x�� ���� Ȯ���Ͽ� �̺�Ʈ�� ����Ŵ
            Debug.Log(x + "  x�ð����尪");
            TimeDifGrow(x);
        }
        else if (i == 1)
        {
            y += timeDifference;
            Debug.Log(y + "  y�ð����尪");
            TimeDifGrow(y);
        }
        else if (i == 2)
        {
            z += timeDifference;
            Debug.Log(z + "  ���ð����尪");
            TimeDifGrow(z);
        }

    }


    void CheckMethod10sec(int index)
    {
        Debug.Log("�޼��� ����10�� ");
        GameObject seedObject = GameObject.Find("seed" + index);
        GameObject Pot = GameObject.Find("Pot" + index);
        GameObject Sprout = Pot.transform.Find("Sprout" + index).gameObject;

        // seed+index ã��.
        if (seedObject)
        {
            Image seedImage = seedObject.GetComponent<Image>();
            if (seedImage.sprite == SeedSpr)
            {
                seedObject.AddComponent<CanvasGroup>();
                CanvasGroup SeedAlpha = seedObject.GetComponent<CanvasGroup>();
                SeedAlpha.alpha = 0;
                Sprout.SetActive(true);
            }

        }

    }

    //FreesiaDemo == �� �̹��� ���� ��ü 
    // 0 1 2 ���δ� �־��ִ°� ���� �ΰŰ���.
    void CheckMethod30sec(int index)
    {
        Debug.Log("�޼��� ����30�� ");
        GameObject seedObject = GameObject.Find("seed" + index);
        GameObject Pot = GameObject.Find("Pot" + index);
        GameObject Plant = Pot.transform.Find("FreesiaDemo" + index).gameObject;
        GameObject Sprout = Pot.transform.Find("Sprout" + index).gameObject;
        if (seedObject)
        {
            Image seedImage = seedObject.GetComponent<Image>();
            if (seedImage.sprite == SeedSpr)
            {
                if (Plant)
                {
                    Sprout.SetActive(false);
                    Plant.SetActive(true);
                    Plant.GetComponent<Image>().sprite = GetPlantSprite(0);
                }

            }
        }
    }


    void CheckMethod60sec(int index)
    {
        Debug.Log("�޼��� ����60�� ");
        GameObject seedObject = GameObject.Find("seed" + index);
        GameObject Pot = GameObject.Find("Pot" + index);
        GameObject Plant = Pot.transform.Find("FreesiaDemo" + index).gameObject;
        GameObject Sprout = Pot.transform.Find("Sprout" + index).gameObject;
        if (seedObject)
        {
            Image seedImage = seedObject.GetComponent<Image>();
            if (seedImage.sprite == SeedSpr)
            {
                if (Plant)
                {
                    Sprout.SetActive(false);
                    Plant.SetActive(true);
                    Plant.GetComponent<Image>().sprite = GetPlantSprite(1);
                }

            }
        }
    }




    Sprite GetPlantSprite(int index)
    {
        switch (index)
        {
            case 0:
                return itemData.Y_Freesia;
            case 1:
                return itemData.F_Freesia;
            
            default:
                return null;
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
                        Debug.Log("�� ���� ����  " +i + savedTime);
                    }
                   
                }
            }
        }
        
   
    }







}