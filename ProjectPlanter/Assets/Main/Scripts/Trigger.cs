using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    GameObject[] seedObject = new GameObject[3];
    GameObject[] Pot = new GameObject[3];
    GameObject[] Sprout = new GameObject[3];
    GameObject[] Plant = new GameObject[3];
    GameObject[] PlantState = new GameObject[3];
    GameObject[] bubleObject = new GameObject[3];
    /*public Image[] PlantImage = new Image[3]; //PlantImage 타입별로 성장값 조정*/

    private void Awake()
    {
        for (int i = 0; i < 3; i++)
        {
            seedObject[i] = GameObject.Find("seed" + i);
            Pot[i] = GameObject.Find("Pot" + i);
            Sprout[i] = Pot[i].transform.Find("Sprout" + i).gameObject;
            Plant[i] = Pot[i].transform.Find("FlowerDemo" + i).gameObject;
            PlantState[i] = Pot[i].transform.Find("PlantState" + i).gameObject;
            bubleObject[i] = Pot[i].transform.Find("Button Buble" + i).gameObject;
        }
    }




}
