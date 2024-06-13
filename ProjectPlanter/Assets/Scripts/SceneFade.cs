using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


// ���� �� ������ ���̵� ��/�ƿ��� ������ �ڵ��Դϴ�


public class SceneFade : MonoBehaviour
{
    private static SceneFade instance = null;
    private Image fadeImage;
    public float fadeDuration;
    AsyncOperation ao;

    private void Awake()
    {

        // �� ��ȯ�� ���ӿ�����Ʈ�� ��� ������ ��������

        if (instance != null) // ���� ������ ���ƿͼ�, SceneFade������Ʈ�� �ߺ��Ǿ� ������ ��츦 ����
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
    void Start()
    {
        // ���̵� ��/�ƿ��� ���� �̹����� �����´�
        fadeImage = GetComponentInChildren<Image>();

        // ���� ��ȯ�Ǵ°��� �����ϰ�, ���̵� �ƿ� �ڵ带 �����ϱ� ���� �غ�
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode mode) // �� �ε尡 ������ ���
    {
        // ���̵� �ƿ� �Ϸ� ���� �� �ε� ����
        ao = SceneManager.LoadSceneAsync(scene.buildIndex);
        ao.allowSceneActivation = false;

        // ���̵� ��/�ƿ� �ڵ� ����
        StartCoroutine(SceneChangeSequence());
    }

    // ���̵� ��/�ƿ� �ڵ�
    IEnumerator SceneChangeSequence()
    {
        float fadeTimeElapse = 0f;
        Color imageColor = fadeImage.color;
        imageColor.a = 0f;

        // ������ �ð� ���� ���̵� �ƿ�
        while (fadeTimeElapse < fadeDuration)
        {
            fadeTimeElapse += Time.deltaTime;
            imageColor.a = Mathf.Clamp01(fadeTimeElapse / fadeDuration);
            fadeImage.color = imageColor;
            yield return null;
        }

        // ���� �� �ε� �Ϸ���� ���
        while(!ao.isDone)
        {
            yield return null;
        }
        ao.allowSceneActivation = true;

        yield return null;
    }
}
