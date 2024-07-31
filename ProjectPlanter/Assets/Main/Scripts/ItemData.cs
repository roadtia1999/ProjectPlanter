using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "item", menuName = "Scriptble object/ItemData")]
public class ItemData : ScriptableObject
{
    public enum ItemType { Plant, Event }

    [Header("# Main Info")]
    public ItemType itemType;
    public int itemId;
    public string itemName;
    public string itemDesc;
    public Sprite itemIcon;

    [Header("# Event Data")]
    /*�̺�Ʈ ������ , �ð���*/
    public GameObject EventPrefab;

    [Header("# Plant Data")]
    // �Ĺ� ������, ������ , ����ð�
    public float GrowthTime;
    public Sprite[] FlowerSp = new Sprite[8];




}
