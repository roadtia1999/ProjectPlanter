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
    public Sprite[] StateSpr;
    GameObject[] PlantState = new GameObject[3];
    GameObject[] Pot = new GameObject[3];
    GameObject[] seed = new GameObject[3];
    GameObject[] bubleObject = new GameObject[3];
    TimeSpan[] timeDif = new TimeSpan[3];

    public double seconds;
    public int[] value = new int[3];
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
            PlantState[i] = Pot[i].transform.Find("PlantState" + i).gameObject;
            bubleObject[i] = Pot[i].transform.Find("Button Buble" + i).gameObject;

            x[i] = PlayerPrefs.GetString("PlantingAfterTime" + i);


            if (!string.IsNullOrEmpty(x[i]))
            {

                // ������ ����� �ð��� �ִٸ� �ҷ��ͼ� DateTime���� ��ȯ
                chkDate = DateTime.Parse(x[i]);

                DateTime now = DateTime.Now;

                timeDifference = now - chkDate;
                stack[i] = PlayerPrefs.GetInt("Stack" + i, 0); // ����� ���� ������ 0�� �⺻������ ���
                CheckAndResetStack(i);
                State(i);

            }


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
            seconds = timeDif[index].TotalSeconds;
            // ����� ���
            /*    Debug.LogFormat("Index: {0}\nStored Time: {1}\nCurrent Time: {2}\nTime Difference: {3}\nCumulative Time Difference: {4}\n",
                    index, storedTime, DateTime.Now, timeDifference, timeDif[index]);*/

            // 80�ʰ� ������ ���� �ʱ�ȭ
            if (timeDif[index].TotalSeconds > 80)
            {
                PlayerPrefs.SetInt("Stack" + index, 0);
                /*Debug.LogFormat("���� �ʱ�ȭ: Index {0}, �ð� ���̰� 80�ʸ� �ʰ��߽��ϴ�.", index);*/
            }
            
        }
        value[index] = PlayerPrefs.GetInt("Stack" + index);


    }

    //���� 0 �ູ 1 ���� 2 �񸶸� 3
    
    void State(int i)
    {
        GameObject Sprout = Pot[i].transform.Find("Sprout" + i).gameObject;
        GameObject FlowerDemo = Pot[i].transform.Find("FlowerDemo" + i).gameObject;
        Image PlantImage = PlantState[i].GetComponent<Image>();

        //�� ���Ƕ��� ��������Ʈ�� �ƹ��͵� ǥ�� �ȉ�
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
            }

            else if (stack[i] == 0)
            {
                //state =thirsty
                PlantImage.sprite = StateSpr[3];

            }

        }
        // �Ϸ� ������.
        /*else if (24< timeDifference.TotalHours && timeDifference.TotalHours<48)*/
        /*        else if (80 < timeDifference.TotalSeconds && timeDifference.TotalSeconds < 109)
                {
                    //����
                    PlantImage.sprite = StateSpr[2];
                }*/

        //2�ϵ��� ������ �̶��
        /*else if (timeDifference.TotalHours > 48)*/
        else if (timeDifference.TotalSeconds > 80)
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


    //���� Ȯ���غ���
    //plantstate[] �� ���� �ǵ鿩����.
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


    private IEnumerator DestroyRefreshAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(RefreshInstance);
    }

    private IEnumerator DestroyTrowelAfterDelay(float delay, Button clickedButton)
    {
        yield return new WaitForSeconds(delay);
        Destroy(TrowelInstance);

        yield return new WaitForSeconds(0.2f); // 0.5�� ���
        FertilizerPlay(clickedButton);
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
            Debug.Log("PlayerPrefs �ʱ�ȭ ����.");

            // �ʱ�ȭ�� ��ü���� bubleObject[stackindex] Ȱ��ȭ
            if (bubleObject[stateIndex] != null)
            {
                bubleObject[stateIndex].SetActive(true);
            }
            else
            {
                Debug.LogWarning("bubleObject[" + stateIndex + "]��(��) null�Դϴ�.");
            }
        }
        else
        {
            Debug.LogWarning("PlayerPrefs �ʱ�ȭ ����.");
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


/*    private IEnumerator HandleButtonClick(Button clickedButton)
    {
        // ��� ��ư�� ��Ȱ��ȭ
        SetAllButtonsInteractable(false);

        DaedClick(clickedButton);

        yield return new WaitForSeconds(2f); // �ִϸ��̼� ��ü ���ӽð� (1s + 0.5s + 1s)

        // ��� ��ư�� �ٽ� Ȱ��ȭ
        SetAllButtonsInteractable(true);

        clickedButton.GetComponent<Image>().enabled = false; // Ŭ���� ��ư �����
    }

    private void SetAllButtonsInteractable(bool interactable)
    {
        foreach (Button btn in allButtons)
        {
            btn.interactable = interactable;
        }
    }
*/ //�ڷ�ƾ Ȱ��ȭ ���� ��� ��ư ��Ȱ��ȭ.
    public void RequreState()
    {
        
        PlayerPrefs.SetInt("Stack" + stateIndex, 1);

        int stackValue = PlayerPrefs.GetInt("Stack" + stateIndex);
        Debug.Log($"Stack{stateIndex}�� ��: {stackValue}");

    }




   


}
