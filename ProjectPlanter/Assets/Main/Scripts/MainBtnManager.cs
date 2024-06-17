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

    [Header("# Hand")]
    public GameObject handPrefab;
    private GameObject handInstance;
    private bool handClicked = false;

    [Header("# Seed")]
    public GameObject seedPrefab;
    private GameObject seedInstance;
    public bool seedPlanted;
/*    public GameObject x;*/
    public Sprite Seed; // 변경할 씨앗 이미지 스프라이트
    // 클릭된 버블의 인덱스
    public int bubleIndex;

    public Canvas canvas;

    void Start()
    {
        // 0 1 2 각 버튼 체크.
        for (int i = 0; i < 3; i++)
        {
            seedPlanted = PlayerPrefs.HasKey("Button Buble" + i + "Clicked" + i);

            // 심어져있는지 체크
            // 심어져있다면
            if (seedPlanted)
            {
                // 0값 받아오기.
                int value = PlayerPrefs.GetInt("Button Buble" + i + "Clicked" + i);

                if (value == 0)
                {
                    GameObject bubleObject = GameObject.Find("Button Buble" + i);
                    if (bubleObject !=null)
                    {
                        bubleObject.SetActive(false);
                    }
                }

            }

        }

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
    public void BubleIndex(int btnIndex)
    {
        //화분 potx 구하기.
        bubleIndex = btnIndex;
        

    }

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
            Plantingseed();

            clickedButton.gameObject.SetActive(false);
            PlayerPrefs.SetInt(clickedButton.name + "Clicked" +bubleIndex, 0); // 버튼 상태 저장 false
            // 상태 초기화
            handClicked = false;
            
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





    // 씨앗 image 활성화1
    public void Plantingseed()
    {
        GameObject seedObject = GameObject.Find("seed" + bubleIndex);
        
        if (seedObject != null)
        {
            seedObject.GetComponent<Image>().sprite = Seed;
            
        }
        else
        {
            Debug.Log("nullllll");
        }

    }
    
    public void PlantedSeed()
    {
   
     /*   //씨앗 심어짐 === 참 
        seedPlanted = !seedPlanted;
        // PlayerPrefs에 저장
        PlayerPrefs.SetInt("IsSeedPlanted", seedPlanted ? 1 : 0); //true 이면 1 false 0*/


    }




}
