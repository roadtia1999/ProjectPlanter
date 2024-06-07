using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainBtnManager : MonoBehaviour
{
    // ��ġ ��ų ������Ʈ
    public GameObject canPrefab;

    // can ������
    private GameObject canInstance;

    // Ŭ�� �� Ui
    public Button firstButtonClicked;

    // Ŭ�� ���� Ȯ��
    private bool CanClicked = false;

   // Can�� canvas�� �ֱ����� ����
    public Canvas canvas;

    void Start()
    {
       
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
   
    public void PlaceCanOnButton(Button clickedButton)
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

    public void MenuBtnClick()
    {
        /*// ���� Scene�� �̸��� �����ּ���
        string nextSceneName = "MenuScene";

        // �ش� Scene���� �̵��մϴ�.
        SceneManager.LoadScene(nextSceneName);*/


        SceneManager.LoadScene("MenuScene");
    }

}