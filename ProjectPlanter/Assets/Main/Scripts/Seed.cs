using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Seed : MonoBehaviour
{
    public static Seed instance;

    
    //seed 스프라이트
    public Sprite SeedSpr;

    [Header("# SaveTime")]
    public string PlantingAfter;
    public string PlantingAfterString;

    [Header("# DateTime")]
    // 게임 시작 시간
    private DateTime seedlastTime;
    //시간 차이 값.
    private TimeSpan timeDifference;
    public TimeSpan[] GrowTime = new TimeSpan[3];

    [Header("# ItemData")]
    //Itemdata에 FlowerSP 가져오는 용도
    public ItemData itemData;

    [Header("# Arrangement")]
    public int[] seconds = new int[3]; //각 화분에 시간값 체크
    public int[] PlantType = new int[3]; //화분 어디어디로 들어갔는지 확인 가능.
    public int[] stack = new int[3]; //물 얼마나 뿌려졌는지 확인 가능.
    public Image[] PlantImage = new Image[3]; //PlantImage 타입별로 성장값 조정
    int eventOccur;
    int[] plantType = new int[3];
    GameObject[] seedObject = new GameObject[3];
    GameObject[] Pot = new GameObject[3];
    GameObject[] Sprout = new GameObject[3];
    GameObject[] Plant = new GameObject[3];
    
    private void Awake()
    {        
        instance = this;

        PlantingAfterString = DateTime.Now.ToString();
        PlayerPrefs.SetString("PlantingAfterRestart", PlantingAfterString);
        eventOccur = PlayerPrefs.GetInt("EventOccur"); 


        for (int i = 0; i < 3; i++)
        {
            seedObject[i] = GameObject.Find("seed" + i);
            Pot[i] = GameObject.Find("Pot" + i);
            Sprout[i] = Pot[i].transform.Find("Sprout" + i).gameObject;
            Plant[i] = Pot[i].transform.Find("FlowerDemo" + i).gameObject;
            
            string PlantedTimeString = PlayerPrefs.GetString("PlantingAfterTime"+i);
            
            if (!string.IsNullOrEmpty(PlantedTimeString))
            {
                // 이전에 저장된 시간이 있다면 불러와서 DateTime으로 변환
                seedlastTime = DateTime.Parse(PlantedTimeString);
                
                // 현재 시작 시간도 DateTime으로 변환
                DateTime startTime = DateTime.Parse(PlantingAfterString);

                // 시간 차이 계산 후 클래스 레벨 변수에 저장
                timeDifference = startTime - seedlastTime;

                // 시간 차이 출력                
                stack[i] = PlayerPrefs.GetInt("Stack" + i, 0);
                PlantType[i] = PlayerPrefs.GetInt("PlantType"+i);

                TimeDifChk(i);

            }
        }
        
    }


    //Awake -> TimeDifChk -> TimeDifGrow -> CheckMethod -> CheckMethodXX
    // x 배열로 묶기  묶어서 뽑아오기 . x[0] x[1]
    void TimeDifChk(int i)
    {
        if (eventOccur ==0)
        {
            timeDifference += TimeSpan.FromSeconds(10);   
        }
        else if (eventOccur == 1)
        {
            timeDifference -= TimeSpan.FromSeconds(10);
        }

        if (stack[i] !=1)
            //물을 안주거나 너무 많이 줬다면 -10초로 성장 방지.
            timeDifference = timeDifference - TimeSpan.FromSeconds(10);


        GrowTime[i] += timeDifference;
        
        // x의 값을 확인하여 이벤트를 일으킴
        TimeDifGrow(GrowTime[i], i);
        
        seconds[i] = (int)Math.Round(GrowTime[i].TotalSeconds);
        
    }


    //시간 조건에 따라 성장 메서드 실행
    //10~29 새싹     60> Flower
    //30~60 Y_Flower
    void TimeDifGrow(TimeSpan timeDiff, int index)
    {
        // 예: 10 ~ 29
        //2일. 1~2일 새싹 및 Y_Flower 3일 꽃
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


    // 꽃 자라는 순서
    // 프리지아 < 장미 < 수국
    // 0 /+10 /+20
    void RandomY_Flower(int index)
    {
        if (Plant[index])
            {
                Sprout[index].SetActive(false);
               
                Plant[index].SetActive(true);

                Sprite yPlantSprite = GetY_PlantSprite(PlantType[index]);


                if (yPlantSprite != null)
                    Plant[index].GetComponent<Image>().sprite = yPlantSprite;
                

            }
        FlowerChk(index);
            
        
    }

    void Random_Flower(int index)
    {
     
        if (Plant[index])
        {
            Sprout[index].SetActive(false);

            Plant[index].SetActive(true);

            Sprite fPlantSprite = GetF_PlantSprite(PlantType[index]);
            if (fPlantSprite != null)
            {
                Plant[index].GetComponent<Image>().sprite = fPlantSprite;
                PlayerPrefs.SetInt("PlantDexScene" + index, 1); // 식물 성장 정보를 PlayerPrefs에 저장
            }
            

        }

        
    }

    void FlowerChk(int index)
    {
        
        if (PlantImage[index].sprite == itemData.FlowerSp[1])
        {
            GrowTime[index] -= TimeSpan.FromSeconds(10);
            
        }
        else if (PlantImage[index].sprite == itemData.FlowerSp[2])
        {
            GrowTime[index] -= TimeSpan.FromSeconds(20);
        }
    }

    // 0.프리지아 1. 장미  2. 수국 
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


    // 씨앗 찾기.
    // 씨앗 심어진 후 종료.
    // 문제점? 특이점
    // 씨앗을 따로 따로 심으면 리스트가 새로 또 만들어지기때문에 
    // 중복된 씨앗 나올 수 도 있음
    // 개인저그올 나쁘지않다고 생각.
    void OnApplicationQuit()
    {
        // 게임 종료 시 현재 시간 저장
        PlantingAfter = DateTime.Now.ToString();
        List<int> availableTypes = new List<int> { 0, 1, 2 }; // 사용 가능한 꽃 종류 리스트
        System.Random rand = new System.Random(); // 랜덤 생성기
        for (int i = 0; i < 3; i++)
        {
            // seed+i 찾기.
            if (seedObject[i])
            {
                Image seedImage = seedObject[i].GetComponent<Image>();

                //이미지 seed 인지 확인
                if (seedImage.sprite == SeedSpr)
                {
                    // 남아 있는 꽃 종류에서 랜덤하게 선택
                    int randomIndex = rand.Next(0, availableTypes.Count);
                    plantType[i] = availableTypes[randomIndex];
                    availableTypes.RemoveAt(randomIndex); // 선택된 꽃 종류 제거

                    // PlayerPrefs에서 값을 읽어옴
                    string savedTime = PlayerPrefs.GetString("PlantingAfterTime" + i);

                    // 값이 비어 있는지 확인
                    if (string.IsNullOrEmpty(savedTime))
                    {
                        // PlantingAfterTime0 1 2 에 다른 값들 부여.
                        PlayerPrefs.SetString("PlantingAfterTime" + i, PlantingAfter);


                        PlayerPrefs.SetInt("PlantType" + i, plantType[i]); // 꽃 종류 저장
                        Debug.Log("PlantType" + i + " 저장된 값: " + PlayerPrefs.GetInt("PlantType" + i)); // 저장된 값을 출력


                    }
                   
                }

            }

        }
        
    }







}