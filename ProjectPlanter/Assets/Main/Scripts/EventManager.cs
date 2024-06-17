using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    public int id;
    public int prefabId;

    public Canvas canvas;

    public ItemData[] itemData; // 스크립터블 오브젝트 배열

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        TimeManager timeManager = TimeManager.instance;

        // 시간 차이 가져오기
        TimeSpan timeDif = timeManager.timeDifference;

        Debug.Log(timeDif + " 타임매니저에서 가져온 시차");

        // 시간 차이가 3초 이상일 때 랜덤 이벤트 발생
        if (timeDif.TotalSeconds >= 3)
        {
            int randomEvent = UnityEngine.Random.Range(0, itemData.Length);
            TriggerRandomEvent(randomEvent);
        }
    }

    public void TriggerRandomEvent(int randomEvent)
    {
        
        name = "Event " + itemData[randomEvent].itemName;
        transform.parent = canvas.transform; // 캔버스 트랜스폼을 설정
        transform.localPosition = Vector3.zero;
        

        id = itemData[randomEvent].itemId;
        prefabId = itemData[randomEvent].itemId; // prefabId 설정

        Debug.Log($"랜덤 이벤트 발생: {itemData[randomEvent].itemName} - {itemData[randomEvent].itemDesc}");

        // 프리팹 번호 찾기
        for (int index = 0; index < PoolManager.instance.Event_prefab.Length; index++)
        {
            if (itemData[randomEvent].EventPrefab == PoolManager.instance.Event_prefab[index])
            {
                prefabId = index;
                break;
            }
        }

        // 랜덤 이벤트 발생
        switch (randomEvent)
        {
            case 0: Bee(); break;
            case 1: xxx(); break;
            default:  break;
        }
    }


    // 풀 매니저에서 prefab가져오기
    void Bee()
    {
        GameObject evBeeObject = PoolManager.instance.EventGet(prefabId);
        RectTransform evBee = evBeeObject.GetComponent<RectTransform>();
        evBeeObject.transform.SetParent(transform, false);
        evBee.anchoredPosition = Vector2.zero;
        
    }

    void xxx()
    {
        
        GameObject xxxObject = PoolManager.instance.EventGet(prefabId);
        RectTransform xxx = xxxObject.GetComponent<RectTransform>();
        xxxObject.transform.SetParent(transform, false);
        xxx.anchoredPosition = Vector2.zero;
        
    }
}
