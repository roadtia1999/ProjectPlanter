using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{

    public GameObject HarvestPrefab;



    public void HarvestAniEnd()
    {
        Destroy(gameObject);
    }

}
