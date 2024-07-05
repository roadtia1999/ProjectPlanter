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

    // Start is called before the first frame update
    void Start()
    {
        // 볼륨 슬라이더 값을 현재 볼륨에 맞추기
        volumeSlider.value = PlayerPrefs.GetFloat("volume");
    }

    // Update is called once per frame
    void Update()
    {
        
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
