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
    // 클릭된 state의 인덱스
    int stateIndex;
    public Canvas canvas;
    /*public Image buttonImage;*/
    //plantstate 말풍선 == mainbtemag 에서 활성화
    private void Awake()
    {
        /*Debug.Log("state 스크립트 실행");*/
        instance = this;
        
        for (int i = 0; i < 3; i++)
        {
            Pot[i] = GameObject.Find("Pot" + i);
            PlantState[i] = Pot[i].transform.Find("PlantState" + i).gameObject;
            x[i] = PlayerPrefs.GetString("PlantingAfterTime" + i);

            if (!string.IsNullOrEmpty(x[i]))
            {
                
                // 이전에 저장된 시간이 있다면 불러와서 DateTime으로 변환
                chkDate = DateTime.Parse(x[i]);

                DateTime now = DateTime.Now;

                timeDifference = now - chkDate;
                stack[i] = PlayerPrefs.GetInt("Stack" + i, 0); // 저장된 값이 없으면 0을 기본값으로 사용
                CheckAndResetStack(i);
                State(i);

            }
            
            
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

            // 디버그 출력
        /*    Debug.LogFormat("Index: {0}\nStored Time: {1}\nCurrent Time: {2}\nTime Difference: {3}\nCumulative Time Difference: {4}\n",
                index, storedTime, DateTime.Now, timeDifference, timeDif[index]);*/

            // 80초가 지나면 스택 초기화
            if (timeDif[index].TotalSeconds > 80)
            {
                PlayerPrefs.SetInt("Stack" + index, 0);
                /*Debug.LogFormat("스택 초기화: Index {0}, 시간 차이가 80초를 초과했습니다.", index);*/
            }
        }

    }

    //죽음 0 행복 1 아픔 2 목마름 3
    
    void State(int i)
    {


        GameObject Sprout = Pot[i].transform.Find("Sprout" + i).gameObject;
        GameObject FlowerDemo = Pot[i].transform.Find("FlowerDemo" + i).gameObject;


        Image PlantImage = PlantState[i].GetComponent<Image>();

        //이 조건땜에 스프라이트에 아무것도 표시 안됌
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
        // 하루 미접속.
        /*else if (24< timeDifference.TotalHours && timeDifference.TotalHours<48)*/
        /*        else if (60 < timeDifference.TotalSeconds && timeDifference.TotalSeconds < 110)
                {
                    //아픔
                    PlantImage.sprite = StateSpr[2];
                }*/

        //2일동안 미접속 이라면
        /*else if (timeDifference.TotalHours > 48)*/
        /*else if (timeDifference.TotalSeconds > 110)*/
        else if (timeDifference.TotalSeconds > 80)
        {

            //죽음.
            PlantImage.sprite = StateSpr[0];
            
            // 2번 새싹 , 프리지아 데모.
            Sprout.SetActive(false);
            FlowerDemo.SetActive(false);
            
        }
            
    }


    //내일 확인해볼것
    //plantstate[] 로 뭐든 건들여보기.
    public void StatePain(Button clickedButton)
    {

        Image buttonImage = clickedButton.GetComponent<Image>();

        Debug.Log(buttonImage + " 버튼이미지1!!");
        if (buttonImage != null && buttonImage.sprite == StateSpr[2])
        {
            // PainClick 메서드를 호출하여 추가 작업 수행
            PainClick(clickedButton);

            
        }
        else
        {
            Debug.Log("안아픔!");
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
            Debug.Log(clickedButton + " 클릭된 버튼@@@");
            // 클릭된 버튼의 RectTransform을 불러오기
            RectTransform btnRectTransform = clickedButton.GetComponent<RectTransform>();

            // RefreshInstance의 RectTransform을 불러오기
            RectTransform refreshRectTransform = RefreshInstance.GetComponent<RectTransform>();

            // 버튼의 위치를 기준으로 RefreshInstance의 위치를 설정
            Vector3 newPosition = btnRectTransform.position;
            newPosition.x -= 100f;
            newPosition.y -= 40f; 
            refreshRectTransform.position = newPosition;

            // RefreshInstance를 활성화
            RefreshInstance.SetActive(true);

            // Refresh 애니메이션을 재생하고, 애니메이션 종료 후 RefreshInstance 삭제
            StartCoroutine(PlayAnimationAndDestroy(clickedButton));

            

            RequreState();

        }
    }

    IEnumerator PlayAnimationAndDestroy(Button clickedButton)
    {
        
        yield return new WaitForSeconds(1f); // 애니메이션 재생 시간을 기다림

        // 애니메이션이 종료된 후 RefreshInstance 삭제
        if (RefreshInstance != null)
        {
            Destroy(RefreshInstance);
            RefreshInstance = null;
        }
        Debug.Log(clickedButton + " 클릭된 버튼");
        // 버튼 비활성화
        clickedButton.gameObject.SetActive(false);


    }



    public void RequreState()
    {
        Debug.Log(stateIndex + " 클릭된 stateindex  값");
        PlayerPrefs.SetInt("Stack" + stateIndex, 1);

        int stackValue = PlayerPrefs.GetInt("Stack" + stateIndex);
        Debug.Log($"Stack{stateIndex}의 값: {stackValue}");

    }



}
