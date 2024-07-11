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
    //stack == ȭ�п� ���Ѹ� Ƚ��
    public int[] stack = new int[3];
    string[] x = new string[3];
    string[] y = new string[3];
    public Sprite[] StateSpr;
    GameObject[] PlantState = new GameObject[3];
    Button[] Plantstate = new Button[3];
    GameObject[] Pot = new GameObject[3];
    GameObject[] seed = new GameObject[3];
    GameObject[] bubleObject = new GameObject[3];
    TimeSpan[] timeDif = new TimeSpan[3];
    public int[] painStack = new int[3];
    public int[] seconds = new int[3]; //�� ȭ�п� �ð��� üũ
    int[] value = new int[3];


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
    // Ŭ���� state�� �ε���
    int stateIndex;
    public Canvas canvas;
    private void Start()
    {
        
        instance = this;


        for (int i = 0; i < 3; i++)
        {
            Pot[i] = GameObject.Find("Pot" + i);
            seed[i] = GameObject.Find("seed" + i);
            GameObject plantStateObject = GameObject.Find("PlantState" + i);
            PlantState[i] = Pot[i].transform.Find("PlantState" + i).gameObject;
            bubleObject[i] = Pot[i].transform.Find("Button Buble" + i).gameObject;

            Plantstate[i] = plantStateObject.GetComponent<Button>();
            
            Image StateImage = Plantstate[i].GetComponent<Image>();

            //�ڷ�ƾ PlantState2 inactive ���� ������.
            if (StateImage.sprite == null)
            {
                StateImage.enabled = false;
            }
            else
            {
                StateImage.enabled = true;

            }
            //���� �ʱ�ȭ �ڵ�
            //�ʱ�ȭ�� ���� ������ 24�ð� ���� �������� �ص� 
            //������ ��� �����Ǿ� dead�� ��������.
            //�׸��Ͽ� ������ �� ������ state�� �ٲٰ� �� ���� �ʱ�ȭ
            
            x[i] = PlayerPrefs.GetString("PlantingAfterTime" + i);
            y[i] = PlayerPrefs.GetString("StateSaveTime" + i);
            if (string.IsNullOrEmpty(y[i]) && !string.IsNullOrEmpty(x[i]))
            {
                Debug.Log("����");
                InsertTIme(i, x);
                Debug.Log(timeDifference + " �ʱ�ȭ ���� ����" + i);

            }

            else if(!string.IsNullOrEmpty(y[i])) 
            {
                Debug.Log("����2");
                InsertTIme(i, y);
                Debug.Log(timeDifference + " �ʱ�ȭ �� ����" + i);
            }


        }
        
    }
    void InsertTIme(int i, string[] z)
    {

        chkDate = DateTime.Parse(z[i]);

        DateTime now = DateTime.Now;

        timeDifference = now - chkDate;
        Debug.Log(timeDifference + "�ʱ�ȭ �Ǳ� �� ���������� �ð�" + i);

        stack[i] = PlayerPrefs.GetInt("Stack" + i, 0);
        CheckAndResetStack(i);
        State(i);
        seconds[i] = (int)Math.Round(timeDifference.TotalSeconds);
        timeDifference = TimeSpan.Zero;
        if (timeDifference == TimeSpan.Zero)
        {
            string nowtime = DateTime.Now.ToString();
            PlayerPrefs.SetString("StateSaveTime" + i, nowtime);
        }


    }
        

    public void StateIndex(int btnIndex)
    {
        //statex ���ϱ�.
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
            
            
            // 80�ʰ� ������ ���� �ʱ�ȭ
            if (timeDif[index].TotalSeconds > 80)
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



    //���� 0 �ູ 1 ���� 2 �񸶸� 3
    
    void State(int i)
    {
        GameObject Sprout = Pot[i].transform.Find("Sprout" + i).gameObject;
        GameObject FlowerDemo = Pot[i].transform.Find("FlowerDemo" + i).gameObject;
        Image PlantImage = PlantState[i].GetComponent<Image>();

        /*if (timeDifference.TotalHours <= 24)*/
        if (timeDifference.TotalSeconds <= 80)
            {
            if (stack[i] ==1)
            {
                //happy
                PlantImage.sprite = StateSpr[1];
            }

            else if (stack[i] >1)
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
        // �Ϸ� ������.
        /*else if (24< timeDifference.TotalHours && timeDifference.TotalHours<48)*/
        else if (81 < timeDifference.TotalSeconds && timeDifference.TotalSeconds < 109)
        {
            //����
            PlantImage.sprite = StateSpr[2];

            GetPainStack(i);
                Debug.Log(painStack[i] + " ���ν��� " + i);
            if (painStack[i] > 0)
            {
                PlantImage.sprite = StateSpr[0];

                if (PlantImage.sprite == StateSpr[0])
                {
                    Debug.Log("���ν��� ���� ���� ����");
                    // 2�� ���� , �������� ����.
                    Sprout.SetActive(false);
                    FlowerDemo.SetActive(false);

                }
            }
        }

        //2�ϵ��� ������ �̶��
        /*else if (timeDifference.TotalHours > 48)*/

        else if (timeDifference.TotalSeconds > 110)
        {

            //����.
            PlantImage.sprite = StateSpr[0];

            if (PlantImage.sprite == StateSpr[0])
            {
                // 2�� ���� , �������� ����.
                Sprout.SetActive(false);
                FlowerDemo.SetActive(false);

            }
            
        }
    
    }



    public void StatePainOrDead(Button clickedButton)
    {
        Image buttonImage = clickedButton.GetComponent<Image>();

        
        if (buttonImage != null && buttonImage.sprite == StateSpr[2])
        {
            PainClick(clickedButton);
            buttonImage.sprite = StateSpr[1];
        }
        else if (buttonImage != null && buttonImage.sprite == StateSpr[0])
        {
            DaedClick(clickedButton);
            buttonImage.enabled = false; // �����
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

        if (TrowelInstance == null)
        {
            TrowelInstance = Instantiate(TrowelPrefab, canvas.transform);
            TrowelInstance.AddComponent<CanvasGroup>();

            FertilizerInstance = Instantiate(FertilizerPrefab, canvas.transform);
            FertilizerInstance.AddComponent<CanvasGroup>();
            FertilizerInstance.SetActive(false); // ó������ ��Ȱ��ȭ ���·� ����
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
    }
    private IEnumerator DestroyTrowelAfterDelay(float delay, Button clickedButton)
    {
        yield return new WaitForSeconds(delay);
        Destroy(TrowelInstance);

        yield return new WaitForSeconds(0.2f); // 0.5�� ���
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


    void ResetPrefs()
    {
        PlayerPrefs.DeleteKey("Stack" + stateIndex);
        PlayerPrefs.DeleteKey("PlantingAfterTime" + stateIndex);
        PlayerPrefs.DeleteKey("PlantType" + stateIndex);
        PlayerPrefs.DeleteKey("Button Buble" + stateIndex + "Clicked" + stateIndex);

        // �߰�: ���� �� �ʱ�ȭ���� Ȯ���ϴ� �ڵ�
        bool stackDeleted = !PlayerPrefs.HasKey("Stack" + stateIndex);
        bool plantingAfterTimeDeleted = !PlayerPrefs.HasKey("PlantingAfterTime" + stateIndex);
        bool plantTypeDeleted = !PlayerPrefs.HasKey("PlantType" + stateIndex);
        bool buttonBubleDeleted = !PlayerPrefs.HasKey("Button Buble" + stateIndex + "Clicked" + stateIndex);

        if (stackDeleted && plantingAfterTimeDeleted && plantTypeDeleted && buttonBubleDeleted)
        {

            // �ʱ�ȭ�� ��ü���� bubleObject[stackindex] Ȱ��ȭ
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
                    seedImage.sprite = null; // �̹����� None���� ����
                }
            }
        }
    }



    public void RequreState()
    {
        //��ť�� �� ���ð� 1�� �ʱ�ȭ -> State Happy�� �ٲٱ�
        PlayerPrefs.SetInt("Stack" + stateIndex, 1);
        int stackValue = PlayerPrefs.GetInt("Stack" + stateIndex);
        Debug.Log($"Stack{stateIndex}�� ��: {stackValue}");

        //++ PainStack 0���� �ʱ�ȭ.
        PlayerPrefs.DeleteKey("Pstack");
        int x = PlayerPrefs.GetInt("Pstack");
        Debug.Log(x + " Pstack �ʱ�ȭ ��" + stateIndex);

    }



/*    void OnApplicationQuit()
    {
        // ���� ���� �� ���� �ð� ����
        for (int i = 0; i < 3; i++)
        {
            string savetime = DateTime.Now.ToString();
            PlayerPrefs.SetString("StateSaveTime" + i, savetime);

        }
    }
*/




}
