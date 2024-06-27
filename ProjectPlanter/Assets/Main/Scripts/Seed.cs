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
    int[] value = new int[3];
    [Header("# ItemData")]
    //��ũ���ͺ� ������Ʈ -- growthtime
    public ItemData itemData;

    int[] stack = new int[3];
    int[] plantType = new int[3];

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

                stack[i] = PlayerPrefs.GetInt("Stack" + i, 0); // ����� ���� ������ 0�� �⺻������ ���
                value[i] = PlayerPrefs.GetInt("PlantType"+i);

                TimeDifChk(i);

            }
        }
        
    }

    //Awake -> TimeDifChk -> TimeDifGrow -> CheckMethod -> CheckMethodXX
    // x �迭�� ����  ��� �̾ƿ��� . x[0] x[1]
    void TimeDifChk(int i)
    {
        if (stack[i] !=1)
        {
            //���� ���ְų� �ʹ� ���� ��ٸ� -10�ʷ� ���� ����.
            timeDifference = timeDifference - TimeSpan.FromSeconds(10);
        }
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

    void Empt()
    {
        if (value[0] == plantType[0])
        {
            GetPlantSprite(0);
        }
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
                    Empt();
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
            /*case 2:
                return //x() ;*/

            default:
                return null;
        }
    }
    /*Sprte x()
    {
        return itmeData.X;
    }*/

    // ���� ã��.
    // ���� �ɾ��� �� ����.
    void OnApplicationQuit()
    {
        // ���� ���� �� ���� �ð� ����
        PlantingAfter = DateTime.Now.ToString();
        List<int> availableTypes = new List<int> { 0, 1, 2 }; // ��� ������ �� ���� ����Ʈ
        System.Random rand = new System.Random(); // ���� ������
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
                    // ���� �ִ� �� �������� �����ϰ� ����
                    int randomIndex = rand.Next(0, availableTypes.Count);
                    plantType[i] = availableTypes[randomIndex];
                    availableTypes.RemoveAt(randomIndex); // ���õ� �� ���� ����

                    PlayerPrefs.SetInt("PlantType" + i, plantType[i]); // �� ���� ����



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