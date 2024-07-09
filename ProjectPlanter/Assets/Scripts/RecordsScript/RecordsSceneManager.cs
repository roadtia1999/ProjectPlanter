using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Linq;


// Records 씬의 작동 전반을 담당하는 코드입니다


public class RecordsSceneManager : MonoBehaviour
{
    // 파일을 읽어오기 위해 파일이름을 저장
    private string[] fileNames;

    // 읽어올 파일 및 파일명을 저장할 List
    private List<Texture2D> recordList;
    private List<string> recordDate;

    // 스크린샷을 띄워 줄 이미지;
    public Image recordsImage;

    // 전환 버튼
    public Button leftButton;
    public Button rightButton;
    public Button menuButton;

    // 스크린샷이 없을 때 비활성화해야 하는 오브젝트들
    public Image polaroid;
    public Text polaroidText;
    public Text noRecordsMessage;

    // 현재 스크린에 보이는 스크린샷의 인덱스
    private int currentIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        // 리스트 초기화
        recordList = new List<Texture2D>();
        recordDate = new List<string>();

        fileNames = Directory.GetFiles(Application.dataPath + "/RecordScreenshots/"); // 화면에 띄울 파일이 있는지 검색
        if (fileNames.Length > 0) // 파일이 있으면
        {
            // 파일 전체에 대한 리스트화 실시
            foreach (var file in fileNames)
            {
                string fn = file.Replace(Application.dataPath + "/RecordScreenshots/", "");

                // 파일명이 형식에 맞는 파일 걸러내기
                if (fn.Contains(".png") // png 파일일 때,
                    && fn.IndexOf("-") == 4
                    && fn.LastIndexOf("-") == 7 // 날짜 형식의 파일명일 때 ex) 1972-11-21.png 
                    && !fn.Contains(".meta")) // 유니티 설정 저장 파일이 아닐 때
                {
                    // 이미지를 텍스쳐 형식으로 가져오기
                    byte[] fileBytes = File.ReadAllBytes(file);
                    Texture2D tex = new Texture2D(0, 0);
                    tex.LoadImage(fileBytes);

                    // 가져오기를 성공하면 리스트에 저장
                    recordDate.Add(fn.Replace(".png", ""));
                    recordList.Add(tex);
                }
            }
        }
        
        if (!recordList.Any()) // 가져오기에 성공한 파일이 없으면
        {
            // UI 비활성화 후 기록 없음 메세지 출력
            leftButton.gameObject.SetActive(false);
            rightButton.gameObject.SetActive(false);
            recordsImage.gameObject.SetActive(false);
            polaroid.gameObject.SetActive(false);
            polaroidText.gameObject.SetActive(false);
            noRecordsMessage.gameObject.SetActive(true);
            return; // 기록 이미지 초기화하지 않게 방지
        }

        // 기록 이미지와 이름 초기화
        SetPolaroid();
    }

    // Update is called once per frame
    void Update()
    {
        if (recordDate.Count != 0) // 최소한 기록이 하나는 남아 있을 때에만 실행
        {
            // 왼쪽 버튼 활성화 조건 확인
            if (currentIndex == 0) // 인덱스가 0일때
            {
                leftButton.gameObject.SetActive(false);
            }
            else
            {
                leftButton.gameObject.SetActive(true);
            }

            // 오른쪽 버튼 활성화 조건 확인
            if (currentIndex == recordDate.Count - 1) // 인덱스가 최종 인덱스일때
            {
                rightButton.gameObject.SetActive(false);
            }
            else
            {
                rightButton.gameObject.SetActive(true);
            }
        }
    }

    public void LeftButtonClicked() // 이전 기록 불러오기 ( 인덱스 - )
    {
        currentIndex--;
        SetPolaroid();
    }

    public void RightButtonClicked() // 다음 기록 불러오기 ( 인덱스 + )
    {
        currentIndex++;
        SetPolaroid();
    }

    public void MenuButtonClicked()
    {
        SceneFade sf = GameObject.Find("FadeCanvas").GetComponent<SceneFade>();
        sf.SceneChange("MenuScene");
    }

    private void SetPolaroid() // 기록 이미지와 이름 변경
    {
        recordsImage.sprite = Sprite.Create(recordList[currentIndex], new Rect(0, 0, recordList[currentIndex].width, recordList[currentIndex].height), new Vector2(0.5f, 0.5f));
        polaroidText.text = recordDate[currentIndex];
    }

}
