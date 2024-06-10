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
        IsSave = PlayerPrefs.HasKey("SavedTime");

        // save�� ���ٸ� ������ ���� time �ʱ�ȭ.
        if (!IsSave)
        {
            Debug.Log("����� �����Ͱ� �����ϴ�.");
            
        }
        string startDateTimeString = DateTime.Now.ToString();
        PlayerPrefs.SetString("KeepSavedTime", startDateTimeString);
        // ������ ����� �ð� �ҷ�����
        string savedTimeString = PlayerPrefs.GetString("SavedTime");
        if (!string.IsNullOrEmpty(savedTimeString))
        {
            // ������ ����� �ð��� �ִٸ� �ҷ��ͼ� DateTime���� ��ȯ
            lastTime = DateTime.Parse(savedTimeString);

            string KeepsavedTimeString = PlayerPrefs.GetString("KeepSavedTime");
            keepStartTime = DateTime.Parse(KeepsavedTimeString);

            /*GameTime = keepStartTime - lastTime;*/
        }
      
    }

    void OnApplicationQuit()
    {
        // ���� ���� �� ���� �ð� ����
        string lastDateTimeString = DateTime.Now.ToString();
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