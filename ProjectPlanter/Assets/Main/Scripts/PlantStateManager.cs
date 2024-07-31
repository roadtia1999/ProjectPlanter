using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantStateManager : MonoBehaviour
{
    public static PlantStateManager instance;

    public DateTime chkDate;
    private TimeSpan timeDifference;

    [Header("# Array")]
    //stack == 화분에 물뿌린 횟수
    public Sprite[] StateSpr;
    int[] stack = new int[3];
    string[] x = new string[3];
    string[] y = new string[3];
    GameObject[] PlantState = new GameObject[3];
    Button[] Plantstate = new Button[3];
    GameObject[] Pot = new GameObject[3];
    GameObject[] seed = new GameObject[3];
    public GameObject[] bubleObject = new GameObject[3];
    TimeSpan[] timeDif = new TimeSpan[3];
    public int[] painStack = new int[3]; //pain stack -> 2스택이면 dead 
    public int[] TimeDifseconds = new int[3]; //각 화분에 시간값 체크
    public int[] value = new int[3];
    GameObject[] Sprout = new GameObject[3];
    GameObject[] Plant = new GameObject[3];


    [Header("# Refresh")]
    public GameObject RefreshPrefab;
    private GameObject RefreshInstance;
    private bool RefreshClicked = false;

    [Header("# Trowel")]
    public GameObject TrowelPrefab;
    private GameObject TrowelInstance;
    private bool TrowelClicked = false;

    [Header("# Fertilizer")]
    public GameObject FertilizerPrefab;
    private GameObject FertilizerInstance;


    [Header("# etc")]
    // 클릭된 state의 인덱스
    int stateIndex;
    public Canvas canvas;
    Image[] PlantImage = new Image[3];
    Seed seedCode;

    private void Start()
    {

        instance = this;

        for (int i = 0; i < 3; i++)
        {
            Pot[i] = GameObject.Find("Pot" + i);
            seed[i] = GameObject.Find("seed" + i);
            GameObject plantStateObject = GameObject.Find("PlantState" + i);
            PlantState[i] = GameObject.Find("PlantState" + i);
            Sprout[i] = Pot[i].transform.Find("Sprout" + i).gameObject;
            Plant[i] = Pot[i].transform.Find("FlowerDemo" + i).gameObject;

            Plantstate[i] = plantStateObject.GetComponent<Button>();

            Image StateImage = Plantstate[i].GetComponent<Image>();
            PlantImage[i] = Plant[i].GetComponent<Image>();

            if (bubleObject[i].activeSelf)
            {
                StateImage.enabled = false;
            }
            else
            {
                StateImage.enabled = true;
            }

            //시차 초기화 코드
            //초기화를 하지 않으면 24시간 전에 재접속을 해도 
            //시차는 계속 증가되어 dead로 만들어버림.
            //그리하여 재접속 할 때마다 state를 바꾸고 난 다음 초기화

            x[i] = PlayerPrefs.GetString("PlantingAfterTime" + i);
            y[i] = PlayerPrefs.GetString("StateSaveTime" + i);
            if (string.IsNullOrEmpty(y[i]) && !string.IsNullOrEmpty(x[i]))
            {
                InsertTIme(i, x);
            }

            else if (!string.IsNullOrEmpty(y[i]))
            {
                InsertTIme(i, y);
            }
        }

        seedCode = GameObject.Find("SeedManager").GetComponent<Seed>();
    }

    void InsertTIme(int i, string[] z)
    {

        chkDate = DateTime.Parse(z[i]);

        DateTime now = DateTime.Now;

        timeDifference = now - chkDate;

        stack[i] = PlayerPrefs.GetInt("Stack" + i, 0);
        CheckAndResetStack(i);
        State(i);
        TimeDifseconds[i] = (int)Math.Round(timeDifference.TotalSeconds);

        //80초 전 재접시 스택이 0이라면 painStack ++
        if (timeDifference.TotalSeconds < 81 && stack[i] == 0)
        {
            SetPainStack();
            GetPainStack(i);
        }

        // 재접속 해도 81초 후에는 초기화 시키지않기.
        if (timeDifference.TotalSeconds > 81)
            return;

        timeDifference = TimeSpan.Zero;
        if (timeDifference == TimeSpan.Zero)
        {
            string nowtime = DateTime.Now.ToString();
            PlayerPrefs.SetString("StateSaveTime" + i, nowtime);
        }
    }


    public void StateIndex(int btnIndex)
    {
        //statex 구하기.
        stateIndex = btnIndex;
    }


    void CheckAndResetStack(int index)
    {
        string stackTimeKey = "StackTime" + index;

        if (PlayerPrefs.HasKey(stackTimeKey))
        {
            string storedTime = PlayerPrefs.GetString(stackTimeKey);
            DateTime lastSavedTime = DateTime.Parse(storedTime);
            TimeSpan timeDifference = DateTime.Now - lastSavedTime;
            timeDif[index] += timeDifference;


            // 80초가 지나면 스택 초기화
            if (timeDif[index].TotalSeconds > 79)
            {
                PlayerPrefs.SetInt("Stack" + index, 0);
            }

        }
        value[index] = PlayerPrefs.GetInt("Stack" + index);


    }

    void SetPainStack()
    {
        PlayerPrefs.SetInt("Pstack", 1);

    }

    void GetPainStack(int index)
    {
        painStack[index] += PlayerPrefs.GetInt("Pstack");
    }



    //죽음 0 행복 1 아픔 2 목마름 3

    void State(int i)
    {

        GameObject Sprout = Pot[i].transform.Find("Sprout" + i).gameObject;
        GameObject FlowerDemo = Pot[i].transform.Find("FlowerDemo" + i).gameObject;
        Image PlantImage = PlantState[i].GetComponent<Image>();

        /*if (timeDifference.TotalHours <= 24)*/
        if (timeDifference.TotalSeconds <= 80)
        {
            //만약 페인스택이 2 이라면 죽음.
            if (painStack[i] > 1)
            {
                PlantImage.sprite = StateSpr[0];
                return;
            }
            if (stack[i] == 1)
            {
                //happy
                PlantImage.sprite = StateSpr[1];
            }

            else if (stack[i] > 1)
            {
                //state = pain
                PlantImage.sprite = StateSpr[2];
                SetPainStack();
                GetPainStack(i);

            }

            else if (stack[i] == 0)
            {
                //state =thirsty
                PlantImage.sprite = StateSpr[3];


            }

        }
        // 하루 미접속.
        /*else if (24< timeDifference.TotalHours && timeDifference.TotalHours<48)*/
        else if (80 < timeDifference.TotalSeconds && timeDifference.TotalSeconds <= 109)
        {
            //아픔
            PlantImage.sprite = StateSpr[2];

            GetPainStack(i);
            if (painStack[i] > 0)
            {
                PlantImage.sprite = StateSpr[0];

                if (PlantImage.sprite == StateSpr[0])
                {
                    Sprout.SetActive(false);
                    FlowerDemo.SetActive(false);
                }
            }
        }

        //2일동안 미접속 이라면
        /*else if (timeDifference.TotalHours > 48)*/

        else if (timeDifference.TotalSeconds > 109)
        {

            //죽음.
            PlantImage.sprite = StateSpr[0];

            if (PlantImage.sprite == StateSpr[0])
            {
                Sprout.SetActive(false);
                FlowerDemo.SetActive(false);
            }

        }

    }



    public void StatePainOrDead(Button clickedButton)
    {
        Image buttonImage = clickedButton.GetComponent<Image>();

        //State 가 Pain 일 때
        if (buttonImage != null && buttonImage.sprite == StateSpr[2])
        {
            PainClick(clickedButton);
            buttonImage.sprite = StateSpr[1];
        }
        //Dead 일 때
        else if (buttonImage != null && buttonImage.sprite == StateSpr[0])
        {
            DaedClick(clickedButton);
            buttonImage.enabled = false; // 숨기기
        }
    }

    void PainClick(Button clickedButton)
    {
        RefreshClicked = true;

        if (RefreshInstance == null)
        {
            RefreshInstance = Instantiate(RefreshPrefab, canvas.transform);
            RefreshInstance.AddComponent<CanvasGroup>();
        }

        if (RefreshClicked)
        {

            RectTransform btnRectTransform = clickedButton.GetComponent<RectTransform>();
            RectTransform refreshRectTransform = RefreshInstance.GetComponent<RectTransform>();

            Vector3 newPosition = btnRectTransform.position;
            newPosition.x -= 100f;
            newPosition.y -= 40f;
            refreshRectTransform.position = newPosition;

            RefreshInstance.SetActive(true);

            StartCoroutine(DestroyRefreshAfterDelay(1f));
            RequreState();
        }
    }

    private IEnumerator DestroyRefreshAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(RefreshInstance);
    }

    void DaedClick(Button clickedButton)
    {
        TrowelClicked = true;

        int nowDeadStack = PlayerPrefs.GetInt("DeadStack" + stateIndex);
        int DeadStackValue = 0;
        if (nowDeadStack >= 1)
        {
            DeadStackValue++;
            PlayerPrefs.SetInt("DeadStack" + stateIndex, DeadStackValue);

        }
        else
        {
            DeadStackValue++;
            PlayerPrefs.SetInt("DeadStack" + stateIndex, 1);

        }

        if (TrowelInstance == null)
        {
            TrowelInstance = Instantiate(TrowelPrefab, canvas.transform);
            TrowelInstance.AddComponent<CanvasGroup>();

            FertilizerInstance = Instantiate(FertilizerPrefab, canvas.transform);
            FertilizerInstance.AddComponent<CanvasGroup>();
            FertilizerInstance.SetActive(false); // 처음에는 비활성화 상태로 시작
        }

        if (TrowelClicked)
        {

            RectTransform btnRectTransform = clickedButton.GetComponent<RectTransform>();
            RectTransform trowelRectTransform = TrowelInstance.GetComponent<RectTransform>();

            Vector3 newPosition = btnRectTransform.position;
            newPosition.x -= 100f;
            newPosition.y -= 60f;
            trowelRectTransform.position = newPosition;

            TrowelInstance.SetActive(true);

            StartCoroutine(DestroyTrowelAfterDelay(1f, clickedButton));
            ChangeSeedImageToNone();
        }

        if (PlantImage[stateIndex] != null && PlantImage[stateIndex].enabled)
        {
            PlantImage[stateIndex].enabled = false;


        }

    }
    private IEnumerator DestroyTrowelAfterDelay(float delay, Button clickedButton)
    {
        yield return new WaitForSeconds(delay);
        Destroy(TrowelInstance);

        yield return new WaitForSeconds(0.2f); // 0.5초 대기
        FertilizerPlay(clickedButton);
    }

    void FertilizerPlay(Button clickedButton)
    {
        RectTransform btnRectTransform = clickedButton.GetComponent<RectTransform>();
        RectTransform fertilizerRectTransform = FertilizerInstance.GetComponent<RectTransform>();

        Vector3 newPosition = btnRectTransform.position;
        newPosition.x -= 60f;
        newPosition.y += 40f;
        fertilizerRectTransform.position = newPosition;

        FertilizerInstance.SetActive(true);

        StartCoroutine(DestroyFertilizerAfterDelay(1f));

        ResetPrefs();
    }

    private IEnumerator DestroyFertilizerAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(FertilizerInstance);
    }

    //씨앗 다시 심기 위해 초기화
    public void ResetPrefs()
    {

        PlayerPrefs.DeleteKey("Stack" + stateIndex);
        PlayerPrefs.DeleteKey("PlantingAfterTime" + stateIndex);
        PlayerPrefs.DeleteKey("PlantType" + stateIndex);
        PlayerPrefs.DeleteKey("Button Buble" + stateIndex + "Clicked" + stateIndex);
        PlayerPrefs.DeleteKey("StateSaveTime" + stateIndex);
        PlayerPrefs.DeleteKey("GrowStack" + stateIndex);
        // 추가: 삭제 후 초기화된지 확인하는 코드
        bool stackDeleted = !PlayerPrefs.HasKey("Stack" + stateIndex);
        bool plantingAfterTimeDeleted = !PlayerPrefs.HasKey("PlantingAfterTime" + stateIndex);
        bool plantTypeDeleted = !PlayerPrefs.HasKey("PlantType" + stateIndex);
        bool buttonBubleDeleted = !PlayerPrefs.HasKey("Button Buble" + stateIndex + "Clicked" + stateIndex);

        if (stackDeleted && plantingAfterTimeDeleted && plantTypeDeleted && buttonBubleDeleted)
        {
            // 초기화된 객체에만 bubleObject[stackindex] 활성화
            if (bubleObject[stateIndex] != null)
                bubleObject[stateIndex].SetActive(true);
        }

    }


    void ChangeSeedImageToNone()
    {
        for (int i = 0; i < 3; i++)
        {
            if (seed[i] != null)
            {
                Image seedImage = seed[i].GetComponent<Image>();
                if (seedImage != null)
                {
                    seedImage.sprite = null; // 이미지를 None으로 변경
                    seedCode.seconds[i] = 0;
                }
            }
        }
    }



    public void RequreState()
    {
        //리큐어 시 스택값 1로 초기화 -> State Happy로 바꾸기
        PlayerPrefs.SetInt("Stack" + stateIndex, 1);

        //++ PainStack 0으로 초기화.
        PlayerPrefs.DeleteKey("Pstack");

        //TIMEDIF 값 초기화
        timeDifference = TimeSpan.Zero;

    }

}
