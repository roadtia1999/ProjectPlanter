using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;


    // 게임 시작 시간
    public DateTime lastTime;

    // 저장된 데이터 있는지 확인 
    public bool IsSave;

    // 시간 차이 값을 저장할 변수
    public TimeSpan timeDifference;



    private void Awake()
    {
        instance = this;
        // SavedTime 있는지 찾기
        IsSave = PlayerPrefs.HasKey("SavedTime");

        // save가 없다면 만일을 위해 time 초기화.
        if (!IsSave)
        {
            Debug.Log("저장된 데이터가 없습니다.");

        }



    
        // 알파
        // 이전에 저장된 시간 불러오기
        // 시작시 전에 종료 시간 불러오기 확인.
        string savedTimeString = PlayerPrefs.GetString("SavedTime");

        if (!string.IsNullOrEmpty(savedTimeString))
        {
            // 이전에 저장된 시간이 있다면 불러와서 DateTime으로 변환
            lastTime = DateTime.Parse(savedTimeString);

            DateTime now = DateTime.Now;
            // 시간 차이 계산 후 클래스 레벨 변수에 저장
            timeDifference = now - lastTime;

        }
        Debug.Log("Time 실행@@@@");
    }



 


    // 알파
    // 종료시 타임 저장 메서드
    void OnApplicationQuit()
    {
        // 게임 종료 시 현재 시간 저장
        string lastDateTimeString = DateTime.Now.ToString();
        PlayerPrefs.SetString("SavedTime", lastDateTimeString);
    }

}
