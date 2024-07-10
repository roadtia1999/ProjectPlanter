using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;

    [Header("# Time")]
    public DateTime lastTime;       // ���� ���� �ð�
    public TimeSpan timeDifference; // �ð� ���� ��



    private void Awake()
    {
        instance = this;
        
        string savedTimeString = PlayerPrefs.GetString("SavedTime");
        // ����ð� - ���� �ð�
        if (!string.IsNullOrEmpty(savedTimeString))
        {
            // ������ ����� �ð��� �ִٸ� �ҷ��ͼ� DateTime���� ��ȯ
            lastTime = DateTime.Parse(savedTimeString);

            DateTime now = DateTime.Now;

            timeDifference = now - lastTime;

        }
        
    }

    void OnApplicationQuit()
    {
        // ���� ���� �� ���� �ð� ����
        string lastDateTimeString = DateTime.Now.ToString();
        PlayerPrefs.SetString("SavedTime", lastDateTimeString);
    }

}
