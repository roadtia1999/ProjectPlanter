using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trigger : MonoBehaviour
{
    [SerializeField] private Seed SeedManager;

    [Header("Array")]
    GameObject[] seedObject = new GameObject[3];
    GameObject[] Pot = new GameObject[3];
    GameObject[] Sprout = new GameObject[3];
    GameObject[] Flower = new GameObject[3];
    GameObject[] PlantState = new GameObject[3];
    GameObject[] bubleObject = new GameObject[3];
    int[] plantType = new int[3];
    int[] x = new int[3];

    Image[] FlowerImg = new Image[3];
    Image[] StateImg = new Image[3];

    [Header("Sprite")]
    public Sprite[] Statespr = new Sprite[3];
    public Sprite[] Flowerspr = new Sprite[3];

    int plantindex;

    private void Awake()
    {

        for (int i = 0; i < 3; i++)
        {
            seedObject[i] = GameObject.Find("seed" + i);
            Pot[i] = GameObject.Find("Pot" + i);
            Sprout[i] = Pot[i].transform.Find("Sprout" + i).gameObject;
            Flower[i] = Pot[i].transform.Find("FlowerDemo" + i).gameObject;
            PlantState[i] = Pot[i].transform.Find("PlantState" + i).gameObject;
            bubleObject[i] = Pot[i].transform.Find("Button Buble" + i).gameObject;

            FlowerImg[i] = Flower[i].GetComponent<Image>();
            StateImg[i] = PlantState[i].GetComponent<Image>();
            x[i] = 0;
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
            plantType[plantindex] = SeedManager.PlantType[plantindex];
            if (PlayerPrefs.HasKey("PlantType" + plantindex))
            {
                Flower[plantindex].SetActive(true);
                plantType[plantindex] = SeedManager.PlantType[plantindex];
                FlowerImg[plantindex].sprite = Flowerspr[plantType[plantindex]];

            }


        }

        if (Input.GetKeyDown(KeyCode.W))
        {

            if (!PlantState[plantindex].activeSelf)
                PlantState[plantindex].SetActive(true);


            // 다음 이미지로 변경
            x[plantindex] = (x[plantindex] + 1) % Statespr.Length;
            StateImg[plantindex].sprite = Statespr[x[plantindex]];

        }

        if (Input.GetKeyDown(KeyCode.E))
            PlayerPrefs.DeleteAll();




    }












}
