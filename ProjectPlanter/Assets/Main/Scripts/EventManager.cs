using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    [Header("# EventMove")]
    float moveDuration = 2f; // �̵� �ð�
    float stopDuration = 1f; // ���� �ð�
    float moveSpeed = 100f; // �̵� �ӵ�
    private Vector2 moveDirection; // �̵� ����
    private RectTransform evBee; // Bee RectTransform
    private RectTransform evMite; // Mite RectTransform
    
    public Canvas canvas;

    [Header("# Event")]
    public ItemData[] itemData; // ��ũ���ͺ� ������Ʈ �迭
    public int id;
    public int prefabId;

    private void Awake()
    {
        instance = this;

        TimeManager timeManager = TimeManager.instance;

        // �ð� ���� ��������
        TimeSpan timeDif = timeManager.timeDifference;

        Debug.Log(timeDif + " Ÿ�ӸŴ������� ������ ����");

        // �ð� ���̰� 3�� �̻��� �� ���� �̺�Ʈ �߻�
        if (timeDif.TotalSeconds >= 3)
        {
            int randomEvent = UnityEngine.Random.Range(0, itemData.Length);
            Debug.Log(randomEvent + " ���� �̺�Ʈ ��");
            TriggerRandomEvent(randomEvent);
            
        }
    }


    public void TriggerRandomEvent(int randomEvent)
    {
        
        name = "Event " + itemData[randomEvent].itemName;
        transform.parent = canvas.transform; // ĵ���� Ʈ�������� ����
        transform.localPosition = Vector3.zero;
        

        id = itemData[randomEvent].itemId;
        prefabId = itemData[randomEvent].itemId; // prefabId ����

        Debug.Log($"���� �̺�Ʈ �߻�: {itemData[randomEvent].itemName} - {itemData[randomEvent].itemDesc}");

        // ������ ��ȣ ã��
        for (int index = 0; index < PoolManager.instance.Event_prefab.Length; index++)
        {
            if (itemData[randomEvent].EventPrefab == PoolManager.instance.Event_prefab[index])
            {
                prefabId = index;
                break;
            }
        }

        // ���� �̺�Ʈ �߻�
        switch (randomEvent)
        {
            case 0: Bee(); break;
            case 1: Mite(); break;
            default:  break;
        }
        PlayerPrefs.SetInt("EventDexScene" + randomEvent, 1); // �̺�Ʈ �߻� ������ PlayerPrefs�� ����
        PlayerPrefs.SetInt("EventOccur" , randomEvent); // �̺�Ʈ �߻� �� ����
    }


    // Ǯ �Ŵ������� prefab��������
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

        // ĵ���� ��� ���ϱ�
        float halfWidth = evBee.rect.width / 2;
        float halfHeight = evBee.rect.height / 2;
        float minX = -canvasSize.x / 2 + halfWidth;
        float maxX = canvasSize.x / 2 - halfWidth;
        float minY = -canvasSize.y / 2 + halfHeight;
        float maxY = canvasSize.y / 2 - halfHeight;

        while (true)
        {
            // ���� ���� ����
            moveDirection = UnityEngine.Random.insideUnitCircle.normalized;

            // �������� �̵��� �� �̹��� ����
            if (moveDirection.x < 0)
            {
                evBee.localScale = new Vector3(-1f, 1f, 1f); // x �������� -1�� �����Ͽ� �̹����� ����
            }
            else
            {
                evBee.localScale = Vector3.one; // ������� ����
            }

            float elapsedTime = 0f;
            while (elapsedTime < moveDuration)
            {
                if (evBee != null)
                {
                    // �̵� �������� Bee ��ġ ����
                    evBee.anchoredPosition += moveDirection * moveSpeed * Time.deltaTime;

                    // ���� ��� ������ ������ �ʰԲ� ����
                    beePosition = evBee.anchoredPosition;
                    beePosition.x = Mathf.Clamp(beePosition.x, minX, maxX);
                    beePosition.y = Mathf.Clamp(beePosition.y, minY, maxY);
                    evBee.anchoredPosition = beePosition;
                }
                else
                {
                    Debug.LogError("evBee is null during movement.");
                    yield break; // �ڷ�ƾ ����
                }

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // ����
            yield return new WaitForSeconds(stopDuration);
        }
    }

    void Mite()
    {
        GameObject evMiteObject = PoolManager.instance.EventGet(prefabId);
        evMite = evMiteObject.GetComponent<RectTransform>();
        evMiteObject.transform.SetParent(transform, false);
        evMite.anchoredPosition = new Vector2(0, -300); // Ź�� �������� �̵�

        StartCoroutine(MoveMite());
    }

    IEnumerator MoveMite()
    {
        Vector2 canvasSize = new Vector2(1024, 768);
        Vector2 eventPosition;

        // ĵ���� ��� ���ϱ�
        float halfWidth = evMite.rect.width / 2;
        float minX = -canvasSize.x / 2 + halfWidth;
        float maxX = canvasSize.x / 2 - halfWidth;

        while(true)
        {
            moveDirection = new Vector2(UnityEngine.Random.Range(-1f, 1f), 0); // �¿�θ� �����̰Բ� �� (�ӵ� ����)

            // �������� �̵��� �� �̹��� ������ ����
            if (moveDirection.x < 0)
            {
                evMite.localScale = new Vector3(-1f, 1f, 1f); // x �������� -1�� �����Ͽ� �̹����� ����
            }
            else
            {
                evMite.localScale = Vector3.one; // ������� ����
            }

            float elapsedTime = 0f;
            while (elapsedTime < moveDuration)
            {
                if (evMite != null)
                {
                    // �̵� �������� Mite ��ġ ����
                    evMite.anchoredPosition += moveDirection * moveSpeed * Time.deltaTime;

                    // ����Ⱑ ��� ������ ������ �ʰԲ� ����
                    eventPosition = evMite.anchoredPosition;
                    eventPosition.x = Mathf.Clamp(eventPosition.x, minX, maxX);
                    evMite.anchoredPosition = eventPosition;
                }
                else
                {
                    Debug.LogError("mite uei");
                    yield break; // �ڷ�ƾ ����
                }

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // ����
            yield return new WaitForSeconds(stopDuration);
        }
    }
}

