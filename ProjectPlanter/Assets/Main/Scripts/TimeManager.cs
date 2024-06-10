using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{
    // 게임 시작 시간
    public DateTime lastTime;

    // 게임 이어서 시작한 시간
    public DateTime keepStartTime;

    public DateTime GameTime;

    // 저장된 데이터 있는지 확인 
    private bool IsSave;


    void Start()
    {
        IsSave = PlayerPrefs.HasKey("SavedTime");

        // save가 없다면 만일을 위해 time 초기화.
        if (!IsSave)
        {
            Debug.Log("저장된 데이터가 없습니다.");
            
        }
        string startDateTimeString = DateTime.Now.ToString();
        PlayerPrefs.SetString("KeepSavedTime", startDateTimeString);
        // 이전에 저장된 시간 불러오기
        string savedTimeString = PlayerPrefs.GetString("SavedTime");
        if (!string.IsNullOrEmpty(savedTimeString))
        {
            // 이전에 저장된 시간이 있다면 불러와서 DateTime으로 변환
            lastTime = DateTime.Parse(savedTimeString);

            string KeepsavedTimeString = PlayerPrefs.GetString("KeepSavedTime");
            keepStartTime = DateTime.Parse(KeepsavedTimeString);

            /*GameTime = keepStartTime - lastTime;*/
        }
      
    }

    void OnApplicationQuit()
    {
        // 게임 종료 시 현재 시간 저장
        string lastDateTimeString = DateTime.Now.ToString();
        PlayerPrefs.SetString("SavedTime", lastDateTimeString);
    }

/*    void Update()
    {
        // 게임이 진행되는 동안의 경과 시간
        TimeSpan elapsedTime = DateTime.Now - gameStartTime;

        // 여기서 경과 시간을 필요한 형태로 사용할 수 있습니다.
        Debug.Log("Elapsed time: " + elapsedTime);
    }*/
}