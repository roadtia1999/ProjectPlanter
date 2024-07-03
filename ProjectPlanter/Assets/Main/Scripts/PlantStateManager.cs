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
    int[] stack = new int[3];
    string[] x = new string[3];
    public Sprite[] StateSpr;
    GameObject[] PlantState = new GameObject[3];
    GameObject[] Pot = new GameObject[3];
    TimeSpan[] timeDif = new TimeSpan[3];

    [Header("# Refresh")]
    public GameObject RefreshPrefab;
    private GameObject RefreshInstance;
    private bool RefreshClicked = false;

    [Header("# etc")]
    // Ŭ���� state�� �ε���
    int stateIndex;
    public Canvas canvas;
    /*public Image buttonImage;*/
    //plantstate ��ǳ�� == mainbtemag ���� Ȱ��ȭ
    private void Awake()
    {
        /*Debug.Log("state ��ũ��Ʈ ����");*/
        instance = this;
        
        for (int i = 0; i < 3; i++)
        {
            Pot[i] = GameObject.Find("Pot" + i);
            PlantState[i] = Pot[i].transform.Find("PlantState" + i).gameObject;
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

    }

    //���� 0 �ູ 1 ���� 2 �񸶸� 3
    
    void State(int i)
    {


        GameObject Sprout = Pot[i].transform.Find("Sprout" + i).gameObject;
        GameObject FlowerDemo = Pot[i].transform.Find("FlowerDemo" + i).gameObject;


        Image PlantImage = PlantState[i].GetComponent<Image>();

        //�� ���Ƕ��� ��������Ʈ�� �ƹ��͵� ǥ�� �ȉ�
        /*if (timeDifference.TotalHours <= 24)*/
        if (timeDifference.TotalSeconds <= 50)
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
        /*        else if (60 < timeDifference.TotalSeconds && timeDifference.TotalSeconds < 110)
                {
                    //����
                    PlantImage.sprite = StateSpr[2];
                }*/

        //2�ϵ��� ������ �̶��
        /*else if (timeDifference.TotalHours > 48)*/
        /*else if (timeDifference.TotalSeconds > 110)*/
        else if (timeDifference.TotalSeconds > 80)
        {

            //����.
            PlantImage.sprite = StateSpr[0];
            
            // 2�� ���� , �������� ����.
            Sprout.SetActive(false);
            FlowerDemo.SetActive(false);
            
        }
            
    }


    //���� Ȯ���غ���
    //plantstate[] �� ���� �ǵ鿩����.
    public void StatePain(Button clickedButton)
    {

        Image buttonImage = clickedButton.GetComponent<Image>();

        Debug.Log(buttonImage + " ��ư�̹���1!!");
        if (buttonImage != null && buttonImage.sprite == StateSpr[2])
        {
            // PainClick �޼��带 ȣ���Ͽ� �߰� �۾� ����
            PainClick(clickedButton);

            
        }
        else
        {
            Debug.Log("�Ⱦ���!");
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
            Debug.Log(clickedButton + " Ŭ���� ��ư@@@");
            // Ŭ���� ��ư�� RectTransform�� �ҷ�����
            RectTransform btnRectTransform = clickedButton.GetComponent<RectTransform>();

            // RefreshInstance�� RectTransform�� �ҷ�����
            RectTransform refreshRectTransform = RefreshInstance.GetComponent<RectTransform>();

            // ��ư�� ��ġ�� �������� RefreshInstance�� ��ġ�� ����
            Vector3 newPosition = btnRectTransform.position;
            newPosition.x -= 100f;
            newPosition.y -= 40f; 
            refreshRectTransform.position = newPosition;

            // RefreshInstance�� Ȱ��ȭ
            RefreshInstance.SetActive(true);

            // Refresh �ִϸ��̼��� ����ϰ�, �ִϸ��̼� ���� �� RefreshInstance ����
            StartCoroutine(PlayAnimationAndDestroy(clickedButton));

            

            RequreState();

        }
    }

    IEnumerator PlayAnimationAndDestroy(Button clickedButton)
    {
        
        yield return new WaitForSeconds(1f); // �ִϸ��̼� ��� �ð��� ��ٸ�

        // �ִϸ��̼��� ����� �� RefreshInstance ����
        if (RefreshInstance != null)
        {
            Destroy(RefreshInstance);
            RefreshInstance = null;
        }
        Debug.Log(clickedButton + " Ŭ���� ��ư");
        // ��ư ��Ȱ��ȭ
        clickedButton.gameObject.SetActive(false);


    }



    public void RequreState()
    {
        Debug.Log(stateIndex + " Ŭ���� stateindex  ��");
        PlayerPrefs.SetInt("Stack" + stateIndex, 1);

        int stackValue = PlayerPrefs.GetInt("Stack" + stateIndex);
        Debug.Log($"Stack{stateIndex}�� ��: {stackValue}");

    }



}
