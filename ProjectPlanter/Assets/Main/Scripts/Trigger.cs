using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trigger : MonoBehaviour
{
    [SerializeField] private Seed SeedManager;

    [Header("# Array")]
    GameObject[] seedObject = new GameObject[3];
    GameObject[] Pot = new GameObject[3];
    GameObject[] Sprout = new GameObject[3];
    GameObject[] Flower = new GameObject[3];
    GameObject[] PlantState = new GameObject[3];
    GameObject[] bubleObject = new GameObject[3];
    int[] plantType = new int[3];
    int[] NextImag = new int[3];
    Image[] FlowerImg = new Image[3];
    Image[] StateImg = new Image[3];
    GameObject[] panel = new GameObject[4];


    [Header("# Sprite")]
    public Sprite[] Statespr = new Sprite[3];
    public Sprite[] Flowerspr = new Sprite[3];

    [Header("# ETC")]
    public GameObject[] text = new GameObject[4];
    public Canvas canvas2;
    public GameObject Panel;
    int currentIndex = 0;
    int plantindex;
    private void Awake()
    {

        for (int i = 0; i < 4; i++)
        {

            panel[i] = Panel.transform.Find("Panel" + i).gameObject;
        }

        for (int i = 0; i < 3; i++)
        {
            seedObject[i] = GameObject.Find("seed" + i);
            Pot[i] = GameObject.Find("Pot" + i);
            Sprout[i] = Pot[i].transform.Find("Sprout" + i).gameObject;
            Flower[i] = Pot[i].transform.Find("FlowerDemo" + i).gameObject;
            PlantState[i] = GameObject.Find("PlantState" + i);
            bubleObject[i] = GameObject.Find("Button Buble" + i);


            FlowerImg[i] = Flower[i].GetComponent<Image>();
            StateImg[i] = PlantState[i].GetComponent<Image>();
            NextImag[i] = 0;
        }

        if (PlayerPrefs.HasKey("First"))
            canvas2.gameObject.SetActive(false);

        else
        {
            panel[0].SetActive(true);
            text[0].SetActive(true);
        }
    }

    public void ClickOff()
    {


        if (currentIndex >= 3)
        {

            canvas2.gameObject.SetActive(false);
        }

        else if (currentIndex < panel.Length - 1)
        {
            text[currentIndex].SetActive(false);
            panel[currentIndex].SetActive(false);
            currentIndex++;
            panel[currentIndex].SetActive(true);
            text[currentIndex].SetActive(true);
        }

    }
    public void PlantNum(int num)
    {
        plantindex = num;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (PlayerPrefs.HasKey("PlantType" + plantindex))
            {
                Flower[plantindex].SetActive(true);
                if (!FlowerImg[plantindex].enabled)
                    FlowerImg[plantindex].enabled = true;


                plantType[plantindex] = SeedManager.PlantType[plantindex];
                FlowerImg[plantindex].sprite = Flowerspr[plantType[plantindex]];

            }


        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Image StateImage = PlantState[plantindex].GetComponent<Image>();

            if (!PlantState[plantindex].activeSelf)
            {
                PlantState[plantindex].SetActive(true);

            }
            StateImage.enabled = true;


            // 다음 이미지로 변경
            NextImag[plantindex] = (NextImag[plantindex] + 1) % Statespr.Length;
            StateImg[plantindex].sprite = Statespr[NextImag[plantindex]];

        }

        if (Input.GetKeyDown(KeyCode.E))
            PlayerPrefs.DeleteAll();




    }












}
