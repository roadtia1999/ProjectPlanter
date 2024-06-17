using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainBtnManager : MonoBehaviour
{
    [Header("# Can")]
    public GameObject canPrefab;
    private GameObject canInstance;
    public Button firstButtonClicked;
    private bool CanClicked = false;

    [Header("# Hand")]
    public GameObject handPrefab;
    private GameObject handInstance;
    private bool handClicked = false;

    [Header("# Seed")]
    public GameObject seedPrefab;
    private GameObject seedInstance;
    public bool seedPlanted;
/*    public GameObject x;*/
    public Sprite Seed; // ������ ���� �̹��� ��������Ʈ
    // Ŭ���� ������ �ε���
    public int bubleIndex;

    public Canvas canvas;

    void Start()
    {
        // 0 1 2 �� ��ư üũ.
        for (int i = 0; i < 3; i++)
        {
            seedPlanted = PlayerPrefs.HasKey("Button Buble" + i + "Clicked" + i);

            // �ɾ����ִ��� üũ
            // �ɾ����ִٸ�
            if (seedPlanted)
            {
                // 0�� �޾ƿ���.
                int value = PlayerPrefs.GetInt("Button Buble" + i + "Clicked" + i);

                if (value == 0)
                {
                    GameObject bubleObject = GameObject.Find("Button Buble" + i);
                    if (bubleObject !=null)
                    {
                        bubleObject.SetActive(false);
                    }
                }

            }

        }

    }


    // ��ư Ŭ�� ��
    public void CanBtnClicked()
    {
        CanClicked = true;
        Debug.Log("Ŭ����");

        if (canInstance == null)
        {
            canInstance = Instantiate(canPrefab, canvas.transform);
        }
    }

    // Can ������Ʈ�� �ش� ��ư ���� ��ġ
    public void PlaceOnButton(Button clickedButton)
    {
        if (CanClicked)
        {
            Debug.Log("if�� ����");
            // Ŭ���� ��ư�� RectTransform�� �ҷ�����
            RectTransform btnRectTransform = clickedButton.GetComponent<RectTransform>();

            // Can ������Ʈ�� RectTransform�� �ҷ�����.
            RectTransform canRectTransform = canInstance.GetComponent<RectTransform>();

            // ��ư�� ��ġ�� �������� Can ������Ʈ�� ��ġ�� �����մϴ�.
            Vector3 newPosition = canRectTransform.anchoredPosition;
            newPosition.x = btnRectTransform.anchoredPosition.x + 200f;
            newPosition.y = btnRectTransform.anchoredPosition.y + 400f; // y ���� ��ư�� y ���� 400��ŭ �̵��մϴ�.
            canRectTransform.anchoredPosition = newPosition;

            // Can ������Ʈ�� Ȱ��ȭ�մϴ�.
            canInstance.SetActive(true);

            // 3�� �Ŀ� Can ������Ʈ�� �ı��մϴ�.
            StartCoroutine(DestroyCanAfterDelay(3f));

            // ���� �ʱ�ȭ
            CanClicked = false;
            Debug.Log("false�� �ʱ�ȭ");
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
        SceneManager.LoadScene("MenuScene");
    }

    // ��ǳ�� Ŭ���� --
    public void BubleIndex(int btnIndex)
    {
        //ȭ�� potx ���ϱ�.
        bubleIndex = btnIndex;
        

    }

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

            // Can ������Ʈ�� RectTransform�� �ҷ�����.
            RectTransform HandRectTransform = handInstance.GetComponent<RectTransform>();

            // ��ư�� ��ġ�� �������� Can ������Ʈ�� ��ġ�� �����մϴ�.
            Vector3 newPosition = HandRectTransform.anchoredPosition;
            newPosition.x = btnRectTransform.anchoredPosition.x + 10f;
            newPosition.y = btnRectTransform.anchoredPosition.y + -40f; 
            HandRectTransform.anchoredPosition = newPosition;

            // hand ������Ʈ�� Ȱ��ȭ�մϴ�.
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
    public void Plantingseed()
    {
        GameObject seedObject = GameObject.Find("seed" + bubleIndex);
        
        if (seedObject != null)
        {
            seedObject.GetComponent<Image>().sprite = Seed;
            
        }
        else
        {
            Debug.Log("nullllll");
        }

    }
    
    public void PlantedSeed()
    {
   
     /*   //���� �ɾ��� === �� 
        seedPlanted = !seedPlanted;
        // PlayerPrefs�� ����
        PlayerPrefs.SetInt("IsSeedPlanted", seedPlanted ? 1 : 0); //true �̸� 1 false 0*/


    }




}
