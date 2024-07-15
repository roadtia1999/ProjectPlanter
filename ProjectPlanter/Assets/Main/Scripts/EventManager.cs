using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    [Header("# EventMove")]
    float moveDuration = 2f; // 이동 시간
    float stopDuration = 1f; // 멈춤 시간
    float moveSpeed = 100f; // 이동 속도
    private Vector2 moveDirection; // 이동 방향
    private RectTransform evBee; // Bee RectTransform
    private RectTransform evMite; // Mite RectTransform
    
    public Canvas canvas;

    [Header("# Event")]
    public ItemData[] itemData; // 스크립터블 오브젝트 배열
    public int id;
    public int prefabId;

    private void Awake()
    {
        instance = this;

        TimeManager timeManager = TimeManager.instance;

        // 시간 차이 가져오기
        TimeSpan timeDif = timeManager.timeDifference;

        Debug.Log(timeDif + " 타임매니저에서 가져온 시차");

        // 시간 차이가 3초 이상일 때 랜덤 이벤트 발생
        if (timeDif.TotalSeconds >= 3)
        {
            int randomEvent = UnityEngine.Random.Range(0, itemData.Length);
            Debug.Log(randomEvent + " 랜덤 이벤트 값");
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
            case 1: Mite(); break;
            default:  break;
        }
        PlayerPrefs.SetInt("EventDexScene" + randomEvent, 1); // 이벤트 발생 정보를 PlayerPrefs에 저장
        PlayerPrefs.SetInt("EventOccur" , randomEvent); // 이벤트 발생 값 저장
    }


    // 풀 매니저에서 prefab가져오기
    void Bee()
    {
        GameObject evBeeObject = PoolManager.instance.EventGet(prefabId);
        evBee = evBeeObject.GetComponent<RectTransform>();
        evBeeObject.transform.SetParent(transform, false);
        evBee.anchoredPosition = Vector2.zero;

        StartCoroutine(MoveBee());

    }

    IEnumerator MoveBee()
    {
        Vector2 canvasSize = new Vector2(1024, 768); 
        Vector2 beePosition;

        // 캔버스 경계 구하기
        float halfWidth = evBee.rect.width / 2;
        float halfHeight = evBee.rect.height / 2;
        float minX = -canvasSize.x / 2 + halfWidth;
        float maxX = canvasSize.x / 2 - halfWidth;
        float minY = -canvasSize.y / 2 + halfHeight;
        float maxY = canvasSize.y / 2 - halfHeight;

        while (true)
        {
            // 랜덤 방향 설정
            moveDirection = UnityEngine.Random.insideUnitCircle.normalized;

            // 왼쪽으로 이동할 때 이미지 반전
            if (moveDirection.x < 0)
            {
                evBee.localScale = new Vector3(-1f, 1f, 1f); // x 스케일을 -1로 설정하여 이미지를 반전
            }
            else
            {
                evBee.localScale = Vector3.one; // 원래대로 복원
            }

            float elapsedTime = 0f;
            while (elapsedTime < moveDuration)
            {
                if (evBee != null)
                {
                    // 이동 방향으로 Bee 위치 변경
                    evBee.anchoredPosition += moveDirection * moveSpeed * Time.deltaTime;

                    // 벌이 경계 밖으로 나가지 않게끔 조절
                    beePosition = evBee.anchoredPosition;
                    beePosition.x = Mathf.Clamp(beePosition.x, minX, maxX);
                    beePosition.y = Mathf.Clamp(beePosition.y, minY, maxY);
                    evBee.anchoredPosition = beePosition;
                }
                else
                {
                    Debug.LogError("evBee is null during movement.");
                    yield break; // 코루틴 종료
                }

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // 멈춤
            yield return new WaitForSeconds(stopDuration);
        }
    }

    void Mite()
    {
        GameObject evMiteObject = PoolManager.instance.EventGet(prefabId);
        evMite = evMiteObject.GetComponent<RectTransform>();
        evMiteObject.transform.SetParent(transform, false);
        evMite.anchoredPosition = new Vector2(0, -300); // 탁자 위에서만 이동

        StartCoroutine(MoveMite());
    }

    IEnumerator MoveMite()
    {
        Vector2 canvasSize = new Vector2(1024, 768);
        Vector2 eventPosition;

        // 캔버스 경계 구하기
        float halfWidth = evMite.rect.width / 2;
        float minX = -canvasSize.x / 2 + halfWidth;
        float maxX = canvasSize.x / 2 - halfWidth;

        while(true)
        {
            moveDirection = new Vector2(UnityEngine.Random.Range(-1f, 1f), 0); // 좌우로만 움직이게끔 함 (속도 랜덤)

            // 왼쪽으로 이동할 때 이미지 방향을 반전
            if (moveDirection.x < 0)
            {
                evMite.localScale = new Vector3(-1f, 1f, 1f); // x 스케일을 -1로 설정하여 이미지를 반전
            }
            else
            {
                evMite.localScale = Vector3.one; // 원래대로 복원
            }

            float elapsedTime = 0f;
            while (elapsedTime < moveDuration)
            {
                if (evMite != null)
                {
                    // 이동 방향으로 Mite 위치 변경
                    evMite.anchoredPosition += moveDirection * moveSpeed * Time.deltaTime;

                    // 진드기가 경계 밖으로 나가지 않게끔 조절
                    eventPosition = evMite.anchoredPosition;
                    eventPosition.x = Mathf.Clamp(eventPosition.x, minX, maxX);
                    evMite.anchoredPosition = eventPosition;
                }
                else
                {
                    Debug.LogError("mite uei");
                    yield break; // 코루틴 종료
                }

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // 멈춤
            yield return new WaitForSeconds(stopDuration);
        }
    }
}

