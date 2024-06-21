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

        // ���� �ٽ� ���۽� �ð� ���� 
        // --> ���İ��� �� �� ��� �ð���ŭ Ʈ���� �۵�.
        string startDateTimeString = DateTime.Now.ToString();
        PlayerPrefs.SetString("KeepSavedTime", startDateTimeString);


        // ����
        // ������ ����� �ð� �ҷ�����
        // ���۽� ���� ���� �ð� �ҷ����� Ȯ��.
        string savedTimeString = PlayerPrefs.GetString("SavedTime");
        


        if (!string.IsNullOrEmpty(savedTimeString))
        {
            // ������ ����� �ð��� �ִٸ� �ҷ��ͼ� DateTime���� ��ȯ
            lastTime = DateTime.Parse(savedTimeString);

            // ���� ���� �ð��� DateTime���� ��ȯ
            DateTime startTime = DateTime.Parse(startDateTimeString);

            // �ð� ���� ��� �� Ŭ���� ���� ������ ����
            timeDifference = startTime - lastTime;



        }
    }



 


    // ����
    // ����� Ÿ�� ���� �޼���
    void OnApplicationQuit()
    {
        // ���� ���� �� ���� �ð� ����
        string lastDateTimeString = DateTime.Now.ToString();
        // ����ÿ� �ð� ���� Ȯ��.
        Debug.Log(lastDateTimeString + "    ����ð� ����");
        PlayerPrefs.SetString("SavedTime", lastDateTimeString);
    }

}
