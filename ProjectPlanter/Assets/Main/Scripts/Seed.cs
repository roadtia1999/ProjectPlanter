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

        /*InsertTimeData();*/
        PlantingAfterString = DateTime.Now.ToString();
        PlayerPrefs.SetString("PlantingAfterRestart", PlantingAfterString);


        for (int i = 0; i < 3; i++)
        {
            string PlantedTimeString = PlayerPrefs.GetString("PlantingAfterTime"+i);
            Debug.Log(PlantedTimeString + " 씨앗별 저장된 시간" + i);
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

                TimeDifChk(i);

            }
        }
    }


    void TimeDifGrow(TimeSpan timeDiff)
    {
        // 예: 10 ~ 29
        if (timeDiff.TotalSeconds >= 10 && timeDiff.TotalSeconds < 30)
        {
            if (x.TotalSeconds >= 10 && x.TotalSeconds < 30)
            {
                Debug.Log(x + "x == 새싹 참");
            }

            if (y.TotalSeconds >= 10 && y.TotalSeconds < 30)
            {
                Debug.Log(y + "y == 새싹 참");
            }

            if (z.TotalSeconds >= 10 && z.TotalSeconds < 30)
            {
                Debug.Log(z + "z == 새싹 참");
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
                Debug.Log(x + " 중간 x == 참");
            }

            if (y.TotalSeconds >= 30 && y.TotalSeconds < 60)
            {
                Debug.Log(y + " 중간 y == 참");
            }

            if (z.TotalSeconds >= 30 && z.TotalSeconds < 60)
            {
                Debug.Log(z + " 중간 z == 참");
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
                Debug.Log(x + "x == 참");
            }

            if (y.TotalSeconds >= 60)
            {
                Debug.Log(y + "y == 참");
            }

            if (z.TotalSeconds >= 60)
            {
                Debug.Log(z + "z == 참");
            }

            for (int i = 0; i < 3; i++)
            {
                CheckMethod60sec(i);
            }
        }

    }

    // x y z 배열로 묶기  묶어서 뽑아오기 .
    void TimeDifChk(int i)
    {
        if (i == 0)
        {
            // 시간 차이를 x에 저장
            x += timeDifference;
            // x의 값을 확인하여 이벤트를 일으킴
            Debug.Log(x + "  x시간저장값");
            TimeDifGrow(x);
        }
        else if (i == 1)
        {
            y += timeDifference;
            Debug.Log(y + "  y시간저장값");
            TimeDifGrow(y);
        }
        else if (i == 2)
        {
            z += timeDifference;
            Debug.Log(z + "  ㅋ시간저장값");
            TimeDifGrow(z);
        }

    }


    void CheckMethod10sec(int index)
    {
        Debug.Log("메서드 실행10초 ");
        GameObject seedObject = GameObject.Find("seed" + index);
        GameObject Pot = GameObject.Find("Pot" + index);
        GameObject Sprout = Pot.transform.Find("Sprout" + index).gameObject;

        // seed+index 찾기.
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

    //FreesiaDemo == 꽃 이미지 넣을 객체 
    // 0 1 2 전부다 넣어주는게 문제 인거같음.
    void CheckMethod30sec(int index)
    {
        Debug.Log("메서드 실행30초 ");
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
        Debug.Log("메서드 실행60초 ");
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
                        Debug.Log("값 저장 저장  " +i + savedTime);
                    }
                   
                }
            }
        }
        
   
    }







}