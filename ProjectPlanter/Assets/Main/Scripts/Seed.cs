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


    [Header("# ItemData")]
    //��ũ���ͺ� ������Ʈ -- growthtime
    public ItemData itemData;


    public int[] seconds = new int[3];
 
   //[Header("# Arrangement")]
    
    public TimeSpan[] GrowTime = new TimeSpan[3];
    public int[] value = new int[3]; //ȭ�� ������ ������ Ȯ�� ����.
    int[] stack = new int[3];
    int[] plantType = new int[3];
    GameObject[] seedObject = new GameObject[3];
    GameObject[] Pot = new GameObject[3];
    GameObject[] Sprout = new GameObject[3];
    GameObject[] Plant = new GameObject[3];
    

    private void Awake()
    {
        
        instance = this;
        
        /*InsertTimeData();*/
        PlantingAfterString = DateTime.Now.ToString();
        PlayerPrefs.SetString("PlantingAfterRestart", PlantingAfterString);
        

        for (int i = 0; i < 3; i++)
        {
            seedObject[i] = GameObject.Find("seed" + i);
            Pot[i] = GameObject.Find("Pot" + i);
            Sprout[i] = Pot[i].transform.Find("Sprout" + i).gameObject;
            Plant[i] = Pot[i].transform.Find("FlowerDemo" + i).gameObject;
            
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
                /*Debug.Log(value[i] + "value" +i + "��@@@@@");*/
                
                TimeDifChk(i);

            }
        }
        
    }

    //Awake -> TimeDifChk -> TimeDifGrow -> CheckMethod -> CheckMethodXX
    // x �迭�� ����  ��� �̾ƿ��� . x[0] x[1]
    void TimeDifChk(int i)
    {
        FlowerChk(i);
        if (stack[i] !=1)
        {
            //���� ���ְų� �ʹ� ���� ��ٸ� -10�ʷ� ���� ����.
            timeDifference = timeDifference - TimeSpan.FromSeconds(10);
        }
        GrowTime[i] += timeDifference;
        
        // x�� ���� Ȯ���Ͽ� �̺�Ʈ�� ����Ŵ
        TimeDifGrow(GrowTime[i], i);
        //���� Ȯ�� ��
        seconds[i] = (int)Math.Round(GrowTime[i].TotalSeconds);

        // �����: seconds �� ���
        Console.WriteLine("Seconds = {0}", seconds[i]);
        /*Debug.Log(GrowTime[i] + " �׷ο�Ÿ��"+i);*/
        
    }


    //�ð� ���ǿ� ���� ���� �޼��� ����
    //10~29 ����     60> Flower
    //30~60 Y_Flower
    void TimeDifGrow(TimeSpan timeDiff, int index)
    {
        // ��: 10 ~ 29
        //2��. 1~2�� ���� �� Y_Flower 3�� ��.

        
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


    // �� �ڶ�� ����
    // �������� < ��� < ����
    // 0 /+10 /+20
    void RandomY_Flower(int index)
    {

            if (Plant[index])
            {
                Sprout[index].SetActive(false);
               
                Plant[index].SetActive(true);

                Sprite yPlantSprite = GetY_PlantSprite(value[index]);


                if (yPlantSprite != null)
                    Plant[index].GetComponent<Image>().sprite = yPlantSprite;
                

            }
            
        
    }

    void Random_Flower(int index)
    {
     
        if (Plant[index])
        {
            Sprout[index].SetActive(false);

            Plant[index].SetActive(true);

            Sprite fPlantSprite = GetF_PlantSprite(value[index]);
            if (fPlantSprite != null)
                Plant[index].GetComponent<Image>().sprite = fPlantSprite;
            

        }

        
    }

    void FlowerChk(int index)
    {
        Image[] PlantImage = new Image[3];
        PlantImage[index] = Plant[index].GetComponent<Image>();

        if (PlantImage[index].sprite == itemData.FlowerSp[1])
        {
            GrowTime[1] -= TimeSpan.FromSeconds(10);
        }
        else if (PlantImage[index].sprite == itemData.FlowerSp[2])
        {
            GrowTime[2] -= TimeSpan.FromSeconds(20);
        }
        //2������ ���� �۵�..
        Debug.Log(GrowTime[index] + "  �Ĺ� üũ�� �� �׷ο� Ÿ��" +index);
    }

    // 0==�������� 1. ���  2. ���� 
    Sprite GetY_PlantSprite(int index)
    {
        switch (index)
        {
            case 0:
                return itemData.FlowerSp[0];
            case 1:
                return itemData.FlowerSp[1];
            case 2:
                return itemData.FlowerSp[2];

            default:
                return null;
        }
    }

    Sprite GetF_PlantSprite(int index)
    {
        switch (index)
        {
            case 0:
                return itemData.FlowerSp[3];
            case 1:
                return itemData.FlowerSp[4];
            case 2:
                return itemData.FlowerSp[5];

            default:
                return null;
        }
    }



    void CheckMethod(int index, int duration)
    {
                   
        if (seedObject[index])
        {
            Image seedImage = seedObject[index].GetComponent<Image>();
            if (seedImage.sprite == SeedSpr)
            {
                if (duration == 10)
                {
                    seedObject[index].AddComponent<CanvasGroup>();
                    CanvasGroup SeedAlpha = seedObject[index].GetComponent<CanvasGroup>();
                    SeedAlpha.alpha = 0;
                    Sprout[index].SetActive(true);
                }
                else if (duration == 30)
                {
                    RandomY_Flower(index);
                }
                else if (duration == 60)
                {
                    Random_Flower(index);
                }
            }
        }
    }


    // ���� ã��.
    // ���� �ɾ��� �� ����.
    // ������? Ư����
    // ������ ���� ���� ������ ����Ʈ�� ���� �� ��������⶧���� 
    // �ߺ��� ���� ���� �� �� ����
    // �������׿� �������ʴٰ� ����.
    void OnApplicationQuit()
    {
        // ���� ���� �� ���� �ð� ����
        PlantingAfter = DateTime.Now.ToString();
        List<int> availableTypes = new List<int> { 0, 1, 2 }; // ��� ������ �� ���� ����Ʈ
        System.Random rand = new System.Random(); // ���� ������
        for (int i = 0; i < 3; i++)
        {

            // seed+i ã��.
            if (seedObject[i])
            {
                Image seedImage = seedObject[i].GetComponent<Image>();

                //�̹��� seed ���� Ȯ��
                if (seedImage.sprite == SeedSpr)
                {
                    // ���� �ִ� �� �������� �����ϰ� ����
                    int randomIndex = rand.Next(0, availableTypes.Count);
                    plantType[i] = availableTypes[randomIndex];
                    availableTypes.RemoveAt(randomIndex); // ���õ� �� ���� ����

                  


                    // PlayerPrefs���� ���� �о��
                    string savedTime = PlayerPrefs.GetString("PlantingAfterTime" + i);
                    


                    // ���� ��� �ִ��� Ȯ��
                    if (string.IsNullOrEmpty(savedTime))
                    {
                        // PlantingAfterTime0 1 2 �� �ٸ� ���� �ο�.
                        PlayerPrefs.SetString("PlantingAfterTime" + i, PlantingAfter);



                        PlayerPrefs.SetInt("PlantType" + i, plantType[i]); // �� ���� ����
                        Debug.Log("PlantType" + i + " ����� ��: " + PlayerPrefs.GetInt("PlantType" + i)); // ����� ���� ���


                    }
                   
                }

            }

        }
        
    }







}