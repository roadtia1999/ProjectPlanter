using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Linq;


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

    // ���� ��ũ���� ���̴� ��ũ������ �ε���
    private int currentIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        // ����Ʈ �ʱ�ȭ
        recordList = new List<Texture2D>();
        recordDate = new List<string>();

        fileNames = Directory.GetFiles(Application.dataPath + "/RecordScreenshots/"); // ȭ�鿡 ��� ������ �ִ��� �˻�
        if (fileNames.Length > 0) // ������ ������
        {
            // ���� ��ü�� ���� ����Ʈȭ �ǽ�
            foreach (var file in fileNames)
            {
                string fn = file.Replace(Application.dataPath + "/RecordScreenshots/", "");

                // ���ϸ��� ���Ŀ� �´� ���� �ɷ�����
                if (fn.Contains(".png") // png ������ ��,
                    && fn.IndexOf("-") == 4
                    && fn.LastIndexOf("-") == 7 // ��¥ ������ ���ϸ��� �� ex) 1972-11-21.png 
                    && !fn.Contains(".meta")) // ����Ƽ ���� ���� ������ �ƴ� ��
                {
                    // �̹����� �ؽ��� �������� ��������
                    byte[] fileBytes = File.ReadAllBytes(file);
                    Texture2D tex = new Texture2D(0, 0);
                    tex.LoadImage(fileBytes);

                    // �������⸦ �����ϸ� ����Ʈ�� ����
                    recordDate.Add(fn.Replace(".png", ""));
                    recordList.Add(tex);
                }
            }
        }
        
        if (!recordList.Any()) // �������⿡ ������ ������ ������
        {
            // UI ��Ȱ��ȭ �� ��� ���� �޼��� ���
            leftButton.gameObject.SetActive(false);
            rightButton.gameObject.SetActive(false);
            recordsImage.gameObject.SetActive(false);
            polaroid.gameObject.SetActive(false);
            polaroidText.gameObject.SetActive(false);
            noRecordsMessage.gameObject.SetActive(true);
            return; // ��� �̹��� �ʱ�ȭ���� �ʰ� ����
        }

        // ��� �̹����� �̸� �ʱ�ȭ
        SetPolaroid();
    }

    // Update is called once per frame
    void Update()
    {
        if (recordDate.Count != 0) // �ּ��� ����� �ϳ��� ���� ���� ������ ����
        {
            // ���� ��ư Ȱ��ȭ ���� Ȯ��
            if (currentIndex == 0) // �ε����� 0�϶�
            {
                leftButton.gameObject.SetActive(false);
            }
            else
            {
                leftButton.gameObject.SetActive(true);
            }

            // ������ ��ư Ȱ��ȭ ���� Ȯ��
            if (currentIndex == recordDate.Count - 1) // �ε����� ���� �ε����϶�
            {
                rightButton.gameObject.SetActive(false);
            }
            else
            {
                rightButton.gameObject.SetActive(true);
            }
        }
    }

    public void LeftButtonClicked() // ���� ��� �ҷ����� ( �ε��� - )
    {
        currentIndex--;
        SetPolaroid();
    }

    public void RightButtonClicked() // ���� ��� �ҷ����� ( �ε��� + )
    {
        currentIndex++;
        SetPolaroid();
    }

    public void MenuButtonClicked()
    {
        SceneFade sf = GameObject.Find("FadeCanvas").GetComponent<SceneFade>();
        sf.SceneChange("MenuScene");
    }

    private void SetPolaroid() // ��� �̹����� �̸� ����
    {
        recordsImage.sprite = Sprite.Create(recordList[currentIndex], new Rect(0, 0, recordList[currentIndex].width, recordList[currentIndex].height), new Vector2(0.5f, 0.5f));
        polaroidText.text = recordDate[currentIndex];
    }

}
