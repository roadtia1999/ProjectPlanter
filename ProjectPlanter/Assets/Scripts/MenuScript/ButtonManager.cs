using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


// 설정 메뉴의 각종 버튼 기능을 구현한 코드입니다


public class ButtonManager : MonoBehaviour
{
    public GameObject SettingsBoard;
    public GameObject SoundsBoard;
    public Slider volumeSlider;
    public Toggle DisableMuteToggle;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("volume")) // 현재 볼륨 설정이 있는 경우
        {
            // 볼륨 슬라이더 값을 현재 볼륨에 맞추기
            volumeSlider.value = PlayerPrefs.GetFloat("volume");
        }
        else // 볼륨 설정을 하지 않은 경우
        {
            // 최대 볼륨 상태를 유지
            volumeSlider.value = 1f;
        }

        if (PlayerPrefs.HasKey("disableMute")) // 비활성화 시 음소거 설정이 있는 경우
        {
            if (PlayerPrefs.GetInt("disableMute") == 1) // 비활성화 시 음소거 체크가 되어 있다면
            {
                DisableMuteToggle.isOn = true; // 토글 체크
            }
            else // 비활성화 시 음소거 체크가 되어 있지 않다면
            {
                DisableMuteToggle.isOn = false; // 토클 체크 끄기
            }
        }
        else // 기존에 비활성화 시 음소거 설정을 하지 않은 경우
        {
            DisableMuteToggle.isOn = false; // 기본 설정 (토글 체크 끄기)
        }
    }

    public void ExitButtonClicked() // 나가기 버튼 클릭
    {
        Application.Quit();
    }

    public void PlantersButtonClicked() // 메인으로 돌아가기 클릭
    {
        SceneFade sf = GameObject.Find("FadeCanvas").GetComponent<SceneFade>();
        sf.SceneChange("_MainScene");
    }

    public void PlantdexButtonClicked() // 식물도감 버튼 클릭
    {
        SceneFade sf = GameObject.Find("FadeCanvas").GetComponent<SceneFade>();
        sf.SceneChange("PlantDexScene");
    }

    public void EventdexButtonClicked() // 이벤트도감 버튼 클릭
    {
        SceneFade sf = GameObject.Find("FadeCanvas").GetComponent<SceneFade>();
        sf.SceneChange("EventDexScene");
    }

    public void RecordsButtonClicked() // 관찰기록 버튼 클릭
    {
        SceneFade sf = GameObject.Find("FadeCanvas").GetComponent<SceneFade>();
        sf.SceneChange("RecordsScene");
    }

    public void SoundsButtonClicked() // 소리 메뉴 버튼 클릭
    {
        SettingsBoard.SetActive(false);
        SoundsBoard.SetActive(true);
    }

    public void SoundsOptionExit() // 소리 메뉴 나가기 버튼 클릭
    {
        SettingsBoard.SetActive(true);
        SoundsBoard.SetActive(false);
    }

    public void VolumeSliderValueChanged() // 볼륨 슬라이더 움직였을 때
    {
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
    }

    public void DisableMuteToggled() // 비활성화 시 음소거 토글 눌렀을 때
    {
        if (DisableMuteToggle.isOn)
        {
            PlayerPrefs.SetInt("disableMute", 1);
        }
        else
        {
            PlayerPrefs.SetInt("disableMute", 0);
        }
    }
}
