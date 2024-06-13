using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BtnManager : MonoBehaviour
{
    private int currentPage = 1;
    private int totalPage = 6;

    public Button btnNext;
    public Button btnPrev;

    public GameObject plantPr1;
    public GameObject plantPr2;
    public GameObject evText1;
    public GameObject evText2;
    public GameObject GameObject;

    private void Start()
    {
        GameObject = GetComponent<GameObject>();

        this.btnNext.onClick.AddListener(() =>
            { this.NextPage(); });

        this.btnPrev.onClick.AddListener(() =>
        { this.PrevPage(); });
    }
    public void ExitDex()
    {
        // 메뉴 씬으로 나가기
        SceneManager.LoadScene("MenuScene");
    }

    public void NextPage()
    {
        if (this.currentPage == this.totalPage)
        {
            return;
        }
        this.currentPage++;

        Debug.LogFormat("currentPage: {0}, totalPage: {1}", 
            this.currentPage, this.totalPage);

        if (this.currentPage == this.totalPage)
        {
            this.btnNext.gameObject.SetActive(false);
        }
        else
        {
            this.btnNext.gameObject.SetActive(true);
        }
        this.btnPrev.gameObject.SetActive(true);
    }

    public void PrevPage()
    {
        if (this.currentPage == 1)
        {
            return;
        }
        this.currentPage--;

        Debug.LogFormat("currentPage: {0}, totalPage: {1}",
            this.currentPage, this.totalPage);

        if (this.currentPage == 1)
        {
            this.btnPrev.gameObject.SetActive(false);
        }
        else
        {
            this.btnPrev.gameObject.SetActive(true);
        }
        this.btnNext.gameObject.SetActive(true);
    }
}
