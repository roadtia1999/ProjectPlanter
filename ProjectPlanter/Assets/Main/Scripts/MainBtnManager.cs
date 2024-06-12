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

    [Header("# Can")]
    public GameObject handPrefab;
    private GameObject handInstance;
    private bool handClicked = false;

    [Header("# Can")]
    public GameObject seedPrefab;
    private GameObject seedInstance;

    public Canvas canvas;

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
            Debug.Log("if�� ����");
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
            Plantingseed(clickedButton);

            clickedButton.gameObject.SetActive(false);
            // ���� �ʱ�ȭ
            handClicked = false;
            Debug.Log("false�� �ʱ�ȭ");
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



    // ���� image Ȱ��ȭ
    public void Plantingseed(Button clickedBtn)
    {
        if (seedInstance == null)
        {
            seedInstance = Instantiate(seedPrefab, canvas.transform);
        }

        RectTransform btnRectTransform = clickedBtn.GetComponent<RectTransform>();

        // seed ������Ʈ�� RectTransform�� �ҷ�����.
        RectTransform seedRectTransform = seedInstance.GetComponent<RectTransform>();

        // ��ư�� ��ġ�� �������� seed ������Ʈ�� ��ġ�� �����մϴ�.
        Vector3 newPosition = seedRectTransform.anchoredPosition;
        newPosition.x = btnRectTransform.anchoredPosition.x + 7f;
        newPosition.y = btnRectTransform.anchoredPosition.y + -160f; 
        seedRectTransform.anchoredPosition = newPosition;

        
        seedInstance.SetActive(true);



    }


}
