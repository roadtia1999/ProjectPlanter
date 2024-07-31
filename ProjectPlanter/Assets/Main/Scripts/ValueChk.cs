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
    [SerializeField] private int[] Growstack;
    [SerializeField] private int[] Deadstack;
    private void Start()
    {
        seconds = SeedManager.seconds;
        PlantType = SeedManager.PlantType;
        stack = SeedManager.stack;
        Growstack = SeedManager.GrowStack;
        Deadstack = SeedManager.DeadStack;

        Painstack = stateManager.painStack;




    }

    private void Update()
    {
        // �ν����� �信�� ���� �ǽð����� ������Ʈ�ǵ��� ����
        if (SeedManager != null)
        {
            seconds = SeedManager.seconds;
            PlantType = SeedManager.PlantType;
            stack = SeedManager.stack;
            Growstack = SeedManager.stack;
            Deadstack = SeedManager.stack;
        }
        if (stateManager != null)
            Painstack = stateManager.painStack;


    }
}
