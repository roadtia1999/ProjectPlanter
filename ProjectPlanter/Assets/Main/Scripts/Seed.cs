using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Seed : MonoBehaviour
{
    public static Seed instance;

    //seed ��������Ʈ
    public Sprite SeedSpr;
    //���� ����Ƚð�
    private string PlantingAfter;
    private string PlantingAfterString;

    
    // ���� ���� �ð�
    private DateTime seedlastTime;
    //�ð� ���� ��.
    private TimeSpan timeDifference;
    private TimeSpan[] GrowTime = new TimeSpan[3];

    [Header("# ItemData")]
    //��ũ���ͺ� ������Ʈ -- growthtime
    public ItemData itemData;
    



    private void Awake()
    {
        instance = this;

        /*InsertTimeData();*/
        PlantingAfterString = DateTime.Now.ToString();
        PlayerPrefs.SetString("PlantingAfterRestart", PlantingAfterString);


        for (int i = 0; i < 3; i++)
        {
            string PlantedTimeString = PlayerPrefs.GetString("PlantingAfterTime"+i);
            
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
                

                TimeDifChk(i);

            }
        }
        Debug.Log("Seed ����@@@@");
    }

    //Awake -> TimeDifChk -> TimeDifGrow -> CheckMethod -> CheckMethodXX
    // x y z �迭�� ����  ��� �̾ƿ��� .
    void TimeDifChk(int i)
    {
        GrowTime[i] += timeDifference;
        // x�� ���� Ȯ���Ͽ� �̺�Ʈ�� ����Ŵ
        
        TimeDifGrow(GrowTime[i], i);
    }


    //�ð� ���ǿ� ���� ���� �޼��� ����
    void TimeDifGrow(TimeSpan timeDiff, int index)
    {
        // ��: 10 ~ 29
        if (timeDiff.TotalSeconds >= 10 && timeDiff.TotalSeconds < 30)
            CheckMethod10sec(index);

        else if (timeDiff.TotalSeconds >= 30 && timeDiff.TotalSeconds < 60)
            CheckMethod30sec(index);

        else if (timeDiff.TotalSeconds >= 60)
            CheckMethod60sec(index);
        
    }

    void CheckMethod10sec(int index)
    {
        CheckMethod(index, 10);
    }

    void CheckMethod30sec(int index)
    {
        CheckMethod(index, 30);
    }

    void CheckMethod60sec(int index)
    {
        CheckMethod(index, 60);
    }



    void CheckMethod(int index, int duration)
    {
        
        GameObject seedObject = GameObject.Find("seed" + index);
        GameObject Pot = GameObject.Find("Pot" + index);
        GameObject Sprout = Pot.transform.Find("Sprout" + index).gameObject;
        GameObject Plant = Pot.transform.Find("FreesiaDemo" + index).gameObject;

        if (seedObject)
        {
            Image seedImage = seedObject.GetComponent<Image>();
            if (seedImage.sprite == SeedSpr)
            {
                if (duration == 10)
                {
                    seedObject.AddComponent<CanvasGroup>();
                    CanvasGroup SeedAlpha = seedObject.GetComponent<CanvasGroup>();
                    SeedAlpha.alpha = 0;
                    Sprout.SetActive(true);
                }
                else if (duration == 30)
                {
                    if (Plant)
                    {
                        Sprout.SetActive(false);
                        Plant.SetActive(true);
                        Plant.GetComponent<Image>().sprite = GetPlantSprite(0);
                    }
                }
                else if (duration == 60)
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
                        
                    }
                   
                }

            }

        }
        
    }







}