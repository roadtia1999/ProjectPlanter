using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainBtnManager : MonoBehaviour
{
    // 위치 시킬 오브젝트
    public GameObject canPrefab;

    // can 프리팹
    private GameObject canInstance;

    // 클릭 될 Ui
    public Button firstButtonClicked;

    // 클릭 여부 확인
    private bool CanClicked = false;

   // Can을 canvas에 넣기위해 선언
    public Canvas canvas;

    void Start()
    {
       
    }

    // 버튼 클릭 시
    public void CanBtnClicked()
    {
        CanClicked = true;
        Debug.Log("클릭됨");

        if (canInstance == null)
        {
            canInstance = Instantiate(canPrefab, canvas.transform);

        }
    }

    // Can 오브젝트를 해당 버튼 위에 배치
   
    public void PlaceCanOnButton(Button clickedButton)
    {
        if (CanClicked)
        {

            Debug.Log("if문 실행");
            // 클릭된 버튼의 RectTransform을 불러오기
            RectTransform btnRectTransform = clickedButton.GetComponent<RectTransform>();

            // Can 오브젝트의 RectTransform을 불러오기.
            RectTransform canRectTransform = canInstance.GetComponent<RectTransform>();

            // 버튼의 위치를 기준으로 Can 오브젝트의 위치를 설정합니다.
           Vector3 newPosition = canRectTransform.anchoredPosition;
            newPosition.x = btnRectTransform.anchoredPosition.x + 200f;
            newPosition.y = btnRectTransform.anchoredPosition.y + 400f; // y 값을 버튼의 y 값에 400만큼 이동합니다.
            canRectTransform.anchoredPosition = newPosition;

            // Can 오브젝트를 활성화합니다.
            canInstance.SetActive(true);

            // 3초 후에 Can 오브젝트를 파괴합니다.
            StartCoroutine(DestroyCanAfterDelay(3f));

            // 상태 초기화
            CanClicked = false;
            Debug.Log("false로 초기화");
        }
    }

    // 일정 시간 후에 Can 오브젝트를 파괴하는 함수
    private IEnumerator DestroyCanAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(canInstance);
    }

    public void MenuBtnClick()
    {
        /*// 다음 Scene의 이름을 적어주세요
        string nextSceneName = "MenuScene";

        // 해당 Scene으로 이동합니다.
        SceneManager.LoadScene(nextSceneName);*/


        SceneManager.LoadScene("MenuScene");
    }

}