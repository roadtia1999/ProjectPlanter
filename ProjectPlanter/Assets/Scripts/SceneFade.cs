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
            DontDestroyOnLoad(gameObject); // ���� ����ǵ� ������Ʈ�� �ı����� ����
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        fadeImage = GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // �� ��ȯ�� ����ϴ� �ڵ�
    public void SceneChange(string nextScene)
    {
        // ��׶��忡�� �� ��ȯ ����
        ao = SceneManager.LoadSceneAsync(nextScene);
        ao.allowSceneActivation = false;

        // �� ��ȯ ���۽� �ٸ� ���� �Ұ���
        fadeImage.raycastTarget = true;

        // �� ��ȯ ����, ���̵� �ƿ�
        StartCoroutine(FadeOut());

        // �ٸ� ������Ʈ�� Ŭ�� �����ϰ� ����
        fadeImage.raycastTarget = false;  
    }

    // �� �ε��� �̹� �Ϸ�� ���¿��� �� ��ȯ�� ����ϴ� �ڵ�
    public void SceneChange(AsyncOperation aoLoaded)
    {
        // �ε��� ���� ���¸� �Ѱ���
        ao = aoLoaded;

        // �� ��ȯ ���۽� �ٸ� ���� �Ұ���
        fadeImage.raycastTarget = true;

        // �� ��ȯ ����, ���̵� �ƿ�
        StartCoroutine(FadeOut());

        // �ٸ� ������Ʈ�� Ŭ�� �����ϰ� ����
        fadeImage.raycastTarget = false;
    }

    // ���� ���� �ε��� ������ ���
    IEnumerator HoldForLoading()
    {
        while (ao.progress < 0.9f)
        {
            print(ao.progress);
            yield return null;
        }
        ao.allowSceneActivation = true;

        // ���� �� �ε� �Ϸ� �� ���̵� ��
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeOut() // ���̵� �ƿ� �ڵ�
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
        imageColor.a = 1f; // ���� ���ֱ� ���� while���� ������ �ٽ� �ѹ� ����
        fadeImage.color = imageColor;

        // ���� ���� �ε� �Ϸ�� ������ ���
        StartCoroutine(HoldForLoading());
    }

    IEnumerator FadeIn()
    {
        float fadeTimeElapse = 0f;
        Color imageColor = fadeImage.color;
        imageColor.a = 1f;

        // ������ �ð� ���� ���̵� ��
        while (fadeTimeElapse < fadeDuration)
        {
            fadeTimeElapse += Time.deltaTime;
            imageColor.a = Mathf.Clamp01(1f - (fadeTimeElapse / fadeDuration));
            fadeImage.color = imageColor;
            yield return null;
        }
        imageColor.a = 0f; // ���� ���ֱ� ���� while���� ������ �ٽ� �ѹ� ����
        fadeImage.color = imageColor;
    }

}