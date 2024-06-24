using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantStateManager : MonoBehaviour
{
    public DateTime chkDate;
    private TimeSpan timeDifference;
    string[] x = new string[3];
    //stack == 화분에 물뿌린 횟수
    int[] stack = new int[3];
    

    public Sprite[] StateSpr;
    //plantstate 말풍선 == mainbtemag 에서 활성화
    private void Awake()
    {
        for (int i = 0; i < 3; i++)
        {
            x[i] = PlayerPrefs.GetString("PlantingAfterTime" + i);

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
        Debug.Log("PlantState 실행@@@@");
    }

    //죽음 0 행복 1 아픔 2 목마름 3
    void State(int i)
    {
        GameObject PlantState = GameObject.Find("PlantState" + i);
        GameObject Pot = GameObject.Find("Pot" + i);
        GameObject Sprout = Pot.transform.Find("Sprout" + i).gameObject;
        GameObject FreesiaDemo = Pot.transform.Find("FreesiaDemo" + i).gameObject;


        Image PlantImage = PlantState.GetComponent<Image>();
        if (timeDifference.TotalHours <= 24)
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
        else if (24< timeDifference.TotalHours && timeDifference.TotalHours<48)
        {
            //아픔
            PlantImage.sprite = StateSpr[2];
        }

        //2일동안 미접속 이라면
        else if (timeDifference.TotalHours > 48)
        {
            //죽음.
            PlantImage.sprite = StateSpr[0];
            // 2번 새싹 , 프리지아 데모.
            Sprout.SetActive(false);
            FreesiaDemo.SetActive(false);
        }


    }




}
