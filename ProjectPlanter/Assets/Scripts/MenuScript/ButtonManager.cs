using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


// ���� �޴��� ���� ��ư ����� ������ �ڵ��Դϴ�


public class ButtonManager : MonoBehaviour
{
    public GameObject SettingsBoard;
    public GameObject SoundsBoard;
    public Slider volumeSlider;
    public Toggle DisableMuteToggle;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("volume")) // ���� ���� ������ �ִ� ���
        {
            // ���� �����̴� ���� ���� ������ ���߱�
            volumeSlider.value = PlayerPrefs.GetFloat("volume");
        }
        else // ���� ������ ���� ���� ���
        {
            // �ִ� ���� ���¸� ����
            volumeSlider.value = 1f;
        }

        if (PlayerPrefs.HasKey("disableMute")) // ��Ȱ��ȭ �� ���Ұ� ������ �ִ� ���
        {
            if (PlayerPrefs.GetInt("disableMute") == 1) // ��Ȱ��ȭ �� ���Ұ� üũ�� �Ǿ� �ִٸ�
            {
                DisableMuteToggle.isOn = true; // ��� üũ
            }
            else // ��Ȱ��ȭ �� ���Ұ� üũ�� �Ǿ� ���� �ʴٸ�
            {
                DisableMuteToggle.isOn = false; // ��Ŭ üũ ����
            }
        }
        else // ������ ��Ȱ��ȭ �� ���Ұ� ������ ���� ���� ���
        {
            DisableMuteToggle.isOn = false; // �⺻ ���� (��� üũ ����)
        }
    }

    public void ExitButtonClicked() // ������ ��ư Ŭ��
    {
        Application.Quit();
    }

    public void PlantersButtonClicked() // �������� ���ư��� Ŭ��
    {
        SceneFade sf = GameObject.Find("FadeCanvas").GetComponent<SceneFade>();
        sf.SceneChange("_MainScene");
    }

    public void PlantdexButtonClicked() // �Ĺ����� ��ư Ŭ��
    {
        SceneFade sf = GameObject.Find("FadeCanvas").GetComponent<SceneFade>();
        sf.SceneChange("PlantDexScene");
    }

    public void EventdexButtonClicked() // �̺�Ʈ���� ��ư Ŭ��
    {
        SceneFade sf = GameObject.Find("FadeCanvas").GetComponent<SceneFade>();
        sf.SceneChange("EventDexScene");
    }

    public void RecordsButtonClicked() // ������� ��ư Ŭ��
    {
        SceneFade sf = GameObject.Find("FadeCanvas").GetComponent<SceneFade>();
        sf.SceneChange("RecordsScene");
    }

    public void SoundsButtonClicked() // �Ҹ� �޴� ��ư Ŭ��
    {
        SettingsBoard.SetActive(false);
        SoundsBoard.SetActive(true);
    }

    public void SoundsOptionExit() // �Ҹ� �޴� ������ ��ư Ŭ��
    {
        SettingsBoard.SetActive(true);
        SoundsBoard.SetActive(false);
    }

    public void VolumeSliderValueChanged() // ���� �����̴� �������� ��
    {
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
    }

    public void DisableMuteToggled() // ��Ȱ��ȭ �� ���Ұ� ��� ������ ��
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
