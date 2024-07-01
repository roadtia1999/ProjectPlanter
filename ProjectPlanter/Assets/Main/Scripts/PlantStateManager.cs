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
    string[] x = new string[3];
    //stack == ȭ�п� ���Ѹ� Ƚ��
    int[] stack = new int[3];
    GameObject[] PlantState = new GameObject[3];
    GameObject[] Pot = new GameObject[3];
    public Sprite[] StateSpr;


    [Header("# Refresh")]
    public GameObject RefreshPrefab;
    private GameObject RefreshInstance;
    private bool RefreshClicked = false;
    public Canvas canvas;

    //plantstate ��ǳ�� == mainbtemag ���� Ȱ��ȭ
    private void Awake()
    {
        instance = this;
        PlantState = new GameObject[3];
        for (int i = 0; i < 3; i++)
        {
            Pot[i] = GameObject.Find("Pot" + i);
            PlantState[i] = Pot[i].transform.Find("PlantState" + i).gameObject;
            x[i] = PlayerPrefs.GetString("PlantingAfterTime" + i);
            if (x[i] == null)
            {
                continue;
            }
            if (!string.IsNullOrEmpty(x[i]))
            {
                
                // ������ ����� �ð��� �ִٸ� �ҷ��ͼ� DateTime���� ��ȯ
                chkDate = DateTime.Parse(x[i]);

                DateTime now = DateTime.Now;

                timeDifference = now - chkDate;
                stack[i] = PlayerPrefs.GetInt("Stack" + i, 0); // ����� ���� ������ 0�� �⺻������ ���
                State(i);

            }
            
            
        }
        
    }

    
  

    //���� 0 �ູ 1 ���� 2 �񸶸� 3
    void State(int i)
    {


        GameObject Sprout = Pot[i].transform.Find("Sprout" + i).gameObject;
        GameObject FreesiaDemo = Pot[i].transform.Find("FreesiaDemo" + i).gameObject;


        Image PlantImage = PlantState[i].GetComponent<Image>();


        /*if (timeDifference.TotalHours <= 24)*/
        if (timeDifference.TotalSeconds <= 24)
            {
            if (stack[i] ==1)
            {
                //happy
                PlantImage.sprite = StateSpr[1];
            }

            else if (stack[i] >2)
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
        else if (60 < timeDifference.TotalSeconds && timeDifference.TotalSeconds < 110)
        {
            //����
            PlantImage.sprite = StateSpr[2];
        }

        //2�ϵ��� ������ �̶��
        /*else if (timeDifference.TotalHours > 48)*/
        else if (timeDifference.TotalSeconds > 110)
        {
            
            //����.
            PlantImage.sprite = StateSpr[0];
            // 2�� ���� , �������� ����.
            Sprout.SetActive(false);
            FreesiaDemo.SetActive(false);
        }


        // state �۵� �ڵ� ����
   /*     if (stack[i] == 1)
        {
            if (timeDifference.TotalSeconds < 150)
            {
                //state hppy
            }
            else
            {
                //����
            }

        }
        else if (stack[i] >= 2)
        {
            //state pain
            if (timeDifference.TotalSeconds < 150)
            {
                //state dead
            }

        }

        else if (stack[i] == 0)
        {
            // �񸶸� 
            if (timeDifference.TotalSeconds < 150)
            {
                //state dead
            }
        }*/
    }



    public void StatePain(Button clickedButton)
    {
        Image buttonImage = clickedButton.GetComponent<Image>();

        if (buttonImage != null && buttonImage.sprite == StateSpr[2])
        {
            // PainClick �޼��带 ȣ���Ͽ� �߰� �۾� ����
            PainClick(clickedButton);

            // ��ư�� �̹����� �ູ ���·� ����
            buttonImage.sprite = StateSpr[1];
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
            // Ŭ���� ��ư�� RectTransform�� �ҷ�����
            RectTransform btnRectTransform = clickedButton.GetComponent<RectTransform>();

            // RefreshInstance�� RectTransform�� �ҷ�����
            RectTransform refreshRectTransform = RefreshInstance.GetComponent<RectTransform>();

            // ��ư�� ��ġ�� �������� RefreshInstance�� ��ġ�� ����
            Vector3 newPosition = btnRectTransform.position;
            newPosition.x -= 100f; // ��ư�� ���̸�ŭ �Ʒ��� �̵�
            newPosition.y -= 40f; // ��ư�� ���̸�ŭ �Ʒ��� �̵�
            refreshRectTransform.position = newPosition;

            // RefreshInstance�� Ȱ��ȭ
            RefreshInstance.SetActive(true);

            // ��ư ��Ȱ��ȭ
            clickedButton.gameObject.SetActive(false);

            // Refresh �ִϸ��̼��� ����ϰ�, �ִϸ��̼� ���� �� RefreshInstance ����
            StartCoroutine(PlayAnimationAndDestroy());
        }
    }

    IEnumerator PlayAnimationAndDestroy()
    {
        // TODO: RefreshInstance�� �ִϸ��̼��� �߰��ϰ� ����ϴ� �ڵ带 ���⿡ �ۼ�
        yield return new WaitForSeconds(1f); // �ִϸ��̼� ��� �ð��� ��ٸ�

        // �ִϸ��̼��� ����� �� RefreshInstance ����
        if (RefreshInstance != null)
        {
            Destroy(RefreshInstance);
            RefreshInstance = null;
        }

        // �ٽ� ��ư�� Ȱ��ȭ
        // ���⿡ �ʿ��� ������ �߰��Ͽ� ��ư�� �ٽ� Ȱ��ȭ�� �� �ֽ��ϴ�.
    }





}
