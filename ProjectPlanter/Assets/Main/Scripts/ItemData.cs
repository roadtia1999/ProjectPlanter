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
    /*이벤트 프리팹 , 시간대*/

    public GameObject EventPrefab;
    public float TimeZone;

    [Header("# Plant Data")]
    
    // 식물 프리팹, 아이콘 , 성장시간.
    public GameObject PlantPrefab;
    public float GrowthTime;
    public Sprite Y_Freesia;
    public Sprite F_Freesia;


}
