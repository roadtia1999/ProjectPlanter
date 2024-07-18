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
        // �ν����� �信�� ���� �ǽð����� ������Ʈ�ǵ��� ����
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
