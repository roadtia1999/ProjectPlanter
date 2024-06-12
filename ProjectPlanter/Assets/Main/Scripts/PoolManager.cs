using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    //Event
    [Header("# Event")]
    public GameObject[] Event_prefab;
    List<GameObject>[] Event_pool;

    [Header("# Plant")]
    public GameObject[] Plant_prefab;
    List<GameObject>[] Plant_pool;

    public static PoolManager instance;


    void Awake()
    {
        instance = this; // Singleton 인스턴스 설정

        Event_pool = new List<GameObject>[Event_prefab.Length];

        for (int index = 0; index < Event_pool.Length; index++)
        {
            Event_pool[index] = new List<GameObject>();
        }



        Plant_pool = new List<GameObject>[Plant_prefab.Length];

        for (int index = 0; index < Plant_pool.Length; index++)
        {
            Plant_pool[index] = new List<GameObject>();
        }


    }

    public GameObject EventGet(int index)
    {
        GameObject select = null;

        foreach (GameObject item in Event_pool[index])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        if (!select)
        {
            select = Instantiate(Event_prefab[index], transform);
            Event_pool[index].Add(select);
        }

        return select;
    }


    public GameObject PlantGet(int index)
    {
        GameObject select = null;

        foreach (GameObject item in Plant_pool[index])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        if (!select)
        {
            select = Instantiate(Plant_prefab[index], transform);
            Plant_pool[index].Add(select);
        }

        return select;
    }

}
