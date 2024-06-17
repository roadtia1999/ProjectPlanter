using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
  
    void Update()
    {

        // PlayerPrefs¿¡¼­ °¡Á®¿À±â
        int isSeedPlanted = PlayerPrefs.GetInt("IsSeedPlanted", 0); // ±âº»°ªÀº 0

        if (isSeedPlanted == 1)
        {
            Debug.Log("¾¾¾Ñ ½É¾îÁü.");
        }
        else
        {
            Debug.Log("¾¾¾Ñ ¾ö½¿.");
        }
    }
}