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

        // 게임 다시 시작시 시간 저장 
        // --> 알파값과 비교 후 경과 시간만큼 트리거 작동.
        string startDateTimeString = DateTime.Now.ToString();
        PlayerPrefs.SetString("KeepSavedTime", startDateTimeString);


        // 알파
        // 이전에 저장된 시간 불러오기
        // 시작시 전에 종료 시간 불러오기 확인.
        string savedTimeString = PlayerPrefs.GetString("SavedTime");
        


        if (!string.IsNullOrEmpty(savedTimeString))
        {
            // 이전에 저장된 시간이 있다면 불러와서 DateTime으로 변환
            lastTime = DateTime.Parse(savedTimeString);

            // 현재 시작 시간도 DateTime으로 변환
            DateTime startTime = DateTime.Parse(startDateTimeString);

            // 시간 차이 계산 후 클래스 레벨 변수에 저장
            timeDifference = startTime - lastTime;



        }
    }



 


    // 알파
    // 종료시 타임 저장 메서드
    void OnApplicationQuit()
    {
        // 게임 종료 시 현재 시간 저장
        string lastDateTimeString = DateTime.Now.ToString();
        // 종료시에 시간 저장 확인.
        Debug.Log(lastDateTimeString + "    종료시간 저장");
        PlayerPrefs.SetString("SavedTime", lastDateTimeString);
    }

}
