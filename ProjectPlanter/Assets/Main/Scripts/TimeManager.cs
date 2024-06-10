using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{
    // ���� ���� �ð�
    public DateTime lastTime;

    // ���� �̾ ������ �ð�
    public DateTime keepStartTime;

    public DateTime GameTime;

    // ����� ������ �ִ��� Ȯ�� 
    private bool IsSave;


    void Start()
    {
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
        Debug.Log(startDateTimeString + "    �ٽ� ���� �� ����");
        PlayerPrefs.SetString("KeepSavedTime", startDateTimeString);


        // ����
        // ������ ����� �ð� �ҷ�����
        // ���۽� ���� ���� �ð� �ҷ����� Ȯ��.
        string savedTimeString = PlayerPrefs.GetString("SavedTime");
        Debug.Log(savedTimeString + "   ����ð� �ҷ�����");


        if (!string.IsNullOrEmpty(savedTimeString))
        {
            // ������ ����� �ð��� �ִٸ� �ҷ��ͼ� DateTime���� ��ȯ
            lastTime = DateTime.Parse(savedTimeString);

            // ���� ���� �ð��� DateTime���� ��ȯ
            DateTime startTime = DateTime.Parse(startDateTimeString);

            // �ð� ���� ���
            TimeSpan timeDifference = startTime - lastTime;

            // �ð� ���� ���
            Debug.Log("�ð� ����: " + timeDifference);

            /*// �ð� ���̰� 1�ð� �̻��� �� ���� �̺�Ʈ �߻�
            if (timeDifference.TotalHours >= 1)
            {
                TriggerRandomEvent();
            }*/

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

/*    void Update()
    {
        // ������ ����Ǵ� ������ ��� �ð�
        TimeSpan elapsedTime = DateTime.Now - gameStartTime;

        // ���⼭ ��� �ð��� �ʿ��� ���·� ����� �� �ֽ��ϴ�.
        Debug.Log("Elapsed time: " + elapsedTime);
    }*/
}