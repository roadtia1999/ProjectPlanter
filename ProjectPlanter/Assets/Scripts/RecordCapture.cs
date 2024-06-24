using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;


// 매일마다 메인 씬의 상황을 스크린샷으로 남기는 코드입니다


public class RecordCapture : MonoBehaviour
{
    private void Awake()
    {
        // 메인 씬 활성화와 동시에 캡쳐 시작
        StartCoroutine(CaptureSequence());
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator CaptureSequence()
    {
        // 씬이 전부 로드될 때까지 대기
        yield return new WaitForEndOfFrame();

        // 페이드 인/아웃 때문에 어둡게 스크린샷이 찍히는 경우 방지
        Image fadeImage = GameObject.Find("FadeCanvas").GetComponentInChildren<Image>(); // 씬 전환용 이미지를 가져옴
        Color fadeCheck = fadeImage.color;
        while (fadeCheck.a != 0f) // 만약 씬 전환용 이미지가 완전 투명하지 않으면 대기
        {
            yield return null;
        }

        // 현재 날짜가 지난번 기록된 날짜와 다른지 체크
        string autoCaptureDate = PlayerPrefs.GetString("AutoCaptureDate");
        if (autoCaptureDate != DateTime.Now.ToString("yyyy/MM/dd"))
        {
            // 화면의 크기를 가지는 텍스쳐를 준비
            int width = Screen.width;
            int height = Screen.height;
            Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);

            // 화면을 텍스쳐에 읽어냄
            tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            tex.Apply();

            // 텍스쳐를 PNG로 변환한 뒤 텍스쳐는 파괴
            byte[] byteData = tex.EncodeToPNG();
            Destroy(tex);

            // 만약 캡쳐 파일을 저장할 폴더가 없다면 생성
            if (!Directory.Exists(Application.dataPath + "/RecordScreenshots"))
            {
                Directory.CreateDirectory(Application.dataPath + "/RecordScreenshots");
            }

            // 변환된 PNG 파일을 어플리케이션 경로에 저장
            File.WriteAllBytes(Application.dataPath + "/RecordScreenshots/" + DateTime.Now.ToString("yyyy/MM/dd") + ".png", byteData);

            // 마지막 캡쳐 일자를 저장하는 PlayerPrefs 갱신
            PlayerPrefs.SetString("AutoCaptureDate", DateTime.Now.ToString("yyyy/MM/dd"));
        }
    }
}
