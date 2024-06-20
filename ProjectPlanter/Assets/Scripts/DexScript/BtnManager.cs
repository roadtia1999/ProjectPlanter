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
    public GameObject plantPr3;
    public GameObject plantPr4;
    public GameObject plantPr5;
    public GameObject plantPr6;

    public GameObject evText1;
    public GameObject evText2;
    public GameObject evText3;
    public GameObject evText4;
    public GameObject evText5;
    public GameObject evText6;

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
        // �޴� ������ ������
        SceneFade sf = GameObject.Find("FadeCanvas").GetComponent<SceneFade>();
        sf.SceneChange("MenuScene");
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

        if (currentPage == 1)
        {
            plantPr1.gameObject.SetActive(true);
            evText1.gameObject.SetActive(true);
        }
        else
        {
            plantPr1.gameObject.SetActive(false);
            evText1.gameObject.SetActive(false);
        }

        if (currentPage == 2)
        {
            plantPr2.gameObject.SetActive(true);
            evText2.gameObject.SetActive(true);
        }
        else
        {
            plantPr2.gameObject.SetActive(false);
            evText2.gameObject.SetActive(false);
        }

        if (currentPage == 3)
        {
            plantPr3.gameObject.SetActive(true);
            evText3.gameObject.SetActive(true);
        }
        else
        {
            plantPr3.gameObject.SetActive(false);
            evText3.gameObject.SetActive(false);
        }

        if (currentPage == 4)
        {
            plantPr4.gameObject.SetActive(true);
            evText4.gameObject.SetActive(true);
        }
        else
        {
            plantPr4.gameObject.SetActive(false);
            evText4.gameObject.SetActive(false);
        }

        if (currentPage == 5)
        {
            plantPr5.gameObject.SetActive(true);
            evText5.gameObject.SetActive(true);
        }
        else
        {
            plantPr5.gameObject.SetActive(false);
            evText5.gameObject.SetActive(false);
        }

        if (currentPage == 6)
        {
            plantPr6.gameObject.SetActive(true);
            evText6.gameObject.SetActive(true);
        }
        else
        {
            plantPr6.gameObject.SetActive(false);
            evText6.gameObject.SetActive(false);
        }

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

        if (currentPage == 1)
        {
            plantPr1.gameObject.SetActive(true);
            evText1.gameObject.SetActive(true);
        }
        else
        {
            plantPr1.gameObject.SetActive(false);
            evText1.gameObject.SetActive(false);
        }

        if (currentPage == 2)
        {
            plantPr2.gameObject.SetActive(true);
            evText2.gameObject.SetActive(true);
        }
        else
        {
            plantPr2.gameObject.SetActive(false);
            evText2.gameObject.SetActive(false);
        }

        if (currentPage == 3)
        {
            plantPr3.gameObject.SetActive(true);
            evText3.gameObject.SetActive(true);
        }
        else
        {
            plantPr3.gameObject.SetActive(false);
            evText3.gameObject.SetActive(false);
        }

        if (currentPage == 4)
        {
            plantPr4.gameObject.SetActive(true);
            evText4.gameObject.SetActive(true);
        }
        else
        {
            plantPr4.gameObject.SetActive(false);
            evText4.gameObject.SetActive(false);
        }

        if (currentPage == 5)
        {
            plantPr5.gameObject.SetActive(true);
            evText5.gameObject.SetActive(true);
        }
        else
        {
            plantPr5.gameObject.SetActive(false);
            evText5.gameObject.SetActive(false);
        }

        if (currentPage == 6)
        {
            plantPr6.gameObject.SetActive(true);
            evText6.gameObject.SetActive(true);
        }
        else
        {
            plantPr6.gameObject.SetActive(false);
            evText6.gameObject.SetActive(false);
        }
    }
}
