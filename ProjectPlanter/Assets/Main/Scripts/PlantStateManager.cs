using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantStateManager : MonoBehaviour
{
    public DateTime chkDate;
    private TimeSpan timeDifference;

    private void Awake()
    {
        for (int i = 0; i < 3; i++)
        {
            string PlantedTimeString = PlayerPrefs.GetString("PlantingAfterTime" + i);

            if (!string.IsNullOrEmpty(PlantedTimeString))
            {
                // 이전에 저장된 시간이 있다면 불러와서 DateTime으로 변환
                chkDate = DateTime.Parse(PlantedTimeString);

                DateTime now = DateTime.Now;

                timeDifference = now - chkDate;

            }

        }
    }

    void State()
    {
        if (timeDifference.TotalHours >= 24)
        {
    

        }


    }

}
