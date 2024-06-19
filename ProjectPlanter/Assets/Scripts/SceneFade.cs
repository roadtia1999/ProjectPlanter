using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


// ���� �� ������ ���̵� ��/�ƿ��� ������ �ڵ��Դϴ�


public class SceneFade : MonoBehaviour
{
    private static SceneFade instance = null;
    public Image fadeImage;
    public float fadeDuration;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // �ܺο��� �� ��ȯ ȣ���ϴ� ������Ʈ ���� �ڵ�
    public static void SceneChange(string nextScene)
    {
        SceneFade sf = new SceneFade();
        sf.Change(nextScene);
    }

    // �� ��ȯ�� ����ϴ� �ڵ�
    public void Change(string nextScene)
    {
        // ��׶��忡�� �� ��ȯ ����
        AsyncOperation ao = SceneManager.LoadSceneAsync(nextScene);
        ao.allowSceneActivation = false;

        // �� ��ȯ ���۽� �ٸ� ���� �Ұ���
        fadeImage.raycastTarget = true;

        // ���̵� �ƿ�
        StartCoroutine(FadeOut());

        // ���� ���� �ε� �Ϸ�� ������ ���
        StartCoroutine(HoldForLoading(ao));

        // ���� �� �ε� �Ϸ� �� ���̵� ��
        StartCoroutine(FadeIn());

        // �ٸ� ������Ʈ�� Ŭ�� �����ϰ� ����
        fadeImage.raycastTarget = false;  
    }

    IEnumerator HoldForLoading(AsyncOperation ao)
    {
        while (!ao.isDone)
        {
            yield return null;
        }
        ao.allowSceneActivation = true;
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






    /*

    private void OnStartLoadingScene(Scene current, Scene next) // �� �ε尡 ������ ���
    {
        // �ε��� ���۵Ǹ� �ٸ� ������Ʈ Ŭ�� ����
        fadeImage.raycastTarget = true;

        // ���̵� �ƿ� �ڵ� ���� (���̵� �ƿ�
        // StartCoroutine(SceneChangeSequence());
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
        imageColor.a = 1f; // ���� ���ֱ� ���� while���� ������ �ٽ� �ѹ� ����
        fadeImage.color = imageColor;
        fadeTimeElapse = 0f; // �ð� ī��Ʈ �ʱ�ȭ

        // ���� �� �ε� �Ϸ���� ���
        while(!ao.isDone)
        {
            yield return null;
        }
        ao.allowSceneActivation = true;

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
        fadeImage.raycastTarget = false; // �ٸ� ������Ʈ�� Ŭ�� �����ϰ� ����

        yield return null;
    }

    */

}