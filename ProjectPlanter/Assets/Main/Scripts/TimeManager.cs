using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;

    [Header("# Time")]
    public DateTime lastTime;       // 게임 시작 시간
    public TimeSpan timeDifference; // 시간 차이 값



    private void Awake()
    {
        instance = this;
        
        string savedTimeString = PlayerPrefs.GetString("SavedTime");
        // 종료시간 - 현재 시간
        if (!string.IsNullOrEmpty(savedTimeString))
        {
            // 이전에 저장된 시간이 있다면 불러와서 DateTime으로 변환
            lastTime = DateTime.Parse(savedTimeString);

            DateTime now = DateTime.Now;

            timeDifference = now - lastTime;

        }
        
    }

    void OnApplicationQuit()
    {
        // 게임 종료 시 현재 시간 저장
        string lastDateTimeString = DateTime.Now.ToString();
        PlayerPrefs.SetString("SavedTime", lastDateTimeString);
    }

}
