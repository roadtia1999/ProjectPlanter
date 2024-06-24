using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BtnManager : MonoBehaviour
{
    public List<ItemData> database;
    public GameObject dexSprite;
    public Text dexTitleText;
    public Text dexDescText;

    private int currentPage = 0;

    public Button btnNext;
    public Button btnPrev;

    private void Start()
    {
        if (database.Count == 1)
        {
            btnNext.gameObject.SetActive(false);
        }
        btnPrev.gameObject.SetActive(false);
    }
    public void ExitDex()
    {
        // 메뉴 씬으로 나가기
        SceneFade sf = GameObject.Find("FadeCanvas").GetComponent<SceneFade>();
        sf.SceneChange("MenuScene");
    }

    public void NextPage()
    {       
        currentPage++;

        // Debug.LogFormat("currentPage: {0}, totalPage: {1}", currentPage, this.totalPage);

        if (currentPage == database.Count - 1)
        {
            btnNext.gameObject.SetActive(false);
        }
        else
        {
            btnNext.gameObject.SetActive(true);
        }
        btnPrev.gameObject.SetActive(true);

        SetNewData();
    }
    public void PrevPage()
    {
        currentPage--;

        // Debug.LogFormat("currentPage: {0}, totalPage: {1}", this.currentPage, this.totalPage);

        if (currentPage == 0)
        {
            btnPrev.gameObject.SetActive(false);
        }
        else
        {
            btnPrev.gameObject.SetActive(true);
        }
        btnNext.gameObject.SetActive(true);

        SetNewData();
    }

    private void SetNewData()
    {
        Sprite newSprite = dexSprite.GetComponent<Sprite>();
        newSprite = database[currentPage].itemIcon;
        dexTitleText.text = database[currentPage].itemName;
        dexDescText.text = database[currentPage].itemDesc;
    }
}
