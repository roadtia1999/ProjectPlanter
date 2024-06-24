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
    //stack == ȭ�п� ���Ѹ� Ƚ��
    int[] stack = new int[3];
    

    public Sprite[] StateSpr;
    //plantstate ��ǳ�� == mainbtemag ���� Ȱ��ȭ
    private void Awake()
    {
        for (int i = 0; i < 3; i++)
        {
            x[i] = PlayerPrefs.GetString("PlantingAfterTime" + i);

            if (!string.IsNullOrEmpty(x[i]))
            {
                // ������ ����� �ð��� �ִٸ� �ҷ��ͼ� DateTime���� ��ȯ
                chkDate = DateTime.Parse(x[i]);

                DateTime now = DateTime.Now;

                timeDifference = now - chkDate;

            }
            stack[i] = PlayerPrefs.GetInt("Stack" + i, 0); // ����� ���� ������ 0�� �⺻������ ���
            State(i);
            
            
        }
        Debug.Log("PlantState ����@@@@");
    }

    //���� 0 �ູ 1 ���� 2 �񸶸� 3
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
        // �Ϸ� ������.
        else if (24< timeDifference.TotalHours && timeDifference.TotalHours<48)
        {
            //����
            PlantImage.sprite = StateSpr[2];
        }

        //2�ϵ��� ������ �̶��
        else if (timeDifference.TotalHours > 48)
        {
            //����.
            PlantImage.sprite = StateSpr[0];
            // 2�� ���� , �������� ����.
            Sprout.SetActive(false);
            FreesiaDemo.SetActive(false);
        }


    }




}
