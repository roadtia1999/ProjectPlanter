using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueChk : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private Seed SeedManager;
    [SerializeField] private PlantStateManager stateManager;

    [Header("Values")]
    [SerializeField] private int[] seconds;
    [SerializeField] private int[] PlantType;
    [SerializeField] private int[] stack;
    [SerializeField] private int[] Painstack;

    private void Start()
    {
        if (SeedManager != null)
        {
            seconds = SeedManager.seconds;
            PlantType = SeedManager.PlantType;
            stack = SeedManager.stack;
            Painstack = stateManager.painStack;
        }

    }

    private void Update()
    {
        // 인스펙터 뷰에서 값이 실시간으로 업데이트되도록 보장
        if (SeedManager != null)
        {
            seconds = SeedManager.seconds;
            PlantType = SeedManager.PlantType;
            stack = SeedManager.stack;

        }
        if (stateManager != null)
            Painstack = stateManager.painStack;
        
    }
}
