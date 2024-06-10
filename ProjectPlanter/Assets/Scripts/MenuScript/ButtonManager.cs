using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// ���� �޴��� ���� ��ư ����� ������ �ڵ��Դϴ�


public class ButtonManager : MonoBehaviour
{
    public GameObject SettingsBoard;
    public GameObject SoundsBoard;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExitButtonClicked() // ������ ��ư Ŭ��
    {
        Application.Quit();
    }

    public void PlantersButtonClicked() // �������� ���ư��� Ŭ��
    {
        SceneManager.LoadScene("_MainScene");
    }

    public void PlantdexButtonClicked() // �Ĺ����� ��ư Ŭ��
    {
        SceneManager.LoadScene("DexScene");
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
}
