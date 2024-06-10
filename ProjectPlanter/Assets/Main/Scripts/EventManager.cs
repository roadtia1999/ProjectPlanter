using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    private void Awake()
    {
        instance = this;

    }

    void Start()
    {

        TimeManager timeManager = TimeManager.instance;

            // 시간 차이 가져오기
            TimeSpan timeDif = timeManager.timeDifference;

            Debug.Log(timeDif + "   타임매니져에서 가져온 시차");

            // 시간 차이가 1시간 이상일 때 랜덤 이벤트 발생
            if (timeDif.TotalSeconds >= 30 )
            {
                TriggerRandomEvent();
            }
        
      
    }

    public void TriggerRandomEvent()
    {
        // 랜덤 값을 생성
        int randomEvent = UnityEngine.Random.Range(0, 3);

        // 랜덤 이벤트 발생
        switch (randomEvent)
        {
            case 0: Debug.Log("랜덤 이벤트 발생"); break;

            default: break;
        }
    }
}
