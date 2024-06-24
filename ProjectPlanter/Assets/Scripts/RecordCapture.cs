using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;


// ���ϸ��� ���� ���� ��Ȳ�� ��ũ�������� ����� �ڵ��Դϴ�


public class RecordCapture : MonoBehaviour
{
    private void Awake()
    {
        // ���� �� Ȱ��ȭ�� ���ÿ� ĸ�� ����
        StartCoroutine(CaptureSequence());
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator CaptureSequence()
    {
        // ���� ���� �ε�� ������ ���
        yield return new WaitForEndOfFrame();

        // ���̵� ��/�ƿ� ������ ��Ӱ� ��ũ������ ������ ��� ����
        Image fadeImage = GameObject.Find("FadeCanvas").GetComponentInChildren<Image>(); // �� ��ȯ�� �̹����� ������
        Color fadeCheck = fadeImage.color;
        while (fadeCheck.a != 0f) // ���� �� ��ȯ�� �̹����� ���� �������� ������ ���
        {
            yield return null;
        }

        // ���� ��¥�� ������ ��ϵ� ��¥�� �ٸ��� üũ
        string autoCaptureDate = PlayerPrefs.GetString("AutoCaptureDate");
        if (autoCaptureDate != DateTime.Now.ToString("yyyy/MM/dd"))
        {
            // ȭ���� ũ�⸦ ������ �ؽ��ĸ� �غ�
            int width = Screen.width;
            int height = Screen.height;
            Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);

            // ȭ���� �ؽ��Ŀ� �о
            tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            tex.Apply();

            // �ؽ��ĸ� PNG�� ��ȯ�� �� �ؽ��Ĵ� �ı�
            byte[] byteData = tex.EncodeToPNG();
            Destroy(tex);

            // ���� ĸ�� ������ ������ ������ ���ٸ� ����
            if (!Directory.Exists(Application.dataPath + "/RecordScreenshots"))
            {
                Directory.CreateDirectory(Application.dataPath + "/RecordScreenshots");
            }

            // ��ȯ�� PNG ������ ���ø����̼� ��ο� ����
            File.WriteAllBytes(Application.dataPath + "/RecordScreenshots/" + DateTime.Now.ToString("yyyy/MM/dd") + ".png", byteData);

            // ������ ĸ�� ���ڸ� �����ϴ� PlayerPrefs ����
            PlayerPrefs.SetString("AutoCaptureDate", DateTime.Now.ToString("yyyy/MM/dd"));
        }
    }
}
