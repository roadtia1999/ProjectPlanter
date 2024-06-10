using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BtnManager : MonoBehaviour
{
    public GameObject pr1;
    public GameObject pr2;
    public GameObject evText1;
    public GameObject evText2;
    public GameObject GameObject;

    private void Start()
    {
        GameObject = GetComponent<GameObject>();    
    }
    public void ExitDex()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void FirstPage()
    {
        pr1.gameObject.SetActive(true);
        pr2.gameObject.SetActive(false);
        evText1.gameObject.SetActive(true);
        evText2.gameObject.SetActive(false);
    }

    public void SecondPage()
    {
        pr1.gameObject.SetActive(false);
        pr2.gameObject.SetActive(true);
        evText1.gameObject.SetActive(false);
        evText2.gameObject.SetActive(true);
    }
}
