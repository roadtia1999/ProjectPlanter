using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;


// ��� ����, ��ư Ŭ�� ���� ���� �����ϴ� �ڵ��Դϴ�


public class SoundMaker : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public AudioSource buttonClick;
    public AudioSource backgroundClick;

    private static SoundMaker instance = null;
    public static float volume;

    private void Awake()
    {

        // ���� ���ӿ�����Ʈ�� ��� ������ ��������

        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // ���� ����ǵ� ���� ������Ʈ�� �ı����� ����
        }
    }

    // Start is called before the first frame update
    void Start() // ���� �� ����� ���� ����
    {
        volume = PlayerPrefs.GetFloat("volume");
        SetVolume();

        // BGM ��ŸƮ
        backgroundMusic.loop = true;
        backgroundMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //  ������ ���� �� �ִ� �޴� ������ ���������� ���� ����
        if (SceneManager.GetActiveScene().name == "MenuScene")
        {
            volume = PlayerPrefs.GetFloat("volume");
            SetVolume();
        }

        // ��ư Ŭ�� ���� �ڵ�
        if (Input.GetMouseButtonDown(0))
        {
            // UI�� Ŭ���� ����� ���� üũ
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            // ���� UI�� Ŭ���� ���� �´ٸ�
            if (results != null)
            {
                // ��ư���� Ȯ�� �� ��ư Ŭ�� ���� ���
                foreach (RaycastResult result in results)
                {
                    Button button = result.gameObject.GetComponent<Button>();
                    if (button != null)
                    {
                        buttonClick.Play();
                        return;
                    }
                }

                // ��ư�� �ƴϸ� �Ϲ� Ŭ�� ����
                backgroundClick.Play();
            }
            // UI�� �ƴϸ� �Ϲ� Ŭ�� ����
            else
            {
                backgroundClick.Play();    
            }
        }
    }

    // ��Ȱ��ȭ �� ���Ұ� �ڵ�
    private void OnApplicationFocus(bool focus)
    {
        if (PlayerPrefs.GetInt("disableMute") == 1)
        {
            float volumeF;

            if (!focus)
            {
                volumeF = 0;
            }
            else
            {
                volumeF = PlayerPrefs.GetFloat("volume");
            }
            SetVolume(volumeF);
        }
    }

    // ������ �ѹ��� �������ֱ� ���� �ڵ�
    private void SetVolume()
    {
        backgroundMusic.volume = volume;
        buttonClick.volume = volume;
        backgroundClick.volume = volume;
    }

    // �Է°��� �ִ� ���� ����
    private void SetVolume(float volumeF)
    {
        backgroundMusic.volume = volumeF;
        buttonClick.volume = volumeF;
        backgroundClick.volume = volumeF;
    }
}
