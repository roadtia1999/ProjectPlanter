using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;


// 배경 음악, 버튼 클릭 사운드 등을 관리하는 코드입니다


public class SoundMaker : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public AudioSource buttonClick;
    public AudioSource backgroundClick;

    private static SoundMaker instance = null;
    public static float volume;

    private void Awake()
    {

        // 사운드 게임오브젝트는 모든 씬에서 돌려쓴다

        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 변경되도 사운드 오브젝트가 파괴되지 않음
        }
    }

    // Start is called before the first frame update
    void Start() // 시작 시 저장된 볼륨 설정
    {
        volume = PlayerPrefs.GetFloat("volume");
        SetVolume();

        // BGM 스타트
        backgroundMusic.loop = true;
        backgroundMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //  볼륨이 변할 수 있는 메뉴 씬에선 지속적으로 볼륨 변경
        if (SceneManager.GetActiveScene().name == "MenuScene")
        {
            volume = PlayerPrefs.GetFloat("volume");
            SetVolume();
        }

        // 버튼 클릭 사운드 코드
        if (Input.GetMouseButtonDown(0))
        {
            // UI가 클릭된 경우의 수를 체크
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            // 만약 UI가 클릭된 것이 맞다면
            if (results != null)
            {
                // 버튼인지 확인 후 버튼 클릭 사운드 출력
                foreach (RaycastResult result in results)
                {
                    Button button = result.gameObject.GetComponent<Button>();
                    if (button != null)
                    {
                        buttonClick.Play();
                        return;
                    }
                }

                // 버튼이 아니면 일반 클릭 사운드
                backgroundClick.Play();
            }
            // UI가 아니면 일반 클릭 사운드
            else
            {
                backgroundClick.Play();    
            }
        }
    }

    // 비활성화 시 음소거 코드
    private void OnApplicationFocus(bool focus)
    {
        if (PlayerPrefs.GetInt("disableMute") == 1)
        {
            float volumeF;

            if (!focus)
            {
                volumeF = 0;
            }
            else
            {
                volumeF = PlayerPrefs.GetFloat("volume");
            }
            SetVolume(volumeF);
        }
    }

    // 볼륨을 한번에 세팅해주기 위한 코드
    private void SetVolume()
    {
        backgroundMusic.volume = volume;
        buttonClick.volume = volume;
        backgroundClick.volume = volume;
    }

    // 입력값이 있는 볼륨 세팅
    private void SetVolume(float volumeF)
    {
        backgroundMusic.volume = volumeF;
        buttonClick.volume = volumeF;
        backgroundClick.volume = volumeF;
    }
}
