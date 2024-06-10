using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// 설정 메뉴의 각종 버튼 기능을 구현한 코드입니다


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

    public void ExitButtonClicked() // 나가기 버튼 클릭
    {
        Application.Quit();
    }

    public void PlantersButtonClicked() // 메인으로 돌아가기 클릭
    {
        SceneManager.LoadScene("_MainScene");
    }

    public void PlantdexButtonClicked() // 식물도감 버튼 클릭
    {
        SceneManager.LoadScene("DexScene");
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
}
