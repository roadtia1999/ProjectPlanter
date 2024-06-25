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
    //stack == 화분에 물뿌린 횟수
    int[] stack = new int[3];
    GameObject[] PlantState = new GameObject[3];

    public Sprite[] StateSpr;


    [Header("# Refresh")]
    public GameObject RefreshPrefab;
    private GameObject RefreshInstance;
    private bool RefreshClicked = false;
    public Canvas canvas;

    //plantstate 말풍선 == mainbtemag 에서 활성화
    private void Awake()
    {
        instance = this;

        for (int i = 0; i < 3; i++)
        {
            x[i] = PlayerPrefs.GetString("PlantingAfterTime" + i);
            if (x[i] == null)
            {
                continue;
            }
            if (!string.IsNullOrEmpty(x[i]))
            {
                // 이전에 저장된 시간이 있다면 불러와서 DateTime으로 변환
                chkDate = DateTime.Parse(x[i]);

                DateTime now = DateTime.Now;

                timeDifference = now - chkDate;

            }
            stack[i] = PlayerPrefs.GetInt("Stack" + i, 0); // 저장된 값이 없으면 0을 기본값으로 사용
            State(i);
            
            
        }
        
    }

    // 문제 . continue로 피하려고 해도 state에 i값을 대입해서 심지 않은 화분에도 state변화


    //죽음 0 행복 1 아픔 2 목마름 3
    void State(int i)
    {
        PlantState[i] = GameObject.Find("PlantState" + i);
        GameObject Pot = GameObject.Find("Pot" + i);
        GameObject Sprout = Pot.transform.Find("Sprout" + i).gameObject;
        GameObject FreesiaDemo = Pot.transform.Find("FreesiaDemo" + i).gameObject;


        Image PlantImage = PlantState[i].GetComponent<Image>();
        /*if (timeDifference.TotalHours <= 24)*/
            if (timeDifference.TotalSeconds <= 24)
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
        else if (24 < timeDifference.TotalSeconds && timeDifference.TotalSeconds < 48)
        {
            //아픔
            PlantImage.sprite = StateSpr[2];
        }

        //2일동안 미접속 이라면
        /*else if (timeDifference.TotalHours > 48)*/
        else if (timeDifference.TotalSeconds > 48)
        {
            if (i==2)
            {
                Debug.Log("3번째 씨앗 실행");
            }
            //죽음.
            PlantImage.sprite = StateSpr[0];
            // 2번 새싹 , 프리지아 데모.
            Sprout.SetActive(false);
            FreesiaDemo.SetActive(false);
        }


    }

    public void StateThirsty()
    {
        for (int i = 0; i < 3; i++)
        {
            Image PlantImage = PlantState[i].GetComponent<Image>();
            
            if (PlantImage.sprite == StateSpr[3])
            {
                // 이 메서드 실행되면 행복으로 바꿈.
                PlantImage.sprite = StateSpr[1];

            }
            
        }
    }

    public void StatePain(Button clickedButton)
    {
        Image buttonImage = clickedButton.GetComponent<Image>();

        if (buttonImage != null && buttonImage.sprite == StateSpr[2])
        {
            // PainClick 메서드를 호출하여 추가 작업 수행
            PainClick(clickedButton);

            // 버튼의 이미지를 행복 상태로 변경
            buttonImage.sprite = StateSpr[1];
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
            // 클릭된 버튼의 RectTransform을 불러오기
            RectTransform btnRectTransform = clickedButton.GetComponent<RectTransform>();

            // RefreshInstance의 RectTransform을 불러오기
            RectTransform refreshRectTransform = RefreshInstance.GetComponent<RectTransform>();

            // 버튼의 위치를 기준으로 RefreshInstance의 위치를 설정
            Vector3 newPosition = btnRectTransform.position;
            newPosition.y -= 40f; // 버튼의 높이만큼 아래로 이동
            refreshRectTransform.position = newPosition;

            // RefreshInstance를 활성화
            RefreshInstance.SetActive(true);

            // 버튼 비활성화
            clickedButton.gameObject.SetActive(false);

            // Refresh 애니메이션을 재생하고, 애니메이션 종료 후 RefreshInstance 삭제
            StartCoroutine(PlayAnimationAndDestroy());
        }
    }

    IEnumerator PlayAnimationAndDestroy()
    {
        // TODO: RefreshInstance에 애니메이션을 추가하고 재생하는 코드를 여기에 작성
        yield return new WaitForSeconds(1f); // 애니메이션 재생 시간을 기다림

        // 애니메이션이 종료된 후 RefreshInstance 삭제
        if (RefreshInstance != null)
        {
            Destroy(RefreshInstance);
            RefreshInstance = null;
        }

        // 다시 버튼을 활성화
        // 여기에 필요한 로직을 추가하여 버튼을 다시 활성화할 수 있습니다.
    }





}
