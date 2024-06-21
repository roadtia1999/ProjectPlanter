using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;


// Records ���� �۵� ������ ����ϴ� �ڵ��Դϴ�


public class RecordsSceneManager : MonoBehaviour
{
    // ������ �о���� ���� �����̸��� ����
    private string[] fileNames;

    // �о�� ���� �� ���ϸ��� ������ List
    private List<Texture2D> recordList;
    private List<string> recordDate;

    // ��ũ������ ��� �� �̹���;
    public Image recordsImage;

    // ��ȯ ��ư
    public Button leftButton;
    public Button rightButton;
    public Button menuButton;

    // ��ũ������ ���� �� ��Ȱ��ȭ�ؾ� �ϴ� ������Ʈ��
    public Image polaroid;
    public Text polaroidText;
    public Text noRecordsMessage;

    // Start is called before the first frame update
    void Start()
    {
        fileNames = Directory.GetFiles(Application.dataPath + "/RecordScreenshots/"); // ȭ�鿡 ��� ������ �ִ��� �˻�
        if (fileNames.Length > 0) // ������ ������
        {
            // ���� ��ü�� ���� ����Ʈȭ �ǽ�
            foreach (var file in fileNames)
            {
                // ���ϸ��� ���Ŀ� �´� ���� �ɷ�����
                if (file.Contains(".png") // png ������ ��,
                    && file.IndexOf("-") == 4 
                    && file.LastIndexOf("-") == 7) // ��¥ ������ ���ϸ��� �� ex) 1972-11-21.png 
                {
                    // �̹����� �ؽ��� �������� ��������
                    byte[] fileBytes = File.ReadAllBytes(file);
                    Texture2D tex = new Texture2D(0, 0);
                    tex.LoadImage(fileBytes);

                    // �������⸦ �����ϸ� ����Ʈ�� ����
                    recordDate.Add(file);
                    recordList.Add(tex);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
