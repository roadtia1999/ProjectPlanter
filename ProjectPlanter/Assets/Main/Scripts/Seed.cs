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
    //씨앗 저장된시간
    private string PlantingAfter;
    private string PlantingAfterString;

    
    // 게임 시작 시간
    private DateTime seedlastTime;
    //시간 차이 값.
    private TimeSpan timeDifference;


    [Header("# ItemData")]
    //스크랩터블 오브젝트 -- growthtime
    public ItemData itemData;


    //[Header("# Arrangement")]
    private TimeSpan[] GrowTime = new TimeSpan[3];
    int[] value = new int[3];
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
            Plant[i] = Pot[i].transform.Find("FreesiaDemo" + i).gameObject;
            string PlantedTimeString = PlayerPrefs.GetString("PlantingAfterTime"+i);
            
            if (PlantedTimeString == null)
            {
                continue;
            }

            if (!string.IsNullOrEmpty(PlantedTimeString))
            {
                // 이전에 저장된 시간이 있다면 불러와서 DateTime으로 변환
                seedlastTime = DateTime.Parse(PlantedTimeString);
                
                // 현재 시작 시간도 DateTime으로 변환
                DateTime startTime = DateTime.Parse(PlantingAfterString);

                // 시간 차이 계산 후 클래스 레벨 변수에 저장
                timeDifference = startTime - seedlastTime;

                // 시간 차이 출력

                stack[i] = PlayerPrefs.GetInt("Stack" + i, 0); // 저장된 값이 없으면 0을 기본값으로 사용
                value[i] = PlayerPrefs.GetInt("PlantType"+i);

                TimeDifChk(i);

            }
        }
        
    }

    //Awake -> TimeDifChk -> TimeDifGrow -> CheckMethod -> CheckMethodXX
    // x 배열로 묶기  묶어서 뽑아오기 . x[0] x[1]
    void TimeDifChk(int i)
    {
        if (stack[i] !=1)
        {
            //물을 안주거나 너무 많이 줬다면 -10초로 성장 방지.
            timeDifference = timeDifference - TimeSpan.FromSeconds(10);
        }
        GrowTime[i] += timeDifference;
        // x의 값을 확인하여 이벤트를 일으킴
        
        TimeDifGrow(GrowTime[i], i);
    }


    //시간 조건에 따라 성장 메서드 실행
    void TimeDifGrow(TimeSpan timeDiff, int index)
    {
        // 예: 10 ~ 29
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

    void Empt(int index)
    {
        if (value[index] == plantType[index])
        {
            if (Plant[index])
            {
                Sprout[index].SetActive(false);
                Plant[index].SetActive(true);
                Plant[index].GetComponent<Image>().sprite = GetY_PlantSprite(value[index]);
            }
            
        }
    }

    void Empt2(int index)
    {
        if (value[index] == plantType[index])
        {
            if (Plant[index])
            {
                Sprout[index].SetActive(false);
                Plant[index].SetActive(true);
                Plant[index].GetComponent<Image>().sprite = GetF_PlantSprite(value[index]);
            }

        }
    }

    Sprite GetY_PlantSprite(int index)
    {
        switch (index)
        {
            case 0:
                return itemData.Y_Freesia;
            case 1:
                return itemData.F_Freesia;
            /*case 2: 
                return y _xxx;*/

            default:
                return null;
        }
    }

    Sprite GetF_PlantSprite(int index)
    {
        switch (index)
        {
            case 0:
                return itemData.Y_Freesia;
            case 1:
                return itemData.F_Freesia;
            /*case 2: 
                return y _xxx;*/

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
                    Empt(index);
                }
                else if (duration == 60)
                {
                    Empt2(index);
                }
            }
        }
    }

   





    /*Sprte x()
    {
        return itmeData.X;
    }*/

    // 씨앗 찾기.
    // 씨앗 심어진 후 종료.
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

                    PlayerPrefs.SetInt("PlantType" + i, plantType[i]); // 꽃 종류 저장



                    // PlayerPrefs에서 값을 읽어옴
                    string savedTime = PlayerPrefs.GetString("PlantingAfterTime" + i);
                    


                    // 값이 비어 있는지 확인
                    if (string.IsNullOrEmpty(savedTime))
                    {
                        // PlantingAfterTime0 1 2 에 다른 값들 부여.
                        PlayerPrefs.SetString("PlantingAfterTime" + i, PlantingAfter);
                        
                    }
                   
                }

            }

        }
        
    }







}