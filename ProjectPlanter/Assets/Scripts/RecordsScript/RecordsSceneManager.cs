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

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
