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

    // 게임 시작 시간
    public DateTime seedlastTime;

    public TimeSpan timeDifference;

    [Header("# ItemData")]
    //스크랩터블 오브젝트 -- growthtime
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
        Debug.Log(PlantingAfterString + " 씨앗 심고 종료 후 다시 시작");
        PlayerPrefs.SetString("PlantingAfterRestart", PlantingAfterString);


        for (int i = 0; i < 3; i++)
        {
            string PlantedTimeString = PlayerPrefs.GetString("PlantingAfterTime"+i);
            Debug.Log(PlantedTimeString + " 씨앗별 저장된 시간");
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
                Debug.Log("씨앗 심은 후 : " + timeDifference);

                // 시간 차이를 x에 저장
                x = timeDifference;

                // x의 값을 확인하여 이벤트를 일으킴
                CheckTimeDifference(x);
            }

        }





    }


    void CheckTimeDifference(TimeSpan timeDiff)
    {
        // 예: 10 ~ 29
        if (timeDiff.TotalSeconds >= 10 /*&& timeDiff.TotalSeconds < 30*/)
        {
            Debug.Log("10초 이상 30초 미만 경과 -> 성장");

            for (int i = 0; i < 3; i++)
            {

                GameObject seedObject = GameObject.Find("seed" + i);
                GameObject Pot = GameObject.Find("Pot" + i);
                GameObject Sprout = Pot.transform.Find("Sprout" + i).gameObject;
                

                // seed+i 찾기.
                if (seedObject)
                {
                    Image seedImage = seedObject.GetComponent<Image>();
                    Debug.Log("seedImage.sprite 타입: " + seedImage.sprite.GetType());
                    Debug.Log("SeedSpr 타입: " + SeedSpr.GetType());
                    if (seedImage.sprite == SeedSpr)
                    {
                        Debug.Log(seedImage.sprite + " 찾음@@@@@@!@");
                        seedObject.AddComponent<CanvasGroup>();
                        
                        CanvasGroup SeedAlpha = seedObject.GetComponent<CanvasGroup>();
                        SeedAlpha.alpha = 0;
                        Debug.Log(SeedAlpha + " 00000");
                        Sprout.SetActive(true);
                    }
                    else
                    {
                        Debug.Log(" 새싹 찾음");

                    }
                }
            }
        }
    }





   /* // 한번만 실행
    // == 이미지가 씨앗일때만 .실행. 
    public void InsertTimeData()
    {

        // 이미지가 seed 인지 체크. 
        // 맞다면 growthtime 부여 
        for (int i = 0; i < 3; i++)
        {
            GameObject seedObject = GameObject.Find("seed" + i);

            if (seedObject)
            {
                Image seedImage = seedObject.GetComponent<Image>();

                if (seedImage.sprite == SeedSpr)
                {

                    // 로드가 있다면 실행하지않음
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
                        Debug.Log("값 저장 저장");
                    }
                    else
                    {
                        Debug.Log("이미 저장된 값이 있습니다: " + savedTime);
                    }
                }
            }
        }
        
   
    }







}