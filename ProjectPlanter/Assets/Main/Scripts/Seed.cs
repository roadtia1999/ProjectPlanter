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

    private void Awake()
    {
        instance = this;

        InsertTimeData();
        PlantingAfterString = DateTime.Now.ToString();
        Debug.Log(PlantingAfterString + " 씨앗 심고 종료 후 다시 시작");
        PlayerPrefs.SetString("PlantingAfterRestart", PlantingAfterString);


        for (int i = 0; i < 3; i++)
        {
            string savedTimeString = PlayerPrefs.GetString("PlantingAfterTime"+i);
            Debug.Log(savedTimeString + "   저장시간 불러오기");

            if (!string.IsNullOrEmpty(savedTimeString))
            {
                // 이전에 저장된 시간이 있다면 불러와서 DateTime으로 변환
                seedlastTime = DateTime.Parse(savedTimeString);
                Debug.Log(seedlastTime);
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
        // 예: 시간 차이가 1시간 이상일 때 이벤트를 발생시킴
        if (timeDiff.TotalHours >= 1)
        {
            Debug.Log("1시간 이상 경과");
            // 이벤트 발생 코드 추가
        }
    }





    // 한번만 실행
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
 
    // 씨앗 찾기.
    // 씨앗 심어진 후 종료.
    void OnApplicationQuit()
    {
        // 게임 종료 시 현재 시간 저장
        
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
                    Debug.Log(PlantingAfter + i + " 씨앗 심은 후 종료");
                }
            }
        }
        
   
    }







}