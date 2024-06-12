using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainBtnManager : MonoBehaviour
{
    [Header("# Can")]
    public GameObject canPrefab;
    private GameObject canInstance;
    public Button firstButtonClicked;
    private bool CanClicked = false;

    [Header("# Can")]
    public GameObject handPrefab;
    private GameObject handInstance;
    private bool handClicked = false;

    [Header("# Can")]
    public GameObject seedPrefab;
    private GameObject seedInstance;

    public Canvas canvas;

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
    public void PlaceOnButton(Button clickedButton)
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

    // 메뉴 씬 이동
    public void MenuBtnClick()
    {
        SceneManager.LoadScene("MenuScene");
    }

    // 말풍선 클릭시 --
    public void BubleCilck(Button clickedButton)
    {
        handClicked = true;

        if (handInstance == null)
        {
            handInstance = Instantiate(handPrefab, canvas.transform);
            handInstance.AddComponent<CanvasGroup>();
        }

        if (handClicked)
        {
            Debug.Log("if문 실행");
            // 클릭된 버튼의 RectTransform을 불러오기
            RectTransform btnRectTransform = clickedButton.GetComponent<RectTransform>();

            // Can 오브젝트의 RectTransform을 불러오기.
            RectTransform HandRectTransform = handInstance.GetComponent<RectTransform>();

            // 버튼의 위치를 기준으로 Can 오브젝트의 위치를 설정합니다.
            Vector3 newPosition = HandRectTransform.anchoredPosition;
            newPosition.x = btnRectTransform.anchoredPosition.x + 10f;
            newPosition.y = btnRectTransform.anchoredPosition.y + -40f; 
            HandRectTransform.anchoredPosition = newPosition;

            // hand 오브젝트를 활성화합니다.
            handInstance.SetActive(true);

            // 1.5초 후에 hand 오브젝트 a 값 서서히 다운
            StartCoroutine(FadeOutHand(1.5f));
            Plantingseed(clickedButton);

            clickedButton.gameObject.SetActive(false);
            // 상태 초기화
            handClicked = false;
            Debug.Log("false로 초기화");
        }



    }

    private IEnumerator FadeOutHand(float duration)
    {
        CanvasGroup canvasGroup = handInstance.GetComponent<CanvasGroup>();
        float startAlpha = canvasGroup.alpha;
        float rate = 1.0f / duration;
        float progress = 0.0f;

        while (progress < 1.0f)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0, progress);
            progress += rate * Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0;
        handInstance.SetActive(false);
    }



    // 씨앗 image 활성화
    public void Plantingseed(Button clickedBtn)
    {
        if (seedInstance == null)
        {
            seedInstance = Instantiate(seedPrefab, canvas.transform);
        }

        RectTransform btnRectTransform = clickedBtn.GetComponent<RectTransform>();

        // seed 오브젝트의 RectTransform을 불러오기.
        RectTransform seedRectTransform = seedInstance.GetComponent<RectTransform>();

        // 버튼의 위치를 기준으로 seed 오브젝트의 위치를 설정합니다.
        Vector3 newPosition = seedRectTransform.anchoredPosition;
        newPosition.x = btnRectTransform.anchoredPosition.x + 7f;
        newPosition.y = btnRectTransform.anchoredPosition.y + -160f; 
        seedRectTransform.anchoredPosition = newPosition;

        
        seedInstance.SetActive(true);



    }


}
