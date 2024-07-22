using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainBtnManager : MonoBehaviour
{
    public static MainBtnManager instance;

    [Header("# Can")]
    public GameObject canPrefab;
    private GameObject canInstance;
    private bool CanClicked = false;
    int[] stack = new int[3]; // ���Ѹ��� ���� üũ.

    [Header("# Hand")]
    public GameObject handPrefab;
    private GameObject handInstance;
    private bool handClicked = false;

    [Header("# Seed")]
    public GameObject seedPrefab;
    public Sprite SeedSpr; // ������ ���� �̹��� ��������Ʈ
    public bool seedPlanted; // ���� �ɾ����ִ��� ����

    [Header("etc")]
    public int bubleIndex; // Ŭ���� ������ �ε���
    int potIndex; // Ŭ���� ȭ���� �ε��� 

    [Header("Canvas")]
    public Canvas canvas;
    GameObject[] PlantState = new GameObject[3];
    GameObject[] Pot = new GameObject[3];
    public Sprite[] StateSpr;




    private void Awake()
    {
        
        instance = this;
        PlantState = new GameObject[3];
        // 0 1 2 �� ��ư üũ.
        for (int i = 0; i < 3; i++)
        {
            seedPlanted = PlayerPrefs.HasKey("Button Buble" + i + "Clicked" + i);
            Pot[i] = GameObject.Find("Pot" + i);
            GameObject seedObject = GameObject.Find("seed" + i);
            PlantState[i] = Pot[i].transform.Find("PlantState" + i).gameObject;

            if (seedPlanted)
            {
                // 0�� �޾ƿ���.
                int value = PlayerPrefs.GetInt("Button Buble" + i + "Clicked" + i);

                if (value == 0)
                {
                    GameObject bubleObject = GameObject.Find("Button Buble" + i);
                    if (bubleObject != null)
                    {
                        seedObject.GetComponent<Image>().sprite = SeedSpr;

                        PlantState[i].SetActive(true);
                        bubleObject.SetActive(false);
                    }
                }

            }

        }

    }
    private void Start()
    {
        stack = new int[3];

    }

    public void BubleIndex(int btnIndex)
    {
        //����x ���ϱ�.
        bubleIndex = btnIndex;

    }
    public void PotIndex(int btnIndex)
    {
        //potx ���ϱ�.
        potIndex = btnIndex;

    }




    // ��ư Ŭ�� ��
    public void CanBtnClicked()
    {
        CanClicked = !CanClicked;

        if (CanClicked)
        {
            if (canInstance == null)
                canInstance = Instantiate(canPrefab, canvas.transform);
        }

        else
        {
            Destroy(canInstance); 
            canInstance = null;   
        }
    }

    // ���� �߰� �κ�
    void CanStack()
    {
        int[] x = new int[3];
        stack[potIndex]++;
        x[potIndex] = stack[potIndex];   
        PlayerPrefs.SetInt("Stack"+potIndex , x[potIndex]);

        string currentTime = DateTime.Now.ToString();
        PlayerPrefs.SetString("StackTime" + potIndex, currentTime);

    }

    // Can ������Ʈ�� �ش� ��ư ���� ��ġ
    public void PlaceOnButton(Button clickedButton)
    {
        
        CanStack();
        if (CanClicked)
        {
            // Ŭ���� ��ư�� RectTransform�� �ҷ�����
            RectTransform btnRectTransform = clickedButton.GetComponent<RectTransform>();

            // Can ������Ʈ�� RectTransform�� �ҷ�����.
            RectTransform canRectTransform = canInstance.GetComponent<RectTransform>();

            // ��ư�� ��ġ�� �������� hand ������Ʈ�� ��ġ�� ����.
            Vector3 newPosition = btnRectTransform.position;
            newPosition.y += 200f; // ��ư�� ���̸�ŭ �Ʒ��� �̵�
            newPosition.x += 100f; // ��ư�� ���̸�ŭ �Ʒ��� �̵�
            canRectTransform.position = newPosition;

            // ���� ���Ҷ��� �ߵ�
            StateThirsty(clickedButton);

            // Can ������Ʈ�� Ȱ��ȭ�մϴ�.
            canInstance.SetActive(true);

            // 3�� �Ŀ� Can ������Ʈ�� �ı��մϴ�.
            StartCoroutine(DestroyCanAfterDelay(3f));

            // ���� �ʱ�ȭ
            CanClicked = false;
        }
    }

    // ���� �ð� �Ŀ� Can ������Ʈ�� �ı��ϴ� �Լ�
    private IEnumerator DestroyCanAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(canInstance);
    }

    // �޴� �� �̵�
    public void MenuBtnClick()
    {
        SceneFade sf = GameObject.Find("FadeCanvas").GetComponent<SceneFade>();
        sf.SceneChange("MenuScene");
    }

    // ��ǳ�� Ŭ���� -- index�� ����


    public void BubleCilck(Button clickedButton)
    {
        handClicked = true;

        if (handInstance == null)
        {
            handInstance = Instantiate(handPrefab, canvas.transform);
            handInstance.AddComponent<CanvasGroup>();
        }

        if (handClicked)
        {
            
            // Ŭ���� ��ư�� RectTransform�� �ҷ�����
            RectTransform btnRectTransform = clickedButton.GetComponent<RectTransform>();

            // Hnad ������Ʈ�� RectTransform�� �ҷ�����.
            RectTransform HandRectTransform = handInstance.GetComponent<RectTransform>();

            // ��ư�� ��ġ�� �������� hand ������Ʈ�� ��ġ�� ����.
            Vector3 newPosition = btnRectTransform.position;
            newPosition.y -= 40f; // ��ư�� ���̸�ŭ �Ʒ��� �̵�
            HandRectTransform.position = newPosition;


            // hand ������Ʈ�� Ȱ��ȭ.
            handInstance.SetActive(true);

            // 1.5�� �Ŀ� hand ������Ʈ a �� ������ �ٿ�
            StartCoroutine(FadeOutHand(1.5f));
            Plantingseed();

            clickedButton.gameObject.SetActive(false);
            PlayerPrefs.SetInt(clickedButton.name + "Clicked" +bubleIndex, 0); // ��ư ���� ���� false
            // ���� �ʱ�ȭ
            handClicked = false;
            
        }



    }

    private IEnumerator FadeOutHand(float duration)
    {
        CanvasGroup canvasGroup = handInstance.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1.0f;
        float startAlpha = canvasGroup.alpha;
        float rate = 1.0f / duration;
        float progress = 0.0f;

        while (progress < 1.0f)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0, progress);
            progress += rate * Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0;
        handInstance.SetActive(false);
    }

    // ���� image Ȱ��ȭ1
    //
    public void Plantingseed()
    {
        GameObject seedObject = GameObject.Find("seed" + bubleIndex);

        if (seedObject != null)
        {
            seedObject.GetComponent<Image>().sprite = SeedSpr;
            
        }

    }

    //�񸶸��� ���ʿ� 0�̿��⿡ �߻��ǰ�
    //�� �޼��带 �����Ű���� �񸶸� ���¿��� ���� �ִ°ű⿡
    //������ �ö󰡰� ���ִ�. �׷��� stack�� �ʱ�ȭ �ϴ°ͺ��� �̰� ������ �ʹ�.
    public void StateThirsty(Button clickedButton)
    {
        Image[] childImages = clickedButton.GetComponentsInChildren<Image>();

        foreach (Image childImage in childImages)
        {
            if (childImage.sprite == StateSpr[3])
            {
                childImage.sprite = StateSpr[1];
   
                break;
            }
        }
    }


 

}
