using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    public int id;
    public int prefabId;

    // �� ����
    float moveDuration = 2f; // �̵� �ð�
    float stopDuration = 1f; // ���� �ð�
    float moveSpeed = 100f; // �̵� �ӵ�
    private RectTransform evBee; // Bee RectTransform
    private Vector2 moveDirection; // �̵� ����

    public Canvas canvas;

    public ItemData[] itemData; // ��ũ���ͺ� ������Ʈ �迭

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        TimeManager timeManager = TimeManager.instance;

        // �ð� ���� ��������
        TimeSpan timeDif = timeManager.timeDifference;

        Debug.Log(timeDif + " Ÿ�ӸŴ������� ������ ����");

        // �ð� ���̰� 3�� �̻��� �� ���� �̺�Ʈ �߻�
        if (timeDif.TotalSeconds >= 3)
        {
            int randomEvent = UnityEngine.Random.Range(0, itemData.Length);
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
            /*case 1: xxx(); break;*/
            default:  break;
        }
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

            // �������� �̵��� �� �̹��� ������ ������ŵ�ϴ�.
            if (moveDirection.x < 0)
            {
                evBee.localScale = new Vector3(-1f, 1f, 1f); // x �������� -1�� �����Ͽ� �̹����� ������ŵ�ϴ�.
            }
            else
            {
                evBee.localScale = Vector3.one; // ������� �����մϴ�.
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

    void xxx()
    {
        
        GameObject xxxObject = PoolManager.instance.EventGet(prefabId);
        RectTransform xxx = xxxObject.GetComponent<RectTransform>();
        xxxObject.transform.SetParent(transform, false);
        xxx.anchoredPosition = Vector2.zero;
        
    }
}
