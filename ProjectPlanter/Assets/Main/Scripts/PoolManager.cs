using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    //Event
    [Header("# Event")]
    public GameObject[] Event_prefab;
    List<GameObject>[] Event_pool;

    public static PoolManager instance;


    void Awake()
    {
        instance = this; // Singleton 인스턴스 설정

        //프리팹스 초기화
        /*PlayerPrefs.DeleteAll();*/

        Event_pool = new List<GameObject>[Event_prefab.Length];

        for (int index = 0; index < Event_pool.Length; index++)
        {
            Event_pool[index] = new List<GameObject>();
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


   
}
