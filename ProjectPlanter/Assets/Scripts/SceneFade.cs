using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


// 씬과 씬 사이의 페이드 인/아웃을 구현한 코드입니다


public class SceneFade : MonoBehaviour
{
    private static SceneFade instance = null;
    public Image fadeImage;
    public float fadeDuration;

    private void Awake()
    {

        // 씬 전환용 게임오브젝트는 모든 씬에서 돌려쓴다

        if (instance != null) // 메인 씬으로 돌아와서, SceneFade오브젝트가 중복되어 존재할 경우를 방지
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 변경되도 사운드 오브젝트가 파괴되지 않음
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

    // 외부에서 씬 전환 호출하는 프로젝트 전용 코드
    public static void SceneChange(string nextScene)
    {
        SceneFade sf = new SceneFade();
        sf.Change(nextScene);
    }

    // 씬 전환을 담당하는 코드
    public void Change(string nextScene)
    {
        // 백그라운드에서 씬 전환 시작
        AsyncOperation ao = SceneManager.LoadSceneAsync(nextScene);
        ao.allowSceneActivation = false;

        // 씬 전환 시작시 다른 조작 불가능
        fadeImage.raycastTarget = true;

        // 페이드 아웃
        StartCoroutine(FadeOut());

        // 다음 씬이 로딩 완료될 때까지 대기
        StartCoroutine(HoldForLoading(ao));

        // 다음 씬 로드 완료 시 페이드 인
        StartCoroutine(FadeIn());

        // 다른 오브젝트를 클릭 가능하게 설정
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

    IEnumerator FadeOut() // 페이드 아웃 코드
    {
        float fadeTimeElapse = 0f;
        Color imageColor = fadeImage.color;
        imageColor.a = 0f;

        // 지정된 시간 동안 페이드 아웃
        while (fadeTimeElapse < fadeDuration)
        {
            fadeTimeElapse += Time.deltaTime;
            imageColor.a = Mathf.Clamp01(fadeTimeElapse / fadeDuration);
            fadeImage.color = imageColor;
            yield return null;
        }
        imageColor.a = 1f; // 오차 없애기 위해 while문이 끝나도 다시 한번 실행
        fadeImage.color = imageColor;
    }

    IEnumerator FadeIn()
    {
        float fadeTimeElapse = 0f;
        Color imageColor = fadeImage.color;
        imageColor.a = 1f;

        // 지정된 시간 동안 페이드 인
        while (fadeTimeElapse < fadeDuration)
        {
            fadeTimeElapse += Time.deltaTime;
            imageColor.a = Mathf.Clamp01(1f - (fadeTimeElapse / fadeDuration));
            fadeImage.color = imageColor;
            yield return null;
        }
        imageColor.a = 0f; // 오차 없애기 위해 while문이 끝나도 다시 한번 실행
        fadeImage.color = imageColor;
    }






    /*

    private void OnStartLoadingScene(Scene current, Scene next) // 씬 로드가 감지된 경우
    {
        // 로딩이 시작되면 다른 오브젝트 클릭 금지
        fadeImage.raycastTarget = true;

        // 페이드 아웃 코드 실행 (페이드 아웃
        // StartCoroutine(SceneChangeSequence());
    }
    
    // 페이드 인/아웃 코드
    IEnumerator SceneChangeSequence()
    {
        float fadeTimeElapse = 0f;
        Color imageColor = fadeImage.color;
        imageColor.a = 0f;

        // 지정된 시간 동안 페이드 아웃
        while (fadeTimeElapse < fadeDuration)
        {
            fadeTimeElapse += Time.deltaTime;
            imageColor.a = Mathf.Clamp01(fadeTimeElapse / fadeDuration);
            fadeImage.color = imageColor;
            yield return null;
        }
        imageColor.a = 1f; // 오차 없애기 위해 while문이 끝나도 다시 한번 실행
        fadeImage.color = imageColor;
        fadeTimeElapse = 0f; // 시간 카운트 초기화

        // 다음 씬 로드 완료까지 대기
        while(!ao.isDone)
        {
            yield return null;
        }
        ao.allowSceneActivation = true;

        // 지정된 시간 동안 페이드 인
        while (fadeTimeElapse < fadeDuration)
        {
            fadeTimeElapse += Time.deltaTime;
            imageColor.a = Mathf.Clamp01(1f - (fadeTimeElapse / fadeDuration));
            fadeImage.color = imageColor;
            yield return null;
        }
        imageColor.a = 0f; // 오차 없애기 위해 while문이 끝나도 다시 한번 실행
        fadeImage.color = imageColor;
        fadeImage.raycastTarget = false; // 다른 오브젝트를 클릭 가능하게 설정

        yield return null;
    }

    */

}