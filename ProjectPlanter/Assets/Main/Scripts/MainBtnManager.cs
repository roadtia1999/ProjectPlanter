using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainBtnManager : MonoBehaviour
{
    public static MainBtnManager instance;

    [Header("# Can")]
    public GameObject canPrefab;
    private GameObject canInstance;
    private bool CanClicked = false;
    int[] stack = new int[3]; // 물뿌리게 스택 체크.

    [Header("# Hand")]
    public GameObject handPrefab;
    private GameObject handInstance;
    private bool handClicked = false;

    [Header("# Seed")]
    public GameObject seedPrefab;
    public Sprite SeedSpr; // 변경할 씨앗 이미지 스프라이트
    public bool seedPlanted; // 씨앗 심어져있는지 여부

    [Header("etc")]
    public int bubleIndex; // 클릭된 버블의 인덱스
    int potIndex; // 클릭된 화분의 인덱스 

    [Header("Canvas")]
    public Canvas canvas;
    GameObject[] PlantState = new GameObject[3];
    GameObject[] Pot = new GameObject[3];
    public Sprite[] StateSpr;




    private void Awake()
    {
        
        instance = this;
        PlantState = new GameObject[3];
        // 0 1 2 각 버튼 체크.
        for (int i = 0; i < 3; i++)
        {
            seedPlanted = PlayerPrefs.HasKey("Button Buble" + i + "Clicked" + i);
            Pot[i] = GameObject.Find("Pot" + i);
            GameObject seedObject = GameObject.Find("seed" + i);
            PlantState[i] = Pot[i].transform.Find("PlantState" + i).gameObject;

            if (seedPlanted)
            {
                // 0값 받아오기.
                int value = PlayerPrefs.GetInt("Button Buble" + i + "Clicked" + i);

                if (value == 0)
                {
                    GameObject bubleObject = GameObject.Find("Button Buble" + i);
                    if (bubleObject != null)
                    {
                        seedObject.GetComponent<Image>().sprite = SeedSpr;

                        PlantState[i].SetActive(true);
                        bubleObject.SetActive(false);
                    }
                }

            }

        }

    }
    private void Start()
    {
        stack = new int[3];

    }

    public void BubleIndex(int btnIndex)
    {
        //버블x 구하기.
        bubleIndex = btnIndex;

    }
    public void PotIndex(int btnIndex)
    {
        //potx 구하기.
        potIndex = btnIndex;

    }




    // 버튼 클릭 시
    public void CanBtnClicked()
    {
        CanClicked = !CanClicked;

        if (CanClicked)
        {
            if (canInstance == null)
                canInstance = Instantiate(canPrefab, canvas.transform);
        }

        else
        {
            Destroy(canInstance); 
            canInstance = null;   
        }
    }

    // 스택 추가 부분
    void CanStack()
    {
        int[] x = new int[3];
        stack[potIndex]++;
        x[potIndex] = stack[potIndex];   
        PlayerPrefs.SetInt("Stack"+potIndex , x[potIndex]);

        string currentTime = DateTime.Now.ToString();
        PlayerPrefs.SetString("StackTime" + potIndex, currentTime);

    }

    // Can 오브젝트를 해당 버튼 위에 배치
    public void PlaceOnButton(Button clickedButton)
    {
        
        CanStack();
        if (CanClicked)
        {
            // 클릭된 버튼의 RectTransform을 불러오기
            RectTransform btnRectTransform = clickedButton.GetComponent<RectTransform>();

            // Can 오브젝트의 RectTransform을 불러오기.
            RectTransform canRectTransform = canInstance.GetComponent<RectTransform>();

            // 버튼의 위치를 기준으로 hand 오브젝트의 위치를 설정.
            Vector3 newPosition = btnRectTransform.position;
            newPosition.y += 200f; // 버튼의 높이만큼 아래로 이동
            newPosition.x += 100f; // 버튼의 높이만큼 아래로 이동
            canRectTransform.position = newPosition;

            // 물을 원할때만 발동
            StateThirsty(clickedButton);

            // Can 오브젝트를 활성화합니다.
            canInstance.SetActive(true);

            // 3초 후에 Can 오브젝트를 파괴합니다.
            StartCoroutine(DestroyCanAfterDelay(3f));

            // 상태 초기화
            CanClicked = false;
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
        SceneFade sf = GameObject.Find("FadeCanvas").GetComponent<SceneFade>();
        sf.SceneChange("MenuScene");
    }

    // 말풍선 클릭시 -- index값 저장


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

            // Hnad 오브젝트의 RectTransform을 불러오기.
            RectTransform HandRectTransform = handInstance.GetComponent<RectTransform>();

            // 버튼의 위치를 기준으로 hand 오브젝트의 위치를 설정.
            Vector3 newPosition = btnRectTransform.position;
            newPosition.y -= 40f; // 버튼의 높이만큼 아래로 이동
            HandRectTransform.position = newPosition;


            // hand 오브젝트를 활성화.
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
        canvasGroup.alpha = 1.0f;
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
    //
    public void Plantingseed()
    {
        GameObject seedObject = GameObject.Find("seed" + bubleIndex);

        if (seedObject != null)
        {
            seedObject.GetComponent<Image>().sprite = SeedSpr;
            
        }

    }

    //목마름은 애초에 0이였기에 발생되고
    //이 메서드를 실행시키려면 목마름 상태에서 물을 주는거기에
    //스택은 올라가게 되있다. 그래서 stack을 초기화 하는것보다 이게 좋은듯 싶다.
    public void StateThirsty(Button clickedButton)
    {
        Image[] childImages = clickedButton.GetComponentsInChildren<Image>();

        foreach (Image childImage in childImages)
        {
            if (childImage.sprite == StateSpr[3])
            {
                childImage.sprite = StateSpr[1];
   
                break;
            }
        }
    }


 

}
