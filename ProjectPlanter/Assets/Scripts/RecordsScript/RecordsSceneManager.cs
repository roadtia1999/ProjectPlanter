using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;


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

    // Start is called before the first frame update
    void Start()
    {
        fileNames = Directory.GetFiles(Application.dataPath + "/RecordScreenshots/"); // 화면에 띄울 파일이 있는지 검색
        if (fileNames.Length > 0) // 파일이 있으면
        {
            // 파일 전체에 대한 리스트화 실시
            foreach (var file in fileNames)
            {
                // 파일명이 형식에 맞는 파일 걸러내기
                if (file.Contains(".png") // png 파일일 때,
                    && file.IndexOf("-") == 4 
                    && file.LastIndexOf("-") == 7) // 날짜 형식의 파일명일 때 ex) 1972-11-21.png 
                {
                    // 이미지를 텍스쳐 형식으로 가져오기
                    byte[] fileBytes = File.ReadAllBytes(file);
                    Texture2D tex = new Texture2D(0, 0);
                    tex.LoadImage(fileBytes);

                    // 가져오기를 성공하면 리스트에 저장
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
