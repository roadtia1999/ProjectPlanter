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

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
