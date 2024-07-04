using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PRchk : MonoBehaviour
{
    public Seed seedScript; // Seed 스크립트의 참조를 받을 public 변수

    void Start()
    {
        // 현재 GameObject에 붙어 있는 Seed 스크립트의 참조를 가져옴
        seedScript = GetComponent<Seed>();
    }
}
