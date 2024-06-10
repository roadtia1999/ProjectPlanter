using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExitButtonClicked()
    {
        Application.Quit();
    }

    public void PlantersButtonClicked()
    {
        SceneManager.LoadScene("_MainScene");
    }

    public void PlantdexButtonClicked()
    {
        SceneManager.LoadScene("DexScene");
    }
}
