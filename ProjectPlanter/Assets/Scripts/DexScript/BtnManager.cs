using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BtnManager : MonoBehaviour
{
    public List<ItemData> database; // Dex 씬의 데이터가 들어가있는 데이터베이스
    public GameObject dexSprite; // Dex 스프라이트 부분
    public Text dexTitleText; // Dex 이름 부분
    public Text dexDescText; // Dex 설명 부분

    private int currentPage = 0; // 현재 페이지 인덱스를 저장

    // 버튼류
    public Button btnNext;
    public Button btnPrev;

    private void Start()
    {
        if (database.Count == 1) // 데이터베이스 데이터가 하나뿐일 때 예외처리
        {
            btnNext.gameObject.SetActive(false);
        }
        btnPrev.gameObject.SetActive(false);
        SetNewData(); // 데이터 초기화
    }
    public void ExitDex()
    {
        // 메뉴 씬으로 나가기
        SceneFade sf = GameObject.Find("FadeCanvas").GetComponent<SceneFade>();
        sf.SceneChange("MenuScene");
    }

    public void NextPage()
    {       
        currentPage++; // 인덱스 넘기기

        if (currentPage == database.Count - 1) // 마지막 페이지라면 다음 버튼 비활성화
        {
            btnNext.gameObject.SetActive(false);
        }
        else
        {
            btnNext.gameObject.SetActive(true);
        }
        btnPrev.gameObject.SetActive(true); // 페이지가 넘어갔으니 이전 버튼 활성화

        SetNewData(); // 데이터 갱신
    }
    public void PrevPage()
    {
        currentPage--; // 인덱스 넘기기

        if (currentPage == 0) // 첫 페이지라면 이전 버튼 비활성화
        {
            btnPrev.gameObject.SetActive(false);
        }
        else
        {
            btnPrev.gameObject.SetActive(true);
        }
        btnNext.gameObject.SetActive(true); // 페이지가 뒤로 넘어갔으니 다음 버튼 활성화

        SetNewData(); // 데이터 갱신
    }

    private void SetNewData() // 현재 도감에 보이는 데이터를 갱신하는 코드
    {
        Sprite newSprite = dexSprite.GetComponent<Sprite>();
        newSprite = database[currentPage].itemIcon; // 스프라이트 부분 갱신
        dexTitleText.text = database[currentPage].itemName; // 이름 부분 갱신
        dexDescText.text = database[currentPage].itemDesc; // 설명 부분 갱신
    }
}
