using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Seed : MonoBehaviour
{
    public static Seed instance;
    public MainBtnManager BtnManager;

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
    public int[] GrowStack = new int[3];
    public Image[] PlantImage = new Image[3]; //PlantImage 타입별로 성장값 조정
    int[] plantType = new int[3];
    GameObject[] seedObject = new GameObject[3];
    GameObject[] Pot = new GameObject[3];
    GameObject[] Sprout = new GameObject[3];
    GameObject[] Plant = new GameObject[3];
    GameObject[] PlantState = new GameObject[3];
    GameObject[] bubleObject = new GameObject[3];

    [Header("# ETC")]
    int eventOccur;
    int Flowerindex;
    int btnBuble;
    [Header("# Harvest")]
    public Canvas canvas;
    public GameObject HarvestPrefab;
    private GameObject HarvestInstance;
    Image buttonImage;





    private void Awake()
    {
        instance = this;

        /*PlantingAfterString = DateTime.Now.ToString();
        PlayerPrefs.SetString("PlantingAfterRestart", PlantingAfterString);*/
        eventOccur = PlayerPrefs.GetInt("EventOccur");


        for (int i = 0; i < 3; i++)
        {
            seedObject[i] = GameObject.Find("seed" + i);
            Pot[i] = GameObject.Find("Pot" + i);
            Sprout[i] = Pot[i].transform.Find("Sprout" + i).gameObject;
            Plant[i] = Pot[i].transform.Find("FlowerDemo" + i).gameObject;
            PlantState[i] = Pot[i].transform.Find("PlantState" + i).gameObject;
            bubleObject[i] = Pot[i].transform.Find("Button Buble" + i).gameObject;

            string PlantedTimeString = PlayerPrefs.GetString("PlantingAfterTime" + i);

            if (!string.IsNullOrEmpty(PlantedTimeString))
            {
                // 이전에 저장된 시간이 있다면 불러와서 DateTime으로 변환
                seedlastTime = DateTime.Parse(PlantedTimeString);

                // 현재 시작 시간도 DateTime으로 변환
                DateTime startTime = DateTime./*Parse(PlantingAfterString);*/Now;

                // 시간 차이 계산 후 클래스 레벨 변수에 저장
                timeDifference = startTime - seedlastTime;

                // 시간 차이 출력                
                stack[i] = PlayerPrefs.GetInt("Stack" + i, 0);
                PlantType[i] = PlayerPrefs.GetInt("PlantType" + i);

                TimeDifChk(i);

                if (PlantType[i] == 0)
                    Plant0(i);

                else if (PlantType[i] == 1)
                    Plant1(i);

                else if (PlantType[i] == 2)
                    Plant2(i);


            }
        }

    }




    //Awake -> TimeDifChk -> TimeDifGrow -> CheckMethod -> CheckMethodXX
    // x 배열로 묶기  묶어서 뽑아오기 . x[0] x[1]
    void TimeDifChk(int i)
    {
        if (eventOccur == 0)
        {
            timeDifference += TimeSpan.FromSeconds(10);
        }
        else if (eventOccur == 1)
        {
            timeDifference -= TimeSpan.FromSeconds(10);
        }

        if (stack[i] != 1)
            //물을 안주거나 너무 많이 줬다면 -10초로 성장 방지.
            // 어짜피 수국은 성장 주기가 가장 느리니 조정해도 남기는게 나쁘지 않다고 생각.
            timeDifference = timeDifference - TimeSpan.FromSeconds(10);


        GrowTime[i] += timeDifference;


        // x의 값을 확인하여 이벤트를 일으킴
        /*TimeDifGrow(GrowTime[i], i);*/

        seconds[i] = (int)Math.Round(GrowTime[i].TotalSeconds);



    }
    //문제.정해진 시간 안에 못들어오면 grow스택값 부여 받지 못함.
    //-> 영원히 0혹은 전의 스택을 가지고있음.
    void Plant0(int index)
    {
        if (GrowTime[0].TotalSeconds >= 10 && GrowTime[0].TotalSeconds < 30 && stack[0] == 1)
            PlayerPrefs.SetInt("GrowStack" + index, 1);

        else if (GrowTime[0].TotalSeconds >= 30 && GrowTime[0].TotalSeconds < 60 && stack[0] == 1)
            PlayerPrefs.SetInt("GrowStack" + index, 2);

        else if (GrowTime[0].TotalSeconds >= 60 && stack[0] == 1)
            PlayerPrefs.SetInt("GrowStack" + index, 3);


        CheckMethodPlay(index);


    }

    void Plant1(int index)
    {
        if (GrowTime[1].TotalSeconds >= 10 && GrowTime[1].TotalSeconds < 30 && stack[1] == 1)
            PlayerPrefs.SetInt("GrowStack" + index, 1);

        else if (GrowTime[1].TotalSeconds >= 30 && GrowTime[1].TotalSeconds < 60 && stack[1] == 1)
            PlayerPrefs.SetInt("GrowStack" + index, 2);

        else if (GrowTime[1].TotalSeconds >= 60 && stack[1] == 1)
            PlayerPrefs.SetInt("GrowStack" + index, 3);

        CheckMethodPlay(index);
    }

    void Plant2(int index)
    {

        if (GrowTime[2].TotalSeconds >= 10 && GrowTime[2].TotalSeconds < 59 && stack[2] == 1)
            PlayerPrefs.SetInt("GrowStack" + index, 1);

        else if (GrowTime[2].TotalSeconds >= 60 && GrowTime[2].TotalSeconds < 80 && stack[2] > 2)
            PlayerPrefs.SetInt("GrowStack" + index, 2);

        else if (GrowTime[2].TotalSeconds >= 80 && stack[2] > 3)
            PlayerPrefs.SetInt("GrowStack" + index, 3);


        CheckMethodPlay(index);
    }



    void CheckMethodPlay(int index)
    {
        GrowStack[index] = PlayerPrefs.GetInt("GrowStack" + index);

        if (GrowStack[index] > 2)
            CheckMethod(index, 60);

        else if (GrowStack[index] > 1)
            CheckMethod(index, 30);

        else if (GrowStack[index] > 0)
            CheckMethod(index, 10);


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


    // 0.프리지아 1. 장미  2. 수국 
    // 스크랩터블옵젝트에 있는 스프라이트
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
                    RandomY_Flower(index);

                else if (duration == 60)
                    Random_Flower(index);

            }
        }
    }



    public void FLowerIndex(int btnIndex)
    {
        //버블x 구하기.
        Flowerindex = btnIndex;

    }

    public void harvest()
    {
        if (HarvestInstance == null)
        {
            HarvestInstance = Instantiate(HarvestPrefab, canvas.transform);
            HarvestInstance.AddComponent<CanvasGroup>();
        }

        buttonImage = PlantState[Flowerindex].GetComponent<Image>();
        if (Plant[Flowerindex].activeSelf)
        {
            if (PlantImage[Flowerindex].sprite == GetF_PlantSprite(0) || PlantImage[Flowerindex].sprite == GetF_PlantSprite(1)
                || PlantImage[Flowerindex].sprite == GetF_PlantSprite(2))
            {
                Plant[Flowerindex].SetActive(false);
                if (!Plant[Flowerindex].activeSelf)
                {
                    // 클릭된 버튼의 RectTransform을 불러오기
                    RectTransform btnRectTransform = Plant[Flowerindex].GetComponent<RectTransform>();

                    // Hnad 오브젝트의 RectTransform을 불러오기.
                    RectTransform HarvestRectTransform = HarvestInstance.GetComponent<RectTransform>();

                    // 버튼의 위치를 기준으로 hand 오브젝트의 위치를 설정.
                    Vector3 newPosition = btnRectTransform.position;
                    newPosition.y -= 300f; // 버튼의 높이만큼 아래로 이동
                    HarvestRectTransform.position = newPosition;
                }

                buttonImage.enabled = false; // 숨기기
                ResetFlower();
            }

        }

    }

    public void HarvestAniEnd()
    {
        Destroy(HarvestInstance);
        HarvestInstance = null;
    }

    void ResetFlower()
    {
        PlayerPrefs.DeleteKey("Stack" + Flowerindex);
        PlayerPrefs.DeleteKey("PlantingAfterTime" + Flowerindex);
        PlayerPrefs.DeleteKey("PlantType" + Flowerindex);
        PlayerPrefs.DeleteKey("Button Buble" + Flowerindex + "Clicked" + Flowerindex);
        PlayerPrefs.DeleteKey("StateSaveTime" + Flowerindex);
        PlayerPrefs.DeleteKey("GrowStack" + Flowerindex);
        // 추가: 삭제 후 초기화된지 확인하는 코드
        bool stackDeleted = !PlayerPrefs.HasKey("Stack" + Flowerindex);
        bool plantingAfterTimeDeleted = !PlayerPrefs.HasKey("PlantingAfterTime" + Flowerindex);
        bool plantTypeDeleted = !PlayerPrefs.HasKey("PlantType" + Flowerindex);
        bool buttonBubleDeleted = !PlayerPrefs.HasKey("Button Buble" + Flowerindex + "Clicked" + Flowerindex);

        if (stackDeleted && plantingAfterTimeDeleted && plantTypeDeleted && buttonBubleDeleted)
        {
            // 초기화된 객체에만 bubleObject[stackindex] 활성화
            if (bubleObject[Flowerindex] != null)
                bubleObject[Flowerindex].SetActive(true);
        }
    }


    //초기화 시 valuechk 에 표시
    public void Show_value()
    {
        for (int i = 0; i < 3; i++)
        {
            string PlantedTimeString = PlayerPrefs.GetString("PlantingAfterTime" + i);
            stack[i] = PlayerPrefs.GetInt("Stack" + i, 0);
            PlantType[i] = PlayerPrefs.GetInt("PlantType" + i);
            GrowStack[i] = PlayerPrefs.GetInt("GrowStack" + i);
            if (string.IsNullOrEmpty(PlantedTimeString))
            {
                timeDifference = TimeSpan.Zero;
                seconds[i] = (int)Math.Round(timeDifference.TotalSeconds);
            }
        }
    }


    public void PlantingSeed()
    {
        btnBuble = BtnManager.bubleIndex;
        // 씨앗 심을 때
        PlantingAfter = DateTime.Now.ToString();
        List<int> availableTypes = new List<int> { 0, 1, 2 }; // 사용 가능한 꽃 종류 리스트
        System.Random rand = new System.Random(); // 랜덤 생성기


        // seed+i 찾기.
        if (seedObject[btnBuble])
        {
            Image seedImage = seedObject[btnBuble].GetComponent<Image>();

            //이미지 seed 인지 확인
            if (seedImage.sprite == SeedSpr)
            {
                // 남아 있는 꽃 종류에서 랜덤하게 선택
                int randomIndex = rand.Next(0, availableTypes.Count);
                plantType[btnBuble] = availableTypes[randomIndex];
                availableTypes.RemoveAt(randomIndex); // 선택된 꽃 종류 제거

                // PlayerPrefs에서 값을 읽어옴
                string savedTime = PlayerPrefs.GetString("PlantingAfterTime" + btnBuble);

                // 값이 비어 있는지 확인
                if (string.IsNullOrEmpty(savedTime))
                {
                    // PlantingAfterTime0 1 2 에 다른 값들 부여.
                    PlayerPrefs.SetString("PlantingAfterTime" + btnBuble, PlantingAfter);
                    PlayerPrefs.SetInt("PlantType" + btnBuble, plantType[btnBuble]); // 꽃 종류 저장

                }

            }

        }



    }







}