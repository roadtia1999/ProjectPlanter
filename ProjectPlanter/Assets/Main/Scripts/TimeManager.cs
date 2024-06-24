using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;


    // ���� ���� �ð�
    public DateTime lastTime;

    // ����� ������ �ִ��� Ȯ�� 
    public bool IsSave;

    // �ð� ���� ���� ������ ����
    public TimeSpan timeDifference;



    private void Awake()
    {
        instance = this;
        // SavedTime �ִ��� ã��
        IsSave = PlayerPrefs.HasKey("SavedTime");

        // save�� ���ٸ� ������ ���� time �ʱ�ȭ.
        if (!IsSave)
        {
            Debug.Log("����� �����Ͱ� �����ϴ�.");

        }



    
        // ����
        // ������ ����� �ð� �ҷ�����
        // ���۽� ���� ���� �ð� �ҷ����� Ȯ��.
        string savedTimeString = PlayerPrefs.GetString("SavedTime");

        if (!string.IsNullOrEmpty(savedTimeString))
        {
            // ������ ����� �ð��� �ִٸ� �ҷ��ͼ� DateTime���� ��ȯ
            lastTime = DateTime.Parse(savedTimeString);

            DateTime now = DateTime.Now;
            // �ð� ���� ��� �� Ŭ���� ���� ������ ����
            timeDifference = now - lastTime;

        }
        Debug.Log("Time ����@@@@");
    }



 


    // ����
    // ����� Ÿ�� ���� �޼���
    void OnApplicationQuit()
    {
        // ���� ���� �� ���� �ð� ����
        string lastDateTimeString = DateTime.Now.ToString();
        PlayerPrefs.SetString("SavedTime", lastDateTimeString);
    }

}
