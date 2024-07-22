using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Seed : MonoBehaviour
{
    public static Seed instance;
    public MainBtnManager BtnManager;

    //seed ��������Ʈ
    public Sprite SeedSpr;

    [Header("# SaveTime")]
    public string PlantingAfter;
    public string PlantingAfterString;

    [Header("# DateTime")]
    // ���� ���� �ð�
    private DateTime seedlastTime;
    //�ð� ���� ��.
    private TimeSpan timeDifference;
    public TimeSpan[] GrowTime = new TimeSpan[3];

    [Header("# ItemData")]
    //Itemdata�� FlowerSP �������� �뵵
    public ItemData itemData;

    [Header("# Arrangement")]
    public int[] seconds = new int[3]; //�� ȭ�п� �ð��� üũ
    public int[] PlantType = new int[3]; //ȭ�� ������ ������ Ȯ�� ����.
    public int[] stack = new int[3]; //�� �󸶳� �ѷ������� Ȯ�� ����.
    public int[] GrowStack = new int[3];
    public Image[] PlantImage = new Image[3]; //PlantImage Ÿ�Ժ��� ���尪 ����
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
                // ������ ����� �ð��� �ִٸ� �ҷ��ͼ� DateTime���� ��ȯ
                seedlastTime = DateTime.Parse(PlantedTimeString);

                // ���� ���� �ð��� DateTime���� ��ȯ
                DateTime startTime = DateTime./*Parse(PlantingAfterString);*/Now;

                // �ð� ���� ��� �� Ŭ���� ���� ������ ����
                timeDifference = startTime - seedlastTime;

                // �ð� ���� ���                
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
    // x �迭�� ����  ��� �̾ƿ��� . x[0] x[1]
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
            //���� ���ְų� �ʹ� ���� ��ٸ� -10�ʷ� ���� ����.
            // ��¥�� ������ ���� �ֱⰡ ���� ������ �����ص� ����°� ������ �ʴٰ� ����.
            timeDifference = timeDifference - TimeSpan.FromSeconds(10);


        GrowTime[i] += timeDifference;


        // x�� ���� Ȯ���Ͽ� �̺�Ʈ�� ����Ŵ
        /*TimeDifGrow(GrowTime[i], i);*/

        seconds[i] = (int)Math.Round(GrowTime[i].TotalSeconds);



    }
    //����.������ �ð� �ȿ� �������� grow���ð� �ο� ���� ����.
    //-> ������ 0Ȥ�� ���� ������ ����������.
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

    // �� �ڶ�� ����
    // �������� < ��� < ����
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
                PlayerPrefs.SetInt("PlantDexScene" + index, 1); // �Ĺ� ���� ������ PlayerPrefs�� ����
            }

        }
    }


    // 0.�������� 1. ���  2. ���� 
    // ��ũ���ͺ����Ʈ�� �ִ� ��������Ʈ
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
        //����x ���ϱ�.
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
                    // Ŭ���� ��ư�� RectTransform�� �ҷ�����
                    RectTransform btnRectTransform = Plant[Flowerindex].GetComponent<RectTransform>();

                    // Hnad ������Ʈ�� RectTransform�� �ҷ�����.
                    RectTransform HarvestRectTransform = HarvestInstance.GetComponent<RectTransform>();

                    // ��ư�� ��ġ�� �������� hand ������Ʈ�� ��ġ�� ����.
                    Vector3 newPosition = btnRectTransform.position;
                    newPosition.y -= 300f; // ��ư�� ���̸�ŭ �Ʒ��� �̵�
                    HarvestRectTransform.position = newPosition;
                }

                buttonImage.enabled = false; // �����
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
        // �߰�: ���� �� �ʱ�ȭ���� Ȯ���ϴ� �ڵ�
        bool stackDeleted = !PlayerPrefs.HasKey("Stack" + Flowerindex);
        bool plantingAfterTimeDeleted = !PlayerPrefs.HasKey("PlantingAfterTime" + Flowerindex);
        bool plantTypeDeleted = !PlayerPrefs.HasKey("PlantType" + Flowerindex);
        bool buttonBubleDeleted = !PlayerPrefs.HasKey("Button Buble" + Flowerindex + "Clicked" + Flowerindex);

        if (stackDeleted && plantingAfterTimeDeleted && plantTypeDeleted && buttonBubleDeleted)
        {
            // �ʱ�ȭ�� ��ü���� bubleObject[stackindex] Ȱ��ȭ
            if (bubleObject[Flowerindex] != null)
                bubleObject[Flowerindex].SetActive(true);
        }
    }


    //�ʱ�ȭ �� valuechk �� ǥ��
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
        // ���� ���� ��
        PlantingAfter = DateTime.Now.ToString();
        List<int> availableTypes = new List<int> { 0, 1, 2 }; // ��� ������ �� ���� ����Ʈ
        System.Random rand = new System.Random(); // ���� ������


        // seed+i ã��.
        if (seedObject[btnBuble])
        {
            Image seedImage = seedObject[btnBuble].GetComponent<Image>();

            //�̹��� seed ���� Ȯ��
            if (seedImage.sprite == SeedSpr)
            {
                // ���� �ִ� �� �������� �����ϰ� ����
                int randomIndex = rand.Next(0, availableTypes.Count);
                plantType[btnBuble] = availableTypes[randomIndex];
                availableTypes.RemoveAt(randomIndex); // ���õ� �� ���� ����

                // PlayerPrefs���� ���� �о��
                string savedTime = PlayerPrefs.GetString("PlantingAfterTime" + btnBuble);

                // ���� ��� �ִ��� Ȯ��
                if (string.IsNullOrEmpty(savedTime))
                {
                    // PlantingAfterTime0 1 2 �� �ٸ� ���� �ο�.
                    PlayerPrefs.SetString("PlantingAfterTime" + btnBuble, PlantingAfter);
                    PlayerPrefs.SetInt("PlantType" + btnBuble, plantType[btnBuble]); // �� ���� ����

                }

            }

        }



    }







}