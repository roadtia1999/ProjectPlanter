using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BtnManager : MonoBehaviour
{
    public List<ItemData> database; // Dex ���� �����Ͱ� ���ִ� �����ͺ��̽�
    public Image dexSprite; // Dex ��������Ʈ �κ�
    public Text dexTitleText; // Dex �̸� �κ�
    public Text dexDescText; // Dex ���� �κ�

    private int currentPage = 0; // ���� ������ �ε����� ����

    // ��ư��
    public Button btnNext;
    public Button btnPrev;

    private void Start()
    {
        if (database.Count == 1) // �����ͺ��̽� �����Ͱ� �ϳ����� �� ����ó��
        {
            btnNext.gameObject.SetActive(false);
        }
        btnPrev.gameObject.SetActive(false);
        SetNewData(); // ������ �ʱ�ȭ
    }
    public void ExitDex()
    {
        // �޴� ������ ������
        SceneFade sf = GameObject.Find("FadeCanvas").GetComponent<SceneFade>();
        sf.SceneChange("MenuScene");
    }

    public void NextPage()
    {       
        currentPage++; // �ε��� �ѱ��

        if (currentPage == database.Count - 1) // ������ ��������� ���� ��ư ��Ȱ��ȭ
        {
            btnNext.gameObject.SetActive(false);
        }
        else
        {
            btnNext.gameObject.SetActive(true);
        }
        btnPrev.gameObject.SetActive(true); // �������� �Ѿ���� ���� ��ư Ȱ��ȭ

        SetNewData(); // ������ ����
    }
    public void PrevPage()
    {
        currentPage--; // �ε��� �ѱ��

        if (currentPage == 0) // ù ��������� ���� ��ư ��Ȱ��ȭ
        {
            btnPrev.gameObject.SetActive(false);
        }
        else
        {
            btnPrev.gameObject.SetActive(true);
        }
        btnNext.gameObject.SetActive(true); // �������� �ڷ� �Ѿ���� ���� ��ư Ȱ��ȭ

        SetNewData(); // ������ ����
    }

    private void SetNewData() // ���� ������ ���̴� �����͸� �����ϴ� �ڵ�
    {
        dexSprite.sprite = database[currentPage].itemIcon; // ��������Ʈ �κ� ����

        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + currentPage)) // ���� ��Ұ� �߰ߵ� ������ ��
        {
            dexSprite.color = Color.white; // ��������Ʈ ������ ���̰� ����
            dexTitleText.text = database[currentPage].itemName; // �̸� �κ� ����
            dexDescText.text = database[currentPage].itemDesc; // ���� �κ� ����
        }
        else
        {
            dexSprite.color = Color.black; // �˰� ĥ�� ��������Ʈ�� ���̰� ����
            dexTitleText.text = "???";
            dexDescText.text = "���� �߰ߵ��� ���� ����Դϴ�.";
        }
    }
}
