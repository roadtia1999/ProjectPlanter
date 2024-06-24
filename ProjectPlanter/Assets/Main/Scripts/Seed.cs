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
    private TimeSpan[] GrowTime = new TimeSpan[3];

    [Header("# ItemData")]
    //스크랩터블 오브젝트 -- growthtime
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
                // 이전에 저장된 시간이 있다면 불러와서 DateTime으로 변환
                seedlastTime = DateTime.Parse(PlantedTimeString);
                
                // 현재 시작 시간도 DateTime으로 변환
                DateTime startTime = DateTime.Parse(PlantingAfterString);

                // 시간 차이 계산 후 클래스 레벨 변수에 저장
                timeDifference = startTime - seedlastTime;

                // 시간 차이 출력
                

                TimeDifChk(i);

            }
        }
        Debug.Log("Seed 실행@@@@");
    }

    //Awake -> TimeDifChk -> TimeDifGrow -> CheckMethod -> CheckMethodXX
    // x y z 배열로 묶기  묶어서 뽑아오기 .
    void TimeDifChk(int i)
    {
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

    // 씨앗 찾기.
    // 씨앗 심어진 후 종료.
    void OnApplicationQuit()
    {
        // 게임 종료 시 현재 시간 저장
        PlantingAfter = DateTime.Now.ToString();
        for (int i = 0; i < 3; i++)
        {
            GameObject seedObject = GameObject.Find("seed" + i);

            // seed+i 찾기.
            if (seedObject)
            {
                Image seedImage = seedObject.GetComponent<Image>();

                //이미지 seed 인지 확인
                if (seedImage.sprite == SeedSpr)
                {
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